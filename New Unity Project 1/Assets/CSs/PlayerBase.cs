using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    public Color color;

    public GameObject configure(Color color)
    {
        this.color = color;
        return this.gameObject;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play(GameController got)
    {

    }

    public void EndGame()
    {

    }
}
