using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Attack Stats", menuName = "Moonstruck/Weapons/Projectile Attack Stats")]
public class ProjectileAttackStats : AttackStats
{
    [Header("Projectile Settings")]
    public float projectileSpeed = 3;
    public GameObject projectilePrefab;
    public bool affectedByGravity = true;
}
