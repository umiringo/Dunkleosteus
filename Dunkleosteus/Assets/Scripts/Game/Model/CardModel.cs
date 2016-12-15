using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour {
    public GameObject cardPreviewGameObject;
    public CardPreview cardPreview;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowCardPreview(string level)
    {
        Debug.Log("ShowCardPreview level = " + level);
        cardPreview.Init(level);
        cardPreviewGameObject.SetActive(true);
    }

    public void CloseCardPreview()
    {
        cardPreviewGameObject.SetActive(false);
    }
}
