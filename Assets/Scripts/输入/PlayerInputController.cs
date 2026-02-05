using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField, ChineseLabel("攻击键按多久才算是Hold")] private float holdThreshold = 0.5f;
    private InputData InputData => InputData.Instance;
    private MultiTimerManager MultiTimerManager => MultiTimerManager.Instance;
    private WeaponManager weaponManager => WeaponManager.Instance;
    private BuffManager buffManager => BuffManager.Instance;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (buffManager && buffManager.IsBuffSelectionOpen)
        {
            return;
        }

        Vector2 moveDirection = context.ReadValue<Vector2>();
        InputData.MoveDirection = moveDirection;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            InputData.SetMouseState(MouseState.Press);
            StartCoroutine(HoldAttackCoroutine());
        }
        else if(context.canceled)
        {
            InputData.SetMouseState(MouseState.Release);
            StopCoroutine(HoldAttackCoroutine());
        }
    }

    IEnumerator HoldAttackCoroutine()
    {
        yield return new WaitForSeconds(holdThreshold);
        if(InputData.CurrentMouseState == MouseState.Press)
        {
            InputData.SetMouseState(MouseState.Hold);
        }
    }
}


