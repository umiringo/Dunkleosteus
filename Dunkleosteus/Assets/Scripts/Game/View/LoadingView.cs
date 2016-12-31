using UnityEngine;
using System.Collections;

public class LoadingView : MonoBehaviour {
    public TweenRotation loadingTween;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator LoadingOverTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        loadingTween.ResetToBeginning();
        loadingTween.enabled = true;
        loadingTween.PlayForward();
        loadingTween.duration = 3;
        StartCoroutine(LoadingOverTime(10));
    }
}
