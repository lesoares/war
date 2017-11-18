using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    public Color color;
    public int numTurn;

    public GameObject configure(int numTurn, Color color)
    {
        this.numTurn = numTurn;
        this.color = color;
        return this.gameObject;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void EndGame()
    {

    }

    public virtual void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
        Debug.Log("Dist base");
    }
}
