using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponStats))]
public class WeaponStatsEditor : Editor
{
    WeaponStats stats;
    AudioClip clip;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        stats = target as WeaponStats;

        EditorGUILayout.BeginVertical();
        stats.weaponName = EditorHelper.TextField("Weapon Name", stats.weaponName);
        EditorGUILayout.BeginHorizontal();
        stats.dmg = EditorHelper.IntField("Weapon Damage", stats.dmg);
        stats.timeBetweenAttacks = EditorHelper.FloatField("Time Between Attacks", stats.timeBetweenAttacks);
        EditorGUILayout.EndHorizontal();
        EditorHelper.FoldoutObjectList(stats.weaponSounds, clip, "Weapon Sounds", "Add Weapon Sound");

        EditorGUILayout.EndVertical();
    }
}
