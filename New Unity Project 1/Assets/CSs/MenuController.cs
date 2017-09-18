using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public Button play;
	// Use this for initialization
	void Start () {
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartGame  ()
    {
        Debug.Log("Jogar");
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }
}
