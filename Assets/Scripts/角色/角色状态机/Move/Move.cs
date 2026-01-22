using UnityEngine;

public class Move : CharacterState<CharacterHFSM.CharacterStateID>
{
    public Move() : base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入移动状态");
    }

    public override void OnLogic()
    {
        base.OnLogic();
    }

}
