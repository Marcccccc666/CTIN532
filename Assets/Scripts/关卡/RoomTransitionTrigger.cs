using UnityEngine;

public class RoomTransitionTrigger : MonoBehaviour
{
    [SerializeField] private RoomLevelManager roomManager;
    [SerializeField] private int nextRoomIndex = 0;
    [SerializeField] private bool disableAfterTriggered = true;
    [SerializeField] private bool requireCrossing = true;
    [SerializeField] private float crossingBuffer = 0.05f;
    [SerializeField] private bool debugLogs = false;

    private Collider2D triggerCollider;
    private bool hasTriggered;
    private readonly Collider2D[] overlapResults = new Collider2D[4];
    private bool armed;
    private bool entrySideInitialized;
    private int entrySide;
    private bool useXAxis;
    private Transform cachedPlayer;

    public int NextRoomIndex => nextRoomIndex;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
        if (roomManager == null)
        {
            roomManager = FindObjectOfType<RoomLevelManager>();
        }
    }

    private void Update()
    {
        if (hasTriggered || triggerCollider == null)
        {
            return;
        }

        if (!triggerCollider.enabled)
        {
            armed = false;
            entrySideInitialized = false;
            return;
        }

        if (!armed)
        {
            ArmTrigger();
        }

        CheckOverlapFallback();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTrigger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleTrigger(collision);
    }

    private void HandleTrigger(Collider2D collision)
    {
        if (hasTriggered)
        {
            return;
        }

        if (!IsPlayer(collision))
        {
            if (debugLogs)
            {
                Debug.Log($"[RoomTransitionTrigger] Ignored collider '{collision.name}' tag '{collision.tag}'");
            }
            return;
        }

        if (requireCrossing && !HasCrossed(collision.transform.root.position))
        {
            return;
        }

        hasTriggered = true;
        if (debugLogs)
        {
            Debug.Log($"[RoomTransitionTrigger] Triggered -> StartRoom({nextRoomIndex})");
        }
        if (roomManager != null)
        {
            roomManager.StartRoom(nextRoomIndex);
        }

        if (disableAfterTriggered && triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }

    private void CheckOverlapFallback()
    {
        if (triggerCollider == null)
        {
            return;
        }

        Bounds bounds = triggerCollider.bounds;
        int count = Physics2D.OverlapBoxNonAlloc(
            bounds.center,
            bounds.size,
            0f,
            overlapResults
        );

        for (int i = 0; i < count; i++)
        {
            Collider2D hit = overlapResults[i];
            if (hit == null)
            {
                continue;
            }

            HandleTrigger(hit);
            if (hasTriggered)
            {
                return;
            }
        }
    }

    private void ArmTrigger()
    {
        armed = true;
        entrySideInitialized = false;
        entrySide = 0;
        useXAxis = ShouldUseXAxis();

        if (requireCrossing)
        {
            UpdateEntrySide();
        }
    }

    private bool ShouldUseXAxis()
    {
        if (triggerCollider == null)
        {
            return true;
        }

        Vector3 size = triggerCollider.bounds.size;
        return size.x <= size.y;
    }

    private void UpdateEntrySide()
    {
        Transform player = GetPlayerTransform();
        if (player == null)
        {
            return;
        }

        float delta = GetAxisDelta(player.position);
        if (Mathf.Abs(delta) < crossingBuffer)
        {
            return;
        }

        entrySide = delta > 0f ? 1 : -1;
        entrySideInitialized = true;

        if (debugLogs)
        {
            Debug.Log($"[RoomTransitionTrigger] Entry side set to {entrySide}");
        }
    }

    private bool HasCrossed(Vector3 playerPosition)
    {
        if (!requireCrossing)
        {
            return true;
        }

        float delta = GetAxisDelta(playerPosition);
        if (Mathf.Abs(delta) < crossingBuffer)
        {
            return false;
        }

        int currentSide = delta > 0f ? 1 : -1;
        if (!entrySideInitialized)
        {
            entrySide = currentSide;
            entrySideInitialized = true;
            return false;
        }

        return currentSide != entrySide;
    }

    private float GetAxisDelta(Vector3 position)
    {
        if (triggerCollider == null)
        {
            return 0f;
        }

        Vector3 center = triggerCollider.bounds.center;
        return useXAxis ? position.x - center.x : position.y - center.y;
    }

    private Transform GetPlayerTransform()
    {
        if (cachedPlayer != null)
        {
            return cachedPlayer;
        }

        CharacterManager characterManager = CharacterManager.Instance;
        if (characterManager != null && characterManager.GetCurrentPlayerCharacterData != null)
        {
            cachedPlayer = characterManager.GetCurrentPlayerCharacterData.transform;
            return cachedPlayer;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            cachedPlayer = player.transform;
        }

        return cachedPlayer;
    }

    private bool IsPlayer(Collider2D collision)
    {
        if (collision == null)
        {
            return false;
        }

        if (collision.CompareTag("Player"))
        {
            return true;
        }

        Transform root = collision.transform.root;
        return root != null && root.CompareTag("Player");
    }
}
