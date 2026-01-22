using UnityEngine;

public class Warrior_Combat : MonoBehaviour
{
    public Animator aim;
    private float timer;
    public float cooldown = 0.5f;
    public PlayerMovement playerMovement;
    public Transform AttackPoint;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRange = 0.7f;
    public float knockbackForce = 30f;
    
    public AudioSource audioSource;
    public AudioClip swordSwingSound; 
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void attack()
    {
        if  (timer <= 0)
        {
            aim.SetBool("is_attacking", true);
            timer = cooldown;
            playerMovement.speed = 0;
        }
    }  
    public void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
    public void DealDamage()
    {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
            if (hitEnemies.Length > 0)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy_combat>().TakeDamage(attackDamage);
                    enemy.GetComponent<Enemy_HitEffect1>().Knockback(AttackPoint, knockbackForce);
                }
            }
    }
    public void stop_attack()
    {
        aim.SetBool("is_attacking", false);
        playerMovement.speed = 5;
    }
    public void PlaySwingSound()
    {
        if (audioSource != null && swordSwingSound != null)
        {
            audioSource.PlayOneShot(swordSwingSound);
        }
    }
}
