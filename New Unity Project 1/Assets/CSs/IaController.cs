
using Assets.CSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IaController : PlayerBase
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    public override void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
        foreach (KeyValuePair<GameObject, int> entry in exercitos)
        {
            if (entry.Value - exercitosAdd[entry.Key] < 1) {
                continue;
            }
            List<TerritoryController> territories;
            bool isContinent = false;
            if (System.Object.ReferenceEquals(entry.Key, got.gameObject)) {
                territories = got.Territories;
            } else {
                territories = new List<TerritoryController>();
                for( var i = 0; i < entry.Key.transform.childCount; i++) {
                    territories.Add(entry.Key.transform.GetChild(i).GetComponent<TerritoryController>());
                }
                isContinent = true;
            }
            DistributeIA(got, territories, entry.Value - exercitosAdd[entry.Key], isContinent);
        }
        got.AttackState();
    }

    void DistributeIA(GameController got, List<TerritoryController> territories, int add, bool isContinent)
    {
        var territory = territories.OrderBy(t => {
            if (t.player != this.numTurn) return 1000000000;
            var enemy = t.neighborhood.Sum(n => (n.player != this.numTurn) ? 1 : 0);
            if (enemy == 0) return 1000000;
            var result = (Convert.ToDouble(t.neighborhood.Sum(n => (n.player != numTurn) ? n.getTropas() : 0)) - t.getTropas()) / Convert.ToDouble(enemy)
            - Convert.ToDouble(enemy);
            return result;
        }).First();
        for (var i = 0; i < add; i++) {
            var position = territory.PointInArea();
            got.CreateTroop(territory, position);
        }
    }

    public override void Attack(GameController got)
    {
        Attack choose = null;
        foreach(TerritoryController t in got.Territories) {
            if(t.player != this.numTurn) {
                continue;
            }
            foreach(TerritoryController n in t.neighborhood) {
                if (n.player == this.numTurn) {
                    continue;
                }
                if (t.getTropas() <= n.getTropas()) {
                    continue;
                }
                Attack novo = new Attack(t, n, t.getTropas() - n.getTropas());
                Debug.Log("A " + t.name + " " + n.name + " " + novo.pont);

                if ( choose == null || choose.pont < novo.pont) {
                    choose = novo;
                }
            }
        }
        if(choose != null) {
            got.Attack(choose.source, choose.target);
        } else {
            got.RedistributeState();
        }
    }

    public override void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {
        if (got.MoveConquest(Math.Min(3, source.getTropas() - 1))) {
            got.AttackSubstate();
        }

    }
}


class Attack
{
    public TerritoryController source;
    public TerritoryController target;
    public int pont;

    public Attack (TerritoryController source, TerritoryController target, int pont)
    {
        this.source = source;
        this.target = target;
        this.pont = pont;
    }
}