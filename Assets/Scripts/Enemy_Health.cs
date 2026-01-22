using UnityEngine;
using UnityEngine.UI;
public class Enemy_combat : MonoBehaviour
{

    public float maxHealth = 5f; 
    public float currentHealth;

    public Image healthBarFill;
    public GameObject healthBarCanvas;
    public Enemy_HitEffect1 enemyHitEffect;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHitEffect.Knockback(transform, 30f);
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            if (healthBarCanvas != null) healthBarCanvas.SetActive(false);
        }

    }
    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }
}   
