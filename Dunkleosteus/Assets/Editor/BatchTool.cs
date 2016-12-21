using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using SimpleJSON;

public class BatchTool 
{
    [MenuItem("Assets/批处理工具/打开字体选择面板", false, 0)]
    static public void OpenConnectAtlasPanel()
    {
        EditorWindow.GetWindow<BatchFontWindow>(false, "ConnectAtlasPanel", true);
    }
    
    [MenuItem("Assets/批处理工具/重新指定字体", false, 2)]
    public static void CorrectionPublicFontFunction()
    {
        if (NGUISettings.ambigiousFont == null)
        {
            Debug.LogError("对不起！你没有指定字体！");
        }
        else
        {
            CorrectionPublicFont(NGUISettings.ambigiousFont as Font,null);
        }
    }

    private static void SaveDealFinishPrefab(GameObject go, string path)
    {
        if (File.Exists(path) == true)
        {
            Object prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
            PrefabUtility.ReplacePrefab(go, prefab);
        }
        else
        {
            PrefabUtility.CreatePrefab(path, go);
        }
    }

    private static void CorrectionPublicFont(Font replace, Font matching)
    {
        if (NGUISettings.ambigiousFont == null)
        {
            Debug.LogError("Select Font Is Null...");
            return;
        }
        else
        {
            Object[] selectObjs = Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
            foreach (Object selectObj in selectObjs)
            {
                GameObject obj = (GameObject)selectObj;
                if (obj == null || selectObj == null)
                {
                    Debug.LogWarning("ERROR:Obj Is Null !!!");
                    continue;
                }
                string path = AssetDatabase.GetAssetPath(selectObj);
                if (path.Length < 1 || path.EndsWith(".prefab") == false)
                {
                    Debug.LogWarning("ERROR:Folder=" + path);
                }
                else
                {
                    Debug.Log("Selected Folder=" + path);
                    GameObject clone = GameObject.Instantiate(obj) as GameObject;
                    UILabel[] labels = clone.GetComponentsInChildren<UILabel>(true);
                    int count = 0;
                    foreach (UILabel label in labels)
                    {
                        count++;
                        label.trueTypeFont = replace;
                    }
                    Debug.Log("CorrectionPublicFont count = " + count);
                    SaveDealFinishPrefab(clone, path);
                    GameObject.DestroyImmediate(clone);
                    Debug.Log("Connect Font Success=" + path);
                }
            }
            AssetDatabase.Refresh();
        }
    }
}