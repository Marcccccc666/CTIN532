using UnityEngine;
using UnityEngine.UI;
public class Enemy_Health : MonoBehaviour
{

    public float maxHealth = 5f; 
    public float currentHealth;

    public Enemy_HitEffect1 enemyHitEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        var player = GameObject.FindWithTag("Player");
        var attacker = player != null ? player.transform : transform;
        enemyHitEffect.Knockback(attacker, 5f);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            
        }

    }
}
