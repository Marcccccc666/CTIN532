using System.Collections;
using UnityEngine;

public class Enemy_HitEffect1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Color flashColor = new Color(1f, 0f, 0f, 0.7f); 
    public float flashDuration = 0.1f;
    public int numberOfFlashes = 3;   
    public float knockbackDisableDuration = 0.2f;

    private Color originalColor;
    private Coroutine flashCoroutine;
    private Coroutine knockbackCoroutine;
    public Enemy_Movement enemyMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            originalColor = sr.color;
        }
    }

    public void Knockback(Transform playerTransform, float knockbackForce)
    {
        if (rb == null || playerTransform == null) return;

        Vector2 knockbackDirection = (rb.position - (Vector2)playerTransform.position).normalized;

        if (enemyMovement != null) enemyMovement.isChasing = false;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        if (knockbackCoroutine != null) StopCoroutine(knockbackCoroutine);
        knockbackCoroutine = StartCoroutine(ResumeChaseAfterDelay());

        Flash();
    }

    private IEnumerator ResumeChaseAfterDelay()
    {
        yield return new WaitForSeconds(knockbackDisableDuration);
        if (enemyMovement != null) enemyMovement.isChasing = true;
        knockbackCoroutine = null;
    }

    public void Flash()
    {
        if (sr == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            sr.color = originalColor;
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
        flashCoroutine = null;
    }
}