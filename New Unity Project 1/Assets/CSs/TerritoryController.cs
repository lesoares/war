using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

    public List<GameObject> neighborhood;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("região " + name + " clicada");
        Debug.Log("região pertence a " + transform.parent.name);
        foreach (var item in neighborhood)
        {
            Debug.Log( item.name +  " pertence a sua vizinhança");
        }
    }
}
