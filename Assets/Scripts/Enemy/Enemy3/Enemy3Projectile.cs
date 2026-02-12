using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Enemy3Projectile : MonoBehaviour
{
    [SerializeField, ChineseLabel("默认伤害")] private int defaultDamage = 1;
    [SerializeField, ChineseLabel("默认速度")] private float defaultSpeed = 8f;
    [SerializeField, ChineseLabel("默认存活时长")] private float defaultLifetime = 3f;

    private Rigidbody2D rb2D;
    private int damage;
    private bool initialized;
    private bool consumed;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (initialized)
        {
            return;
        }

        damage = defaultDamage;
        rb2D.linearVelocity = (Vector2)transform.right * defaultSpeed;
        Destroy(gameObject, Mathf.Max(0.1f, defaultLifetime));
    }

    public void Initialize(Vector2 direction, float speed, int bulletDamage, float lifetime)
    {
        initialized = true;
        damage = Mathf.Max(0, bulletDamage);

        Vector2 normalizedDirection = direction.sqrMagnitude < 0.0001f ? Vector2.right : direction.normalized;
        rb2D.linearVelocity = normalizedDirection * Mathf.Max(0f, speed);

        float angle = Mathf.Atan2(normalizedDirection.y, normalizedDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, Mathf.Max(0.1f, lifetime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null)
        {
            return;
        }

        HandleHit(collision.collider);
    }

    private void HandleHit(Collider2D collision)
    {
        if (consumed || collision == null)
        {
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            CharacterManager manager = CharacterManager.Instance;
            if (manager != null && manager.GetCurrentPlayerCharacterData != null)
            {
                manager.GetCurrentPlayerCharacterData.Damage(damage);
            }

            Consume();
            return;
        }

        if (collision.CompareTag("Wall") || !collision.isTrigger)
        {
            Consume();
        }
    }

    private void Consume()
    {
        consumed = true;
        Destroy(gameObject);
    }
}
