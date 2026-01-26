using System.Reflection;
using System.Security;
using UnityEngine;
using UnityHFSM;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isChasing;
    public float speed = 3f;
    private Transform target;
    private int facingDirection = -1;
    private EnemyState enemyState;
    private Animator animator;
    void Start()
    {   
        ChangeState(EnemyState.Idle);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Flip()
    {
        facingDirection *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void Update()
    {
        if (!isChasing || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
        if (target.position.x > transform.position.x && facingDirection == -1)
        {
            Flip();
        }
        else if (target.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision.transform; 
        }
        isChasing = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;  
        }
        isChasing = false;
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == enemyState.Idle) {
            animator.SetBool("isIdle", false);
        } else if (enemyState == enemyState.Chasing) {
            animator.SetBool("isChasing", true);
        } 
    }
}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}