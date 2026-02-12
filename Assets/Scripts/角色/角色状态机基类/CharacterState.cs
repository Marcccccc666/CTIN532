using UnityEngine;

public class CharacterState<TStateId> : BaseState<TStateId>
{
    protected InputManager inputManager => InputManager.Instance;
    public CharacterState(bool needsExitTime = false, bool isGhostState = false) : base(needsExitTime:needsExitTime, isGhostState:isGhostState)
    {
    }   
}
