using UnityEngine;
using System.Collections;

public class TitleStar : MonoBehaviour {
    private Shine shine;

	// Use this for initialization
	void Start () {
        shine = gameObject.transform.Find("SpriteShine").gameObject.GetComponent<Shine>();
        shine.StartShine();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
