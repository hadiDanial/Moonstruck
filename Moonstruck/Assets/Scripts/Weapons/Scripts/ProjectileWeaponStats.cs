using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Weapon Stats", menuName = "Moonstruck/Weapons/Projectile Weapon Stats")]
public class ProjectileWeaponStats : WeaponStats
{
    [Header("Projectile Settings")]
    public float projectileSpeed = 3;
    public GameObject projectilePrefab;
}
