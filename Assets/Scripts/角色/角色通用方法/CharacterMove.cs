using UnityEngine;

public static class CharacterMove
{
    /// <summary>
    /// 移动角色
    /// </summary>
    /// <param name="Character">角色刚体</param>
    /// <param name="direction">移动方向</param>
    /// <param name="speed">移动速度</param>
    public static void MoveCharacter(Rigidbody2D Character, Vector3 direction, float speed)
    {
        Vector3 movement = direction.normalized * speed * Time.fixedDeltaTime;
        Character.MovePosition(Character.position + (Vector2)movement);
    }

    /// <summary>
    /// 移动角色
    /// </summary>
    /// <param name="Character">角色变换组件</param>
    /// <param name="direction">移动方向</param>
    /// <param name="speed">移动速度</param>
    public static void MoveCharacter(Transform Character, Vector3 direction, float speed)
    {
        Vector3 movement = direction.normalized * speed * Time.deltaTime;
        Character.position += movement;
    }
}
