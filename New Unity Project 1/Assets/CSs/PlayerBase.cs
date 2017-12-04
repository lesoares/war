using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    float DELAY = 0.5f;

    public Color color;
    public int numTurn;
    public bool clickDistribution = false;
    public bool clickAttack = false;
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
            /*
            Debug.Log(source.name + " ataca " + target.name);
            Debug.Log("Ataque [" + string.Join(", ", attackDice.Select(x => x.ToString()).ToArray()) + "] / Defesa [" + string.Join(", ", defenseDice.Select(x => x.ToString()).ToArray()) + "]");

            
            for (var i = 0; i < Math.Min(attackDice.Count, defenseDice.Count); i++) {
                if (attackDice[i] > defenseDice[i]) {
                    Debug.Log("Ataque " + attackDice[i] + " ganha de defesa " + defenseDice[i]);
                } else {
                    Debug.Log("Defesa " + defenseDice[i] + " ganha de ataque " + attackDice[i]);
                }
            }
            */
            // ToDo: Animacao de Dados

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
}
