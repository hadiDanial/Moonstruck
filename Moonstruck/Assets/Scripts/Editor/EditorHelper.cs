using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EditorHelper : Editor
{
    static bool showFoldoutList = false;

    /// <summary>
    /// Shows a Foldout List of type T
    /// </summary>
    /// <typeparam name="T"> Class of the list (List<AudioClip> for example) </AudioClip></typeparam>
    /// <param name="myList">The list to show</param>
    /// <param name="type">The type of the list (must be a new/empty object).</param>
    /// <param name="headerLabel">The label for the foldout header.</param>
    /// <param name="addLabel">The label of the button used to add a new item to the list</param>
    public static void FoldoutObjectList<T>(List<T> myList, T type, string headerLabel, string addLabel) where T : Object
    {
        EditorGUILayout.Space();
        showFoldoutList = EditorGUILayout.BeginFoldoutHeaderGroup(showFoldoutList, "Weapon Sounds");
        if (showFoldoutList)
        {
            if (GUILayout.Button("Add Weapon Sound"))
            {
                myList.Add(type);
            }
            for (int i = 0; i < myList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                myList[i] = (T)ObjectField(i.ToString(), myList[i], type, 30);
                if (GUILayout.Button("Remove"))
                {
                    myList.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Last Item"))
            {
                myList.RemoveAt(myList.Count - 1);
            }
            if (GUILayout.Button("Clear List"))
            {
                myList.Clear();
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    /// <summary>
    /// Create a EditorGUILayout.TextField with no space between label and text field
    /// </summary>
    public static string TextField(string label, string text)
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
        EditorGUIUtility.labelWidth = textDimensions.x + 7;
        return EditorGUILayout.TextField(label, text);
    }

    /// <summary>
    /// Create a EditorGUILayout.IntField with no space between label and text field
    /// </summary>
    public static int IntField(string label, int num)
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
        EditorGUIUtility.labelWidth = textDimensions.x + 7;
        return EditorGUILayout.IntField(label, num);
    }

    /// <summary>
    /// Create a EditorGUILayout.FloatField with no space between label and text field
    /// </summary>
    public static float FloatField(string label, float num)
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
        EditorGUIUtility.labelWidth = textDimensions.x + 7;
        return EditorGUILayout.FloatField(label, num);
    }
    /// <summary>
    /// Create a EditorGUILayout.ObjectField of type T with custom or no space between label and text field
    /// </summary>
    /// <typeparam name="T">Type of the object to display</typeparam>
    /// <param name="label">Label to show</param>
    /// <param name="obj">Object to show</param>
    /// <param name="type">Type to show</param>
    /// <param name="customWidth">If -1, fit to text, otherwise, use that width</param>
    /// <returns></returns>
    public static T ObjectField<T>(string label, Object obj, T type, float customWidth = -1) where T : Object
    {
        var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
        if (customWidth != -1)
            EditorGUIUtility.labelWidth = customWidth;
        else
            EditorGUIUtility.labelWidth = textDimensions.x + 7;
        return EditorGUILayout.ObjectField(label, obj, typeof(T), true) as T;
    }
}
