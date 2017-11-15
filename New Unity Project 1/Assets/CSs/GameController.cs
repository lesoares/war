
using Assets.CSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static bool isPlayerTurn = true;
    public List<TerritoryController> Territories = new List<TerritoryController>();
    public static Texture2D playerColor = null;
    public GameObject Tropa;

    void Start()
    {
        DistributeTerritories();
        if(UnityEngine.Random.Range(0,1) == 0)
        {
            isPlayerTurn = true;
        }
        else
        {
            isPlayerTurn = false;
        }
        isPlayerTurn = false;
    }

    void DistributeTerritories()
    {
        var players = 2;
        var range = Enumerable.Range(0, Territories.Count).ToList();
        for(int i = 0; i < range.Count; i++)
        {
            var temp = range[i];
            var randomIndex = UnityEngine.Random.Range(i, range.Count);
            range[i] = range[randomIndex];
            range[randomIndex] = temp;
        }
        for (int i = 0; i < range.Count; i++)
        {
            var territory = Territories[range[i]];
            territory.player = i % players;
            GameObject t = Instantiate(Tropa, territory.transform.position, Quaternion.identity);
            t.GetComponent<TropaController>().configure(territory);
            territory.tropasNormais.Add(t);
            if (territory.player == 1)
            {
                Debug.Log("Add player: " + territory.name);
            } else
            {
                Debug.Log("Add IA: " + territory.name);
            }
        }
       
    }

    void Update()
    {
        if (isPlayerTurn)
        {
        } else
        {
            var add = Math.Max(3, Math.Round(Territories.Sum(t => (t.player == 0) ? 0.5 : 0.0)));
            var territory = Territories.OrderBy(t =>
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
            isPlayerTurn = true;
        }
    }

   void EndTurn()
    {
        if (checkObjective())
        {
            EndGame();
        }
        
        isPlayerTurn = !isPlayerTurn;
    }


    bool checkObjective()
    {
        int contaTerritorio = 0;
        foreach (var t in Territories)
        {
            if (t.player == 1)
            {
                contaTerritorio++;
            }
        }
        if ((isPlayerTurn && contaTerritorio > 25) || (isPlayerTurn && contaTerritorio <= 23)) 
        {
            return true;
        }
        return false;
    }

    void EndGame()
    {
        if (isPlayerTurn)
            SceneManager.LoadScene(3);
        else
            SceneManager.LoadScene(2);
          
    }
    
}


