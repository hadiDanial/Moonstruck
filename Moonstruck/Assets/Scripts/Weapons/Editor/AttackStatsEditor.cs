using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackStats))]
public class AttackStatsEditor : Editor
{
    AttackStats stats;
    public AudioClip clip;
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        stats = target as AttackStats;

        EditorGUILayout.BeginVertical();
        stats.attackName = EditorHelper.TextField("Weapon Name", stats.attackName);
        EditorGUILayout.BeginHorizontal();
        stats.dmg = EditorHelper.IntField("Weapon Damage", stats.dmg);
        stats.timeBetweenAttacks = EditorHelper.FloatField("Time Between Attacks", stats.timeBetweenAttacks);
        EditorGUILayout.EndHorizontal();
        EditorHelper.FoldoutObjectList(stats.weaponSounds, clip, "Weapon Sounds", "Add Weapon Sound");

        EditorGUILayout.EndVertical();
    }
}
