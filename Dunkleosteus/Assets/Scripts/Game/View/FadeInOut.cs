using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {
    private TweenAlpha fadeTween;
    public float duration;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitTween(bool isIn)
    {
        fadeTween = gameObject.GetComponent<TweenAlpha>();
        fadeTween.enabled = true;
        if (isIn) {
            fadeTween.from = 0;
            fadeTween.to = 1;
        }
        else {
            fadeTween.from = 1;
            fadeTween.to = 0;
        }
        fadeTween.duration = duration;
        //fadeTween.onFinished.Add(new EventDelegate(FadeEventDelegate));
    }
    public void FadeEventDelegate()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        InitTween(true);
        fadeTween.ResetToBeginning();
        fadeTween.onFinished.Clear();
        fadeTween.PlayForward();
    }

    public void FadeOut()
    {
        InitTween(false);
        fadeTween.ResetToBeginning();
        fadeTween.onFinished.Clear();
        fadeTween.PlayForward();
        EventDelegate.Add(fadeTween.onFinished, FadeEventDelegate);
    }
}
