using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using SimpleJSON;

public class BatchTool : MonoBehaviour
{
    [MenuItem("Assets/批处理工具/打开字体选择面板", false, 0)]
    public static void OpenConnectAtlasPanel()
    {
        EditorWindow.GetWindow<BatchFontWindow>(false, "ConnectAtlasPanel", true);
    }
    
    [MenuItem("Assets/批处理工具/重新指定字体", false, 0)]
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

    [MenuItem("BatchTool/生成卡牌预览的Prefab")]
    public static void GenerateCardPreviewPrefab()
    {
        string cardPath = "Assets/Resources/Prefabs/CardView/CardPreview";
        string constellationPath = "Assets/Resources/Prefabs/Constellation";
        // 删除掉Prefabs/CardView/CardPreview下所有文件
        /*
        foreach(string d in Directory.GetFileSystemEntries(cardPath)) {
            if(File.Exists(d)) {
                Debug.Log("d = " + d);
                File.Delete(d);
            }
        }
        */

        // 遍历Prefabs/Constellation生成card的prefab
        foreach(string d in Directory.GetFileSystemEntries(constellationPath, "*.prefab")) {

            if(File.Exists(d)) {
                string fileName = Path.GetFileName(d);
                string newName = fileName.Replace("Container", "Card");
                string newPath = cardPath + '/' + newName;

                Object obj = AssetDatabase.LoadAssetAtPath(d, typeof(GameObject));
                GameObject clone = GameObject.Instantiate(obj) as GameObject;
                // 修改
                ModifyContainerToCard(clone);
                // 写入
                GameObject targetObj = AssetDatabase.LoadAssetAtPath(newPath, typeof(GameObject)) as GameObject;
                if (targetObj != null) {
                    PrefabUtility.ReplacePrefab(clone, targetObj);
                }
                else {
                    PrefabUtility.CreatePrefab(newPath, clone);   
                }
               // DestroyImmediate(targetObj);
                DestroyImmediate(clone);
                AssetDatabase.ImportAsset(newPath);
                AssetImporter importer = AssetImporter.GetAtPath(newPath);
                importer.SaveAndReimport();
            }
        }
        
        AssetDatabase.SaveAssets();
    }

    private static void ModifyContainerToCard(GameObject containerObj)
    {
        // 获取所有的孩子object
        GameObject skyGameObject = containerObj.transform.Find("Sky").gameObject;
        GameObject detailGameObject = containerObj.transform.Find("Detail").gameObject;
        GameObject starContainer = containerObj.transform.Find("Sky/StarContainer").gameObject;
        GameObject lineContainer = containerObj.transform.Find("Sky/LineContainer").gameObject;

        // 删除container上的组件
        DestroyImmediate(containerObj.GetComponent<GameContainer>());
        DestroyImmediate(containerObj.GetComponent<UIPlayTween>());
        DestroyImmediate(containerObj.GetComponent<TweenScale>());
        // 添加container于组件
        var cardComp = containerObj.AddComponent<Card>();
        string levelName = containerObj.name.Replace("Container", "").Replace("(Clone)", "");
        Debug.Log("levelName = " + levelName);
        cardComp.skyGameObject = skyGameObject;
        cardComp.detailGameObject = detailGameObject;
        cardComp.starContainer = starContainer;
        cardComp.lineContainer = lineContainer;
        cardComp.levelName = levelName;

        // 删除sky的组件
        DestroyImmediate(skyGameObject.GetComponent<Sky>());
        // 添加sky的组件
        skyGameObject.AddComponent<CardFront>();

        // 删除detail的组件
        DestroyImmediate(detailGameObject.GetComponent<StarDetail>());
        // 添加detail的组件
        detailGameObject.AddComponent<CardBack>();

        // 循环修改star
        foreach (Transform child in starContainer.transform) {
            GameObject childGo = child.gameObject;
            // 删除star的组件
            DestroyImmediate(childGo.GetComponent<BoxCollider>());
            DestroyImmediate(childGo.GetComponent<Star>());
            DestroyImmediate(childGo.GetComponent<UIEventTrigger>());
            // 删除shine的GameObject
            GameObject shine = childGo.transform.Find("Sprite_Shine").gameObject;
            DestroyImmediate(shine);
            // 修改star的spriteName
            GameObject star = childGo.transform.Find("Sprite_Star").gameObject;
            if (star.GetComponent<UISprite>().spriteName == "circle_30_30_50cce5") {
                star.GetComponent<UISprite>().spriteName = "circle_30_30_f8b711";
            }
            else if (star.GetComponent<UISprite>().spriteName == "circle_40_40_50cce5") {
                star.GetComponent<UISprite>().spriteName = "circle_30_30_f8b711";
            }
            else if (star.GetComponent<UISprite>().spriteName == "circle_50_50_50cce5") {
                star.GetComponent<UISprite>().spriteName = "circle_40_40_f8b711";
            }
            else if (star.GetComponent<UISprite>().spriteName == "circle_60_60_50cce5") {
                star.GetComponent<UISprite>().spriteName = "circle_40_40_f8b711";
            }
        }

    }
}