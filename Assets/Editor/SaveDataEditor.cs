using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SaveDataEditor : EditorWindow
{
    SaveData saveData;
    const string saveDataFileName = "SaveData.json";
    bool loaded;
    [MenuItem("Window/SaveDataEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SaveDataEditor));
    }

    private void OnGUI()
    {
        int height = 20;
        if (loaded)
        {
            //Ejemplo de variable int
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Current Level Index", GUILayout.Height(height), GUILayout.Width(200));
            saveData.currentLevelIndex = EditorGUILayout.IntField(saveData.currentLevelIndex, GUILayout.Height(height), GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();

            //Ejemplo de array de bool
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Bool Array Example", GUILayout.Height(height), GUILayout.Width(200));
            for (int i = 0; i < saveData.hasTamagotchi.Length; i++)
            {
                saveData.hasTamagotchi[i] = EditorGUILayout.Toggle(saveData.hasTamagotchi[i], GUILayout.Height(height), GUILayout.Width(20));
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save Data"))
            {
                File.WriteAllText(Application.persistentDataPath + "/" + saveDataFileName, JsonUtility.ToJson(saveData));
                loaded = false;
            }
        }
        else
        {
            if (GUILayout.Button("Change SaveData Values"))
            {
                saveData =SaveDataController.GetSaveData();
                loaded= true;
            }
        }

    }
}
