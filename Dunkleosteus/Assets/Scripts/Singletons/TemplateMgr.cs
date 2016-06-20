using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class TemplateMgr : Singleton<TemplateMgr> {

    protected TemplateMgr() { }

    public string identify = "TemplateMgr";

    private Dictionary<string, string> templatePool;
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
        this.Load("config/level_info");
    }

    private bool Load(string filePath)
    {
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (file == null) {
            Debug.Log("TemplateMgr.Load Failed! Cant find file : " + filePath);
            return false;
        }
        JSONNode jo = JSON.Parse(file.text);
        Debug.Log(jo["Scorpius"]);
        return true;
    }
}
