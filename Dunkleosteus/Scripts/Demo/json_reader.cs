using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class json_reader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TextAsset file = Resources.Load<TextAsset>("test");
        if(file == null)
        {
            Debug.Log("We are in big trouble !");
            return;
        }
        JSONNode jo = JSON.Parse(file.text);
        UILabel lbl = GetComponent<UILabel>();
        lbl.text = jo["name"];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
