using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] protected float detectDistance = 6f;
    [SerializeField] private float hitCooldown = 1f;
    [SerializeField] private int damageOnHit = 2;

    private bool canHit = true;

    protected override void Update()
    {
        if (GameManager.Player == null) return;

        if (!isKnockedBack)
        {
            Vector2 playerDiff = GameManager.Player.transform.position - transform.position;
            if (playerDiff.sqrMagnitude < detectDistance * detectDistance)
            {
                rigidbody2D.velocity = playerDiff.normalized * moveSpeed;
            }
            else
            {
                rigidbody2D.velocity = Vector2.zero;
            }
        }

        base.Update();
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (canHit && col.gameObject.CompareTag("Player"))
        {
            GameManager.Player.TakeDamage(damageOnHit);
            StartCoroutine(HitCooldown());
        }
    }

    private IEnumerator HitCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canHit = true;
    }
}
