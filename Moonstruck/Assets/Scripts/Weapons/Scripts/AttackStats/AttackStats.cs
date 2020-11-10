using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Attack Stats", menuName = "Moonstruck/Weapons/Attack Stats"), System.Serializable]
public abstract class AttackStats : ScriptableObject
{
    [Header("Attack Stats")]
    public string attackName = "Slash";
    public int dmg = 1;
    public float timeBetweenAttacks = 0.5f;


    [Header("Other")] 
    public List<AudioClip> weaponSounds;
}
