using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void roll(int number)
    {
        Debug.Log(number);
        if (number == 1) {
            this.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        } else if (number == 2) {
            this.gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
        } else if (number == 3) {
            this.gameObject.transform.rotation = Quaternion.Euler(90, 90, 0);
        } else if (number == 4) {
            this.gameObject.transform.rotation = Quaternion.Euler(90, 270, 0);
        } else if (number == 5) {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if (number == 6) {
            this.gameObject.transform.rotation = Quaternion.Euler(270, 0, 0);
        }
    }
}
