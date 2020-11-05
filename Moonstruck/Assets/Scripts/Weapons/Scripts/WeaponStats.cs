using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "Moonstruck/Weapons/Weapon Stats"), System.Serializable]
public class WeaponStats : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName = "Weapon";
    public int dmg = 1;
    public float timeBetweenAttacks = 0.5f;


    [Header("Other")] 
    public List<AudioClip> weaponSounds;
}
