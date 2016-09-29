using UnityEngine;
using System.Collections;

public class Shine : MonoBehaviour {
    private int shineIntervalMin;
    private int shineIntervalMax;
    private UIPlayTween playTween;
	// Use this for initialization
	void Start () 
    {
        shineIntervalMin = 5;
        shineIntervalMax = 20;
        playTween = GetComponent<UIPlayTween>();
        playTween.resetOnPlay = true;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    
    public void StartShine()
    {
        StopAllCoroutines(); //stop shine first
        StartCoroutine(DoShine());
    }

    IEnumerator DoShine()
    {
        int interval = Random.Range(shineIntervalMin, shineIntervalMax);
        yield return new WaitForSeconds(interval);
        //tween 启动
        playTween.Play(true);
        //start another shine
        StartCoroutine(DoShine());
    }

    public void StopShine()
    {
        StopAllCoroutines();
    }
}
