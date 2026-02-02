using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomLevelConfig
{
    public string roomName;
    public Transform spawnTop;
    public Transform spawnBottom;
    public GameObject entryGate;
    public GameObject exitGate;
    public Collider2D enterTrigger;
}

public class RoomLevelManager : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private EnemyMaker enemyMaker;
    [SerializeField] private List<RoomLevelConfig> rooms = new List<RoomLevelConfig>();
    [SerializeField] private bool autoStartFirstLevel = false;
    [SerializeField] private int firstRoomIndex = 0;
    [SerializeField] private bool debugLogs = false;

    private GameManager gameManager => GameManager.Instance;
    private EnemyManager enemyManager => EnemyManager.Instance;
    private CharacterManager characterManager => CharacterManager.Instance;

    private int activeRoomIndex = -1;

    private void Awake()
    {
        if (levelController == null)
        {
            levelController = FindObjectOfType<LevelController>();
        }

        if (enemyMaker == null)
        {
            enemyMaker = FindObjectOfType<EnemyMaker>();
        }

        InitializeRooms();
    }

    private void Start()
    {
        if (autoStartFirstLevel)
        {
            StartRoom(firstRoomIndex);
        }
    }

    private void OnEnable()
    {
        gameManager.GameCheckout += OnGameCheckout;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            gameManager.GameCheckout -= OnGameCheckout;
        }
    }

    private void InitializeRooms()
    {
        foreach (RoomLevelConfig room in rooms)
        {
            SetGateClosed(room.exitGate, true);

            if (room.enterTrigger != null)
            {
                room.enterTrigger.enabled = false;
            }
        }
    }

    public void StartRoom(int roomIndex)
    {
        if (!IsValidRoomIndex(roomIndex))
        {
            return;
        }

        activeRoomIndex = roomIndex;
        RoomLevelConfig room = rooms[roomIndex];

        if (debugLogs)
        {
            Debug.Log($"[RoomLevelManager] StartRoom({roomIndex})");
        }

        if (enemyMaker != null)
        {
            enemyMaker.SetSpawnBounds(room.spawnTop, room.spawnBottom);
        }

        SetGateClosed(room.entryGate, true);

        SetGateClosed(room.exitGate, true);

        Collider2D exitTrigger = GetExitTriggerForRoom(roomIndex);
        if (exitTrigger != null)
        {
            exitTrigger.enabled = false;
        }
        else if (debugLogs)
        {
            Debug.LogWarning($"[RoomLevelManager] No exit trigger found for room {roomIndex}");
        }

        levelController?.NextLevel();
    }

    private void OnGameCheckout()
    {
        if (!IsLevelClear() || !IsValidRoomIndex(activeRoomIndex))
        {
            return;
        }

        RoomLevelConfig room = rooms[activeRoomIndex];
        SetGateClosed(room.exitGate, false);

        Collider2D exitTrigger = GetExitTriggerForRoom(activeRoomIndex);
        if (exitTrigger != null)
        {
            exitTrigger.enabled = true;
        }
        else if (debugLogs)
        {
            Debug.LogWarning($"[RoomLevelManager] No exit trigger found for room {activeRoomIndex}");
        }
    }

    private bool IsLevelClear()
    {
        if (characterManager.GetCurrentPlayerCharacterData == null)
        {
            return false;
        }

        if (characterManager.GetCurrentPlayerCharacterData.CurrentHealth <= 0)
        {
            return false;
        }

        return enemyManager.GetEnemyDataDict.Count == 0;
    }

    private bool IsValidRoomIndex(int roomIndex)
    {
        return roomIndex >= 0 && roomIndex < rooms.Count;
    }

    private Collider2D GetExitTriggerForRoom(int roomIndex)
    {
        if (!IsValidRoomIndex(roomIndex))
        {
            return null;
        }

        RoomLevelConfig room = rooms[roomIndex];
        int nextIndex = roomIndex + 1;
        if (!IsValidRoomIndex(nextIndex))
        {
            return null;
        }

        if (MatchesNextRoom(room.enterTrigger, nextIndex))
        {
            return room.enterTrigger;
        }

        RoomLevelConfig nextRoom = rooms[nextIndex];
        if (MatchesNextRoom(nextRoom.enterTrigger, nextIndex))
        {
            return nextRoom.enterTrigger;
        }

        foreach (RoomLevelConfig config in rooms)
        {
            if (MatchesNextRoom(config.enterTrigger, nextIndex))
            {
                return config.enterTrigger;
            }
        }

        return null;
    }

    private bool MatchesNextRoom(Collider2D trigger, int nextIndex)
    {
        if (trigger == null)
        {
            return false;
        }

        RoomTransitionTrigger transition = trigger.GetComponent<RoomTransitionTrigger>();
        return transition != null && transition.NextRoomIndex == nextIndex;
    }

    private void SetGateClosed(GameObject gate, bool closed)
    {
        if (gate == null)
        {
            return;
        }

        foreach (Collider2D collider in gate.GetComponentsInChildren<Collider2D>(true))
        {
            if (collider.GetComponent<RoomTransitionTrigger>() != null)
            {
                continue;
            }

            collider.enabled = closed;
        }

        foreach (Renderer renderer in gate.GetComponentsInChildren<Renderer>(true))
        {
            renderer.enabled = closed;
        }
    }
}
