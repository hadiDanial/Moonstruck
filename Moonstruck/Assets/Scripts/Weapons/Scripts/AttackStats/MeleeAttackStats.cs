using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Melee Attack Stats", menuName = "Moonstruck/Weapons/Melee Attack Stats"), System.Serializable]
public class MeleeAttackStats : AttackStats
{
    [Header("Attack Stats")]
    public string weaponName = "Slash";
    public int dmg = 1;
    public float timeBetweenAttacks = 0.5f;


    [Header("Other")]
    public List<AudioClip> weaponSounds;
}
