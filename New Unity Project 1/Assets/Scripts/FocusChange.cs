using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusChange : MonoBehaviour {

    public GameObject camera;
    public GameObject globalView;
    public GameObject focusedView;
    bool changeView = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseClick()
    {
        Debug.Log(changeView);
        if (changeView)
        {
            camera.transform.position = globalView.transform.position;
            changeView = false;
        }
        else
        {
            camera.transform.position = focusedView.transform.position;
            changeView = true;  
        }
        
    }
}
