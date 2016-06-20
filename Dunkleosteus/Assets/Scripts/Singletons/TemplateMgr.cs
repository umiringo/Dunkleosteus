using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class TemplateMgr : Singleton<TemplateMgr> {

    protected TemplateMgr() { }

    public string identify = "TemplateMgr";

    private Dictionary< string, Dictionary<string, string> > _templatePool;
	// Use this for initialization
	void Start () 
    {
	    templatePool = new Dictionary< string, Dictionary<string, string> >();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Init()
    {
        this.Load(ConfigFileName.LevelInfo);
    }

    private bool Load(string fileName)
    {
        if(_templatePool.ConstainsKey(fileName)) {
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
        JSONNode jo = JSON.Parse(file.text);
        
        // Fetch the right templateDictionary
        if(_templatePool[fileName] == null){
            _templatePool[fileName] = new Dictionary<string, string>();
        }
        var templateDictionary = _templatePool[fileName];
        // Cycle to read jsonstring
        foreach (string key in jo.Keys) {
            // Insert into Dictionary
            templateDictionary.Add(key, jo[key]);
        }
        return true;
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    // Interface                                                                 //
    ///////////////////////////////////////////////////////////////////////////////
    public string GetTemplateString(string fileName, string levelName) 
    {
        var levelDictionary = _templatePool[fileName]
        if(levelDictionary == null) {
            return "";
        }
        return levelDictionary["levelName"];
        
    }
}
