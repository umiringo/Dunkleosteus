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
        shineIntervalMax = 10;
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
        Debug.Log("Start Shine, Shine.DoShine:interval = " + interval.ToString());
        yield return new WaitForSeconds(interval);
        //tween 启动
        playTween.Play(true);
        //start another shine
        StartCoroutine(DoShine());
    }

    public void StopShine()
    {
        Debug.Log("Stop Shine !");
        StopAllCoroutines();
    }
}
