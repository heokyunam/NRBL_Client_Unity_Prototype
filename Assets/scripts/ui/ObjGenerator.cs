using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjGenerator : MonoBehaviour {
    private bool canGenerate = true;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
        }
	}

    void OnClick(int x, int y)
    {

    }

    void NextTurn()
    {
        canGenerate = true;
    }
}
