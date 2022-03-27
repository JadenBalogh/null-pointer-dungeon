using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 51)]
public class Item : ScriptableObject
{
    public float shootCooldown = 0.2f;
    public Projectile shootProjectile;
    public float shootSpeed = 10f;
    public int shootDamage = 2;
    public float shootForce = 5f;
    public float shootKnockbackTime = 0.3f;
}
