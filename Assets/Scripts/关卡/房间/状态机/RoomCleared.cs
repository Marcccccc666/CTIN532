using System.Collections.Generic;
using UnityEngine;

public class RoomCleared : BaseState<RoomState>
{
    private BattleRoomController battleRoomController;
    private bool isFirstRoom;
    private EnemyBulletAttack enemyBulletProfab;

    private BuffManager buffManager => BuffManager.Instance;
    private CharacterManager characterManager => CharacterManager.Instance;
    protected WeaponManager weaponManager => WeaponManager.Instance;
    private PoolManager poolManager => PoolManager.Instance;


    public RoomCleared(BattleRoomController battleRoomController, bool isFirstRoom, EnemyBulletAttack enemyBullProfab) : base()
    {
        this.battleRoomController = battleRoomController;
        this.isFirstRoom = isFirstRoom;
        this.enemyBulletProfab = enemyBullProfab;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // 回收房间内的敌人子弹
        if(enemyBulletProfab != null)
        {
            poolManager.ReleasePool(enemyBulletProfab);
        }

        // 开门
        battleRoomController.SetLockRoom(false);

        int currentHealth = characterManager.GetCurrentPlayerCharacterData.CurrentHealth;
        if(isFirstRoom && currentHealth > 0)
        {
            weaponManager.UpgradeCurrentWeaponInvoke();
        }
        else if(currentHealth > 0)
        {
            // 房间清理后触发 Buff 选择界面
            buffManager.RequestBuffSelection();
        }
        
    }
}
