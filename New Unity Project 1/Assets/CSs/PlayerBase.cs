using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBase : MonoBehaviour {

    public Color color;
    public int numTurn;

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

    public virtual void EndGame()
    {

    }

    public virtual void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
        Debug.Log("Dist base");
    }

    public virtual void Attack(GameController got)
    {

    }

    public virtual void ShowAttack(GameController got, TerritoryController source, TerritoryController target, List<int> attackDice, List<int> defenseDice, bool goToConquest)
    {
        timer += Time.deltaTime;
        if (showingAttack) {
            if(timer > 3.5f) {
                if (goToConquest) {
                    got.ConquestSubstate();
                } else {
                    got.AttackSubstate();
                }
                showingAttack = false;
            }
        }else {
            Debug.Log(source.name + " ataca " + target.name);
            Debug.Log("Ataque [" + string.Join(", ", attackDice.Select(x => x.ToString()).ToArray()) + "] / Defesa [" + string.Join(", ", defenseDice.Select(x => x.ToString()).ToArray()) + "]");

            /*
            for (var i = 0; i < Math.Min(attackDice.Count, defenseDice.Count); i++) {
                if (attackDice[i] > defenseDice[i]) {
                    Debug.Log("Ataque " + attackDice[i] + " ganha de defesa " + defenseDice[i]);
                } else {
                    Debug.Log("Defesa " + defenseDice[i] + " ganha de ataque " + attackDice[i]);
                }
            }
            */
            timer = 0.0f;
            showingAttack = true;
        }
        
    }

    public virtual void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {
   
    }
}
