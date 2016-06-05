using UnityEngine;
using System.Collections;

public class Shine : MonoBehaviour {
    private int shineIntervalMin;
    private int shineIntervalMax;
	// Use this for initialization
	void Start () {
        shineIntervalMin = 5;
        shineIntervalMax = 20;
	    
        //开启协程
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator DoShine()
    {
        int interval = Random.Range(shineIntervalMin, shineIntervalMax);
        yield return new WaitForSeconds(interval);
        //tween 启动
    }
}
