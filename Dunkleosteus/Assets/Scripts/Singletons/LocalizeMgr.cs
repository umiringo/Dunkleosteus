using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class LocalizeMgr : Singleton<LocalizeMgr> {
    private JSONClass _localizeJson;

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
        this.Load(ConfigKey.LocalizeJson);
    }

    private bool Load(string fileName)
    {
        string filePath = "config/" + fileName;
        TextAsset file = Resources.Load<TextAsset>(filePath);
        if (file == null) {
            return false;
        }
        var jo = JSON.Parse(file.text) as JSONClass;
        _localizeJson = jo;
        return true;
    }
    
    ///////////////////////////////////////////////////////////////////////////////
    // Interface                                                                 //
    ///////////////////////////////////////////////////////////////////////////////
    public string GetLocalizeStr(string key)
    {
        var localizeDictionary = _localizeJson[key];
        if(localizeDictionary == null) return "not found";
        string lang = Localization.language;
        string langKey = "English";
        switch(lang) {
            case "English":
                langKey = "English";
                break;
            case "Japanese":
                langKey = "Japanese";
                break;
            case "SChinese":
                langKey = "SChinese";
                break;
            case "TChinese":
                langKey = "TChinese";
                break;
            default:
                break;
        }
        return localizeDictionary[langKey];
    }
}
