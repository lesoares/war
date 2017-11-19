using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PlayerBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
    }

    public override void Attack(GameController got)
    {

    }

    public override void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {

    }

    public override void Redistribute(GameController got, Dictionary<TerritoryController, int> redistributed)
    {

    }

    public override void EndGame()
    {
        SceneManager.LoadScene(3);
    }

}
