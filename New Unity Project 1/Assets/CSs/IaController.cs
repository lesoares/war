
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

    public void Play(GameController got)
    {
        var add = Math.Max(3, Math.Round(got.Territories.Sum(t => (t.player == 0) ? 0.5 : 0.0)));
        var territory = got.Territories.OrderBy(t =>
        {
            if (t.player == 1) return 1000000000;
            var enemy = t.neighborhood.Sum(n => (n.player == 1) ? 1 : 0);
            enemy = (enemy == 0) ? 1 : enemy;
            return (Convert.ToDouble(t.neighborhood.Sum(n => (n.player == 1) ? n.getTropas() : 0)) - t.getTropas())
            / Convert.ToDouble(enemy);
        }).First();
        for (var i = 0; i < add; i++)
        {
            var position = territory.PointInArea();
            Debug.Log("Adiciona " + territory.name + " " + position);
            territory.CreateTroop(position);
        }
        got.EndTurn();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
    }

    public int IaDistribuicao(List<TerritoryController> territorios)
    {
        return 2;
    }

}
