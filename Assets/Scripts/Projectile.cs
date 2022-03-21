using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private string friendlyLayer;
    [SerializeField] private string targetLayer;

    private int damage;
    private Vector2 knockbackForce;
    private float knockbackTime;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 velocity, int damage, Vector2 knockbackForce, float knockbackTime)
    {
        this.damage = damage;
        this.knockbackForce = knockbackForce;
        this.knockbackTime = knockbackTime;

        rigidbody2D.velocity = velocity;
        transform.rotation = Quaternion.FromToRotation(Vector2.up, velocity);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string colLayer = LayerMask.LayerToName(col.gameObject.layer);
        if (colLayer == targetLayer && col.TryGetComponent<Actor>(out Actor actor))
        {
            actor.TakeDamage(damage);
            if (knockbackTime > 0)
            {
                actor.Knockback(knockbackForce, knockbackTime);
            }
        }

        if (colLayer != friendlyLayer)
        {
            Destroy(gameObject);
        }
    }
}
