using System.Collections.Generic;
using UnityEngine;

public class RoomCleared : BaseState<RoomState>
{
    private BattleRoomController battleRoomController;

    private BuffManager buffManager => BuffManager.Instance;
    private CameraManager cameraManager => CameraManager.Instance;

    public RoomCleared(BattleRoomController battleRoomController) : base()
    {
        this.battleRoomController = battleRoomController;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        cameraManager.ResetToDefaultCamera();
        battleRoomController.SetLockRoom(false);

        // 房间清理后触发 Buff 选择界面
        buffManager.RequestBuffSelection();
    }
}
