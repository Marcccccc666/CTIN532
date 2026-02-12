using Unity.Cinemachine;

public class RoomFighting : BaseState<RoomState>
{
    private CinemachineCamera roomCamera;
    private EnemyManager enemyManager = EnemyManager.Instance;
    private CameraManager cameraManager = CameraManager.Instance;
    public RoomFighting(CinemachineCamera roomCamera) : base()
    {
        this.roomCamera = roomCamera;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // 切换到房间相机
        cameraManager.SetCurrentCamera(roomCamera);

        // 放狗
        foreach(var enemy in enemyManager.GetEnemyDataDict.Values)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
