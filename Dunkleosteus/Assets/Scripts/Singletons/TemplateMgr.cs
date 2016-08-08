using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class TemplateMgr : Singleton<TemplateMgr> {

    protected TemplateMgr() { }

    public string identify = "TemplateMgr";

    private Dictionary< string, JSONClass > _templatePool;
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Init()
    {
        _templatePool = new Dictionary<string, JSONClass>();
        this.Load(ConfigKey.LevelInfo);
    }

    private bool Load(string fileName)
    {
        if (_templatePool.ContainsKey(fileName)) {
            // Already loaded, log and return
            Debug.Log("TemplateMgr.Load : Template file " + fileName + " already loaded! ");
            return false;
        }
        string filePath = "config/" + fileName;
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (file == null) {
            Debug.Log("TemplateMgr.Load Failed! Cant find file : " + filePath);
            return false;
        }
        var jo = JSON.Parse(file.text) as JSONClass;
        _templatePool[fileName] = jo;
        Debug.Log("TemplateMgr Load fileName = " + fileName + " Success!");
        return true;
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    // Interface                                                                 //
    ///////////////////////////////////////////////////////////////////////////////
    public JSONNode GetTemplateString(string fileName, string levelName) 
    {
        var levelDictionary = _templatePool[fileName];
        if(levelDictionary == null) {
            Debug.LogError("TemplateMgr.GetTemplateString Error! Cant find fileName = " + fileName);
            return null;
        }
        return levelDictionary[levelName];
    }

    public JSONArray GetTemplateArray(string fileName, string levelName)
    {
        var levelDictionary = _templatePool[fileName];
        if(levelDictionary == null) {
            Debug.LogError("TemplateMgr.GetTemplateString Error! Cant find fileName = " + fileName);
            return null;
        }
        return levelDictionary[levelName] as JSONArray;
    }
}
