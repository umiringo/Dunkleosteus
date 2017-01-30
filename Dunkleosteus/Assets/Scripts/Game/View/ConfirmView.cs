using UnityEngine;
using System.Collections;

public class ConfirmView : MonoBehaviour {
    public UILocalize labelTitle;
    public UILocalize labelContent;
    public GameObject buttonOk;
    public UILabel labelNum;
    public UILabel labelPrice;
	public GameObject gameObjectCoin;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowPay(string title, string content, string delegateName, string param, string num, string price)
    {
        labelTitle.key = title;
        labelContent.key = content;

        GameObject controller = GameObject.Find("UI Root/GameController");
        UIButton btn = buttonOk.GetComponent<UIButton>();
        EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), delegateName);
        eventDel.parameters[0] = new EventDelegate.Parameter(param);
        btn.onClick.Clear();
        btn.onClick.Add(eventDel);
        gameObject.SetActive(true);

		labelNum.gameObject.SetActive(true);
		labelPrice.gameObject.SetActive(true);
		gameObjectCoin.SetActive(true);
        labelNum.text = " X " + num + " =";
        labelPrice.text = price;
    }

	public void Show(string title, string content, string delegateName, string param)
	{
		labelNum.gameObject.SetActive(false);
		labelPrice.gameObject.SetActive(false);
		gameObjectCoin.SetActive(false);

		labelTitle.key = title;
		labelContent.key = content;

		GameObject controller = GameObject.Find("UI Root/GameController");
		UIButton btn = buttonOk.GetComponent<UIButton>();
		EventDelegate eventDel = new EventDelegate(controller.GetComponent<EventController>(), delegateName);
		eventDel.parameters[0] = new EventDelegate.Parameter(param);
		btn.onClick.Clear();
		btn.onClick.Add(eventDel);
		gameObject.SetActive(true);
	}
}
