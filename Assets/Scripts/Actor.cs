using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    [SerializeField] private bool isSpriteRight = false;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private GameObject deathEffect;

    public UnityEvent<int, int> OnHealthChanged { get; private set; }
    public UnityEvent OnDeath { get; private set; }

    private int health;
    protected bool isKnockedBack = false;

    protected SpriteRenderer spriteRenderer;
    protected new Rigidbody2D rigidbody2D;
    private Coroutine knockbackCR;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
        OnHealthChanged = new UnityEvent<int, int>();
        OnDeath = new UnityEvent();
    }

    protected virtual void Start()
    {
        OnHealthChanged.Invoke(health, maxHealth);
    }

    protected virtual void Update()
    {
        if (rigidbody2D.velocity.sqrMagnitude > 0.01f)
        {
            spriteRenderer.flipX = isSpriteRight ? rigidbody2D.velocity.x < 0 : rigidbody2D.velocity.x > 0;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        OnHealthChanged.Invoke(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Knockback(Vector2 pushForce, float duration)
    {
        rigidbody2D.AddForce(pushForce, ForceMode2D.Impulse);

        if (knockbackCR != null) StopCoroutine(knockbackCR);
        knockbackCR = StartCoroutine(KnockbackTimer(duration));
    }

    private IEnumerator KnockbackTimer(float duration)
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
    }

    protected virtual void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}
