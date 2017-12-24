using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface CheckListener
{
    void OnCheck();
}
public class CheckDialog : MonoBehaviour
{
    private CheckListener listener;
    // Use this for initialization
    void Start()
    {
        Color color = this.GetComponent<Image>().color;
        color.a = 0.5f;
        this.GetComponent<Image>().color = color;

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetCheckListener(CheckListener listener)
    {
        this.listener = listener;
    }

    public void OnCheck()
    {
        this.listener.OnCheck();
        this.gameObject.SetActive(false);
    }

    public void SetText(string value)
    {
        this.transform.Find("Message").GetComponent<Text>().text = value;
    }
}
