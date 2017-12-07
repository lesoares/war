using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CSs;

public class Permanente : MonoBehaviour {

    static public int num;
    static public Color color;
    static public int iaCount = 1;
    static public int playerCount = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
