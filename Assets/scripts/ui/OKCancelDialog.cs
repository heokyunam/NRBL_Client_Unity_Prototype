using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface DialogListener
{
    void OnOK();
    void OnCancel();
}
public class OKCancelDialog : MonoBehaviour {
    private DialogListener oklistener;
	// Use this for initialization
	void Start () {
        Color color = this.GetComponent<Image>().color;
        color.a = 0.5f;
        this.GetComponent<Image>().color = color;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDialogListener(DialogListener listener)
    {
        this.oklistener = listener;
    }

    public void OnOK()
    {
        this.oklistener.OnOK();
        this.gameObject.SetActive(false);
    }

    public void OnCancel()
    {
        this.oklistener.OnCancel();
        this.gameObject.SetActive(false);
    }
}
