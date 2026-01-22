using UnityEngine;

public class Die : CharacterState<CharacterHFSM.CharacterStateID>
{
    public Die() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入死亡状态");
    }
}
