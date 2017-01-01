using UnityEngine;
using System.Collections;

public class ConfirmView : MonoBehaviour {
    public UILocalize labelTitle;
    public UILocalize labelContent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Show(string title, string content)
    {
        labelTitle.key = title;
        labelContent.key = content;
        gameObject.SetActive(true);
    }
}
