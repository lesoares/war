﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : PlayerBase {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override string Text()
    {
        return "Jogador";
    }

    public override void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
        string msg = "Distribuir\n";
        foreach (KeyValuePair<GameObject, int> entry in exercitos) {
            if (entry.Value - exercitosAdd[entry.Key] < 1) {
                continue;
            }
            msg += "- " + entry.Key.name + ": " + (entry.Value - exercitosAdd[entry.Key]) + "\n";
        }
        Text text = got.log.GetComponent<Text>();
        text.text = msg;
        text.color = this.color;
        clickDistribution = true;
    }

    public override void Attack(GameController got)
    {

        got.log.GetComponent<Text>().text = "Atacar";
        clickDistribution = false;
        clickAttack = true;

    }

    public override void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {
        clickDistribution = false;
        clickAttack = false;
        got.slider.SetActive(true);
        var slider = got.slider.GetComponent<Slider>();
        slider.minValue = 1;
        slider.maxValue = Math.Min(3, source.getTropas() - 1);
        got.log.GetComponent<Text>().text = "Mover " + slider.value;

    }

    public override void Redistribute(GameController got, Dictionary<TerritoryController, int> redistributed)
    {
        got.log.GetComponent<Text>().text = "Remanejar";
        clickDistribution = false;
    }

    public override void EndGame()
    {
        SceneManager.LoadScene(3);
    }

}
