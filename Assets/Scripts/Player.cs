using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shootCooldown = 0.2f;
    [SerializeField] private Projectile shootProjectile;
    [SerializeField] private float shootSpeed = 10f;
    [SerializeField] private int shootDamage = 2;
    [SerializeField] private float shootForce = 5f;
    [SerializeField] private float shootKnockbackTime = 0.3f;
    [SerializeField] private Transform gunSpawnPoint;

    private bool canShoot = true;

    protected override void Update()
    {
        if (!isKnockedBack)
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");
            rigidbody2D.velocity = new Vector2(hInput, vInput).normalized * moveSpeed;
        }

        if (canShoot && Input.GetButton("Fire1"))
        {
            StartCoroutine(ShootCooldown());

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = (mousePos - (Vector2)transform.position).normalized;
            spriteRenderer.flipX = mouseDir.x > 0;

            Vector2 gunSpawn = GetGunSpawn();
            Projectile proj = Instantiate(shootProjectile, gunSpawn, Quaternion.identity);
            Vector2 gunDir = (mousePos - gunSpawn).normalized;
            proj.Shoot(gunDir * shootSpeed, shootDamage, gunDir * shootForce, shootKnockbackTime);
        }

        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        GameManager.EndGame(false);
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private Vector2 GetGunSpawn()
    {
        return Vector2.right * (transform.position.x + (spriteRenderer.flipX ? -gunSpawnPoint.localPosition.x : gunSpawnPoint.localPosition.x)) + Vector2.up * gunSpawnPoint.position.y;
    }
}
