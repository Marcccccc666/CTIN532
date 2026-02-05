using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    private InputData InputData => InputData.Instance;

    private GameManager gameManager => GameManager.Instance;

    private BuffManager buffManager => BuffManager.Instance;

    protected virtual void OnEnable()
    {
        InputData.IsAttackAction += Attack;
    }

    protected virtual void Update()
    {
        if (gameManager && gameManager.IsGamePaused || (buffManager && buffManager.IsBuffSelectionOpen))
        {
            return;
        }

        Vector2 mouseWorldPosition = InputData.MouseWorldPosition;
        ObjectRotation.RotateTowardsTarget(this.transform, mouseWorldPosition, 1000f);
    }

    protected virtual void OnDisable()
    {
        InputData.IsAttackAction -= Attack;
    }


    /// <summary>
    /// 武器攻击方法
    /// </summary>
    public virtual void Attack()
    {
        
    }
}
