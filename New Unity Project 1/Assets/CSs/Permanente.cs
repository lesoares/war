using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CSs;

public class Permanente : MonoBehaviour {

    static public int num;
    static public Color color;

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
