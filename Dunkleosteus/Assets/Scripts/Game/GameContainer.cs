using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {

	// Use this for initialization
    public GameObject starGameObject;
    public GameObject detailGameObject;
    public float duration = 1.0f;
    private GameObject triggerGameObject;
    private TweenRotation starTween;
    private TweenRotation detailTween;
	void Start () 
    {
        triggerGameObject = null;

        detailGameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
        starTween = starGameObject.GetComponent<TweenRotation>();
        if (starTween == null) {
            starTween = starGameObject.AddComponent<TweenRotation>();
        }
        detailTween = detailGameObject.GetComponent<TweenRotation>();
        if (detailTween == null) {
            detailTween = detailGameObject.AddComponent<TweenRotation>();
        }

        starTween.enabled = false;
        detailTween.enabled = false;

        starTween.from = Vector3.zero;
        starTween.to = new Vector3(0, 90, 0);
        starTween.duration = duration;

        detailTween.from = new Vector3(0, 90, 0);
        detailTween.to = Vector3.zero;
        detailTween.duration = duration;

        starTween.onFinished.Add(new EventDelegate(StarTweenEventDelegate));
        detailTween.onFinished.Add(new EventDelegate(DetailTweenEventDelegate));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void DoGameWin()
    {
    }

    public void OnCardTouched()
    {

    }

    public StarTweenEventDelegate()
    {

    }

    public DetailTweenEventDelegate()
    {

    }
}
