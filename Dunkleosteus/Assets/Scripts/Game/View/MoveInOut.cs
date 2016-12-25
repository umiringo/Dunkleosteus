using UnityEngine;
using System.Collections;

public class MoveInOut : MonoBehaviour {
    private TweenPosition moveTween;
    public float duration;
    public float offsetX;
    public float offsetY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitTween(bool isIn)
    {
        moveTween = gameObject.GetComponent<TweenPosition>();
        moveTween.enabled = true;
        if (isIn) {
            moveTween.from = new Vector3(offsetX, offsetY, 0);
            moveTween.to = Vector3.zero;
        }
        else {
            moveTween.from = Vector3.zero;
            moveTween.to = new Vector3(offsetX, offsetY, 0);
        }
        moveTween.duration = duration;
    }
    public void moveEventDelegate()
    {
        gameObject.SetActive(false);
    }

    public void MoveIn()
    {
        gameObject.SetActive(true);
        InitTween(true);
        moveTween.ResetToBeginning();
        moveTween.onFinished.Clear();
        moveTween.PlayForward();
    }

    public void MoveOut()
    {
        InitTween(false);
        moveTween.ResetToBeginning();
        moveTween.onFinished.Clear();
        moveTween.PlayForward();
        EventDelegate.Add(moveTween.onFinished, moveEventDelegate);
    }
}
