using System.Data;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public Rigidbody2D rb;
    public Animator animator;
    public int facingDirection = 1;
    public Warrior_Combat playerCombat;
    void Flip()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void Update()
    { 
        if (Input.GetButtonDown("attack"))
        {
            playerCombat.attack();
        }
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);

        if (horizontal > 0 && facingDirection == -1)
        {
            Flip();
        }
        else if (horizontal < 0 && facingDirection == 1)
        {
            Flip();
        }

        animator.SetFloat("horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("vertical", Mathf.Abs(vertical));

    }
}
