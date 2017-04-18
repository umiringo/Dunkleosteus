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
            return false;
        }
        string filePath = "config/" + fileName;
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (file == null) {
            return false;
        }
        var jo = JSON.Parse(file.text) as JSONClass;
        _templatePool[fileName] = jo;
        return true;
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    // Interface                                                                 //
    ///////////////////////////////////////////////////////////////////////////////
    public JSONNode GetTemplateString(string fileName, string name) 
    {
        var levelDictionary = _templatePool[fileName];
        if(levelDictionary == null) {
            return null;
        }
        return levelDictionary[name];
    }

    public JSONArray GetTemplateArray(string fileName, string name)
    {
        var levelDictionary = _templatePool[fileName];
        if(levelDictionary == null) {
            return null;
        }
        return levelDictionary[name] as JSONArray;
    }
}
