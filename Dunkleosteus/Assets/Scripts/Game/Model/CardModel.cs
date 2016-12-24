using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour {
    public GameObject cardPreviewGameObject;
    public CardPreview cardPreview;
    public GameObject cardListGameObject;

    private bool isClose = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowCardPreview(string level)
    {
        isClose = false;
        cardPreview.Init(level);
        cardPreviewGameObject.SetActive(true);
        cardPreviewGameObject.GetComponent<TweenPosition>().PlayForward();
    }

    public void CloseCardPreview()
    {
        cardListGameObject.SetActive(true);
        isClose = true;
        cardPreviewGameObject.GetComponent<TweenPosition>().PlayReverse();
    }

    public void TriggerCardPreview(GameObject go)
    {
        cardPreview.FlopCard(go);
    }

    public void OnFinishTween()
    {
        if (isClose) {
            cardPreview.ClearPreview();
            cardPreviewGameObject.SetActive(false);
        } else {
            cardListGameObject.SetActive(false);
            //cardPreview.Shake();
        }
    }
}
