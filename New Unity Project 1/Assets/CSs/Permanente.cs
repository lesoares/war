using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.CSs;
using System;

public class Permanente : MonoBehaviour {

    private static Permanente oi;
    static public int num;
    static public Color color;
    static public int iaCount;
    static public int playerCount;

    private Permanente()
    {

    }

    public static Permanente existe
    {
        get 
        {
            if(oi == null) {
                oi = new Permanente();
            }
            return oi;
        }  
    }

    // Use this for initialization
    void Start() {
    
    }

    // Update is called once per frame
    void Update() {

    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Resete()
    {
        num = 0;
        color = Color.clear;
        iaCount = 3;
        playerCount = 3;
    }
}
