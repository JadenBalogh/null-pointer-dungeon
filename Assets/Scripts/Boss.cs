using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float knockbackReduction = 0.5f;
    [SerializeField] private float mainAttackInterval = 5f;
    [SerializeField] private int mainAttackDamage = 2;
    [SerializeField] private float mainAttackSpeed = 2;
    [SerializeField] private int mainAttackProjectileCount = 6;
    [SerializeField] private float mainAttackProjectileInterval = 0.1f;
    [SerializeField] private float mainAttackArcCoeff = 0.8f;
    [SerializeField] private Projectile mainAttackProjectile;

    private bool isTriggered = false;

    protected override void Update()
    {
        base.Update();

        if (GameManager.Player == null) return;

        if (isTriggered) return;

        Vector2 playerDiff = GameManager.Player.transform.position - transform.position;
        if (playerDiff.sqrMagnitude < detectDistance * detectDistance)
        {
            isTriggered = true;
            StartCoroutine(BossLoop());
        }
    }

    private IEnumerator BossLoop()
    {
        while (GameManager.Player != null)
        {
            yield return new WaitForSeconds(mainAttackInterval);

            for (int i = 0; i < mainAttackProjectileCount; i++)
            {
                Vector2 playerDir = (GameManager.Player.transform.position - transform.position).normalized;
                playerDir = (playerDir + Random.insideUnitCircle * mainAttackArcCoeff).normalized;
                
                Projectile proj = Instantiate(mainAttackProjectile, transform.position, Quaternion.identity);
                proj.Shoot(playerDir * mainAttackSpeed, mainAttackDamage, Vector2.zero, 0);

                yield return new WaitForSeconds(mainAttackProjectileInterval);
            }
        }
    }

    protected override void Die()
    {
        base.Die();
        GameManager.EndGame(true);
    }

    public override void Knockback(Vector2 pushForce, float duration)
    {
        base.Knockback(pushForce * knockbackReduction, duration);
    }
}
