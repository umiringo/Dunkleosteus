using UnityEngine;
using System.Collections;

public class GuideView : MonoBehaviour {
    public GameObject spriteTap;
    public GameObject spriteTarget;
    public UILocalize labelGuide;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowTap(GameObject star)
    {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        Vector3 starPosition = star.transform.localPosition;
        spriteTap.transform.localPosition = new Vector3(starPosition.x + 20, starPosition.y - 65, starPosition.z);
        spriteTarget.transform.localPosition = new Vector3(starPosition.x, starPosition.y, starPosition.z);
    }
}
