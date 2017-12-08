using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    float DELAY = 3.5f;

    public Color color;
    public int numTurn;
    public bool clickDistribution = false;
    public bool clickAttack = false;
    public bool clickRedistribution = false;
    public bool showNext = false;

    public TerritoryController selectedTerritory = null;
    public TerritoryController otherTerritory = null;

    private float timer = 0.0f;
    private bool showingAttack = false;


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

    public virtual string Text()
    {
        return "Base";
    }


    public virtual void EndGame()
    {

    }

    public virtual void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
    }

    public virtual void Attack(GameController got)
    {

    }

    public virtual void ShowAttack(GameController got, TerritoryController source, TerritoryController target, List<int> attackDice, List<int> defenseDice, bool goToConquest)
    {
        clickAttack = false;
        showNext = false;

        timer += Time.deltaTime;
        if (showingAttack) {
            if(timer > DELAY) {
                if (goToConquest) {
                    got.ConquestSubstate();
                } else {
                    got.AttackSubstate();
                }
                showingAttack = false;
            }
        }else {
            timer = 0.0f;
            showingAttack = true;
        }
        
    }

    public virtual void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {
   
    }

    public virtual void Redistribute(GameController got, Dictionary<TerritoryController, int> redistributed)
    {
        
    }

    public virtual void RedistributeSelected(GameController got, Dictionary<TerritoryController, int> redistributed)
    {

    }

}
