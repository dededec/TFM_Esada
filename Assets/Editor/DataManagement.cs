using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataManagement : MonoBehaviour
{

    const string _userDataFileName = "SaveData.json";

    [MenuItem("DataManagement/Open PersistentDataPath Folder")]
    public static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }

    [MenuItem("DataManagement/Delete PersistentDataPath Data")]
    [SerializeField]
    public static void DeleteStreamingAssetsFolder()
    {
        File.Delete(Application.persistentDataPath + "/" + _userDataFileName);

    }

}
