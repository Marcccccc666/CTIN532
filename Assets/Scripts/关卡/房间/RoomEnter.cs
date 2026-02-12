using UnityEngine;

/// <summary>
/// 玩家进入房间
/// </summary>
public class RoomEnter : MonoBehaviour
{
    [SerializeField, ChineseLabel("房间控制器")] private BattleRoomController roomController;
    
    private void Awake()
    {
        if(roomController == null)
        {
            OnValidate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("玩家进入房间");
            roomController.PlayerEnterRoom();
        }
    }

#region UNITY_EDITOR
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    private void OnValidate()
    {
        if(roomController == null)
        {
            roomController = GetComponentInParent<BattleRoomController>();

            if(roomController == null)
            {
                Debug.LogError("RoomEnter脚本未找到RoomController组件，请检查！");
            }
        }
    }
#endregion

}
