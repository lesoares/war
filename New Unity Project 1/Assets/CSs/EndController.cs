﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void backToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        //vitória e derrota?
        SceneManager.LoadScene(2);
    }
}
