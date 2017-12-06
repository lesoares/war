using Assets.CSs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vencedor : MonoBehaviour {

    public static int number;
    public static Color color;
    public GameObject Vencedor_1;
    public GameObject Vencedor_2;
    public GameObject Vencedor_3;
    public GameObject Vencedor_4;
    public GameObject Vencedor_5;
    public GameObject Vencedor_6;
    public GameObject Vencedor_7;
    public GameObject Vencedor_8;


    // Use this for initialization
    void Start () {
        if (number == 1) {
            var IASprite = this.Vencedor_1.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 2) {
            var IASprite = this.Vencedor_2.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 3) {
            var IASprite = this.Vencedor_3.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 4) {
            var IASprite = this.Vencedor_4.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 5;
            IASprite.color = IAColor;
        }
        if (number == 5) {
            var IASprite = this.Vencedor_5.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 6) {
            var IASprite = this.Vencedor_6.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 7) {
            var IASprite = this.Vencedor_7.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
        if (number == 8) {
            var IASprite = this.Vencedor_8.GetComponent<SpriteRenderer>();
            var IAColor = color;
            IAColor.a = 1;
            IASprite.color = IAColor;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}
