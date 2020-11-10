using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ProjectileAttackStats))]
public class ProjectileAttackStatsEditor : Editor
{
    ProjectileAttackStats stats;
    public GameObject gObj;
    public AudioClip clip;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        stats = target as ProjectileAttackStats;

        EditorGUILayout.BeginVertical();
        stats.attackName = EditorHelper.TextField("Weapon Name", stats.attackName);
        EditorGUILayout.BeginHorizontal();
        stats.dmg = EditorHelper.IntField("Weapon Damage", stats.dmg);
        stats.timeBetweenAttacks = EditorHelper.FloatField("Time Between Attacks", stats.timeBetweenAttacks);
        EditorGUILayout.EndHorizontal();
        stats.projectilePrefab = EditorHelper.ObjectField("Projectile Prefab", stats.projectilePrefab, gObj);
        stats.projectileSpeed = EditorHelper.FloatField("Projectile Speed", stats.timeBetweenAttacks);
        EditorHelper.FoldoutObjectList(stats.weaponSounds, clip, "Weapon Sounds", "Add Weapon Sound");

        EditorGUILayout.EndVertical();
    }
}
