﻿using System.Collections;
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

        Button btn = this.transform.Find("YesButton").GetComponent<Button>();
        btn.onClick.AddListener(OnCheck);
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
        this.gameObject.SetActive(false);
        this.listener.OnCheck();
    }

    public void SetText(string value)
    {
        this.transform.Find("Message").GetComponent<Text>().text = value;
    }

    public void View(string text, CheckListener listener)
    {
        SetCheckListener(listener);
        gameObject.SetActive(true);
        SetText(text);
    }
}
