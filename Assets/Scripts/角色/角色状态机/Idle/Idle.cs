using UnityEngine;

public class Idle : CharacterState<CharacterHFSM.CharacterStateID>
{
    public Idle(): base()
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入待机状态");
    }
}
