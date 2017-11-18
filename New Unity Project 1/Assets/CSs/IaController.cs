
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
            enemy = (enemy == 0) ? 1 : enemy;
            return (Convert.ToDouble(t.neighborhood.Sum(n => (n.player != numTurn) ? n.getTropas() : 0)) - t.getTropas())
            / Convert.ToDouble(enemy);
        }).First();
        Debug.Log(add);
        for (var i = 0; i < add; i++) {
            var position = territory.PointInArea();
            Debug.Log("Adiciona " + territory.name + " " + position);
            got.CreateTroop(territory, position);
        }
    }
}
