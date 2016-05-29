using UnityEngine;
using System.Collections;
using GlobalDefines;

public class Star : MonoBehaviour {

    public GameObject starChild;
    public GameObject shineChild;
    public int index;
    public int state;
	// Use this for initialization
	void Start () {
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetStatus(int s)
    {
        state = s;
        // TODO
        // 修改child的sprite
    }
}
