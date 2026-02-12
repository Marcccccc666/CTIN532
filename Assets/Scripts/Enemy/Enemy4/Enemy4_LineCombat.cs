using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Enemy4_LineCombat : MonoBehaviour
{
    [Header("射线配置")]
    [SerializeField, ChineseLabel("发射点")] private Transform firePoint;
    [SerializeField, ChineseLabel("阻挡层")] private LayerMask obstacleMask;
    [SerializeField, ChineseLabel("伤害检测层")] private LayerMask damageMask;
    [SerializeField, ChineseLabel("射线宽度")] private float lineWidth = 0.08f;
    [SerializeField, ChineseLabel("射线颜色")] private Color lineColor = Color.red;
    [SerializeField, ChineseLabel("攻击音效")] private AudioClip hitAudio;

    private LineRenderer lineRenderer;
    private Vector2 currentStart;
    private Vector2 currentEnd;

    public Transform FirePoint => firePoint;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupLineRenderer();
        EndAimLine();
    }

    private void SetupLineRenderer()
    {
        if (lineRenderer == null)
        {
            return;
        }

        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.numCapVertices = 4;

        if (lineRenderer.sharedMaterial == null)
        {
            Shader shader = Shader.Find("Sprites/Default");
            if (shader != null)
            {
                lineRenderer.sharedMaterial = new Material(shader);
            }
        }
    }

    public void BeginAimLine(Vector2 targetPosition, float maxDistance)
    {
        if (lineRenderer == null)
        {
            return;
        }

        lineRenderer.enabled = true;
        UpdateAimLine(targetPosition, maxDistance);
    }

    public void UpdateAimLine(Vector2 targetPosition, float maxDistance)
    {
        if (lineRenderer == null)
        {
            return;
        }

        Vector2 origin = firePoint != null ? (Vector2)firePoint.position : (Vector2)transform.position;
        Vector2 direction = targetPosition - origin;
        if (direction.sqrMagnitude < 0.0001f)
        {
            direction = firePoint != null ? (Vector2)firePoint.right : (Vector2)transform.right;
        }

        Vector2 normalizedDirection = direction.normalized;
        float distance = Mathf.Max(0.1f, maxDistance);
        RaycastHit2D obstacleHit = Physics2D.Raycast(origin, normalizedDirection, distance, obstacleMask);
        Vector2 end = obstacleHit.collider != null ? obstacleHit.point : origin + normalizedDirection * distance;

        currentStart = origin;
        currentEnd = end;

        lineRenderer.SetPosition(0, currentStart);
        lineRenderer.SetPosition(1, currentEnd);
    }

    public void FireLockedLine(int damage)
    {
        Vector2 segment = currentEnd - currentStart;
        float distance = segment.magnitude;
        if (distance <= 0.001f)
        {
            return;
        }

        int finalMask = damageMask.value == 0 ? Physics2D.DefaultRaycastLayers : damageMask.value;
        RaycastHit2D[] hits = Physics2D.RaycastAll(currentStart, segment.normalized, distance, finalMask);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D collider = hits[i].collider;
            if (collider == null)
            {
                continue;
            }

            if (collider.CompareTag("Enemy"))
            {
                continue;
            }

            if (collider.CompareTag("Player"))
            {
                CharacterManager manager = CharacterManager.Instance;
                if (manager != null && manager.GetCurrentPlayerCharacterData != null)
                {
                    manager.GetCurrentPlayerCharacterData.Damage(Mathf.Max(0, damage));
                }
                break;
            }

            if (collider.CompareTag("Wall"))
            {
                break;
            }
        }

        if (hitAudio != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(hitAudio, Camera.main.transform.position);
        }
    }

    public void EndAimLine()
    {
        if (lineRenderer == null)
        {
            return;
        }

        lineRenderer.enabled = false;
    }
}
