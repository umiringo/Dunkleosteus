using UnityEngine;
using System.Collections;

public class NotifyView : MonoBehaviour {
    public GameObject labelTitle;
    public GameObject labelContent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(string title, string content) 
    {
        if(title == "") {
            labelTitle.SetActive(false);
        } else {
            labelTitle.SetActive(true);
            labelTitle.GetComponent<UILocalize>().key = title;
        }

        labelContent.GetComponent<UILocalize>().key = content;
    }
}
