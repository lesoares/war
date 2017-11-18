
using Assets.CSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<TerritoryController> Territories = new List<TerritoryController>();
    public static Texture2D playerColor = null;
    public GameObject Tropa;
    public GameObject Player;
    public GameObject IA;
    public static List<GameObject> players = new List<GameObject>();
    public static int turn = 0;



    void Start()
    {
        players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(new Color(0, 0, 1)));
        players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(new Color(1, 0, 0)));
        //players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(new Color(1, 1, 0)));

        DistributeTerritories();
        turn = UnityEngine.Random.Range(0, players.Count - 1);
    }

    void DistributeTerritories()
    {
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
            territory.player = i % players.Count;
            GameObject t = Instantiate(Tropa, territory.transform.position, Quaternion.identity);
            t.GetComponent<TropaController>().configure(territory);
            territory.tropasNormais.Add(t);
        }
       
    }


    void Update()
    {
        players[turn].GetComponent<PlayerBase>().Play(this);
    }

    public void EndTurn()
    {
        if (checkObjective())
        {
            players[turn].GetComponent<PlayerBase>().EndGame();
        }
        turn = (turn + 1) % players.Count;
    }


    bool checkObjective()
    {
        int contaTerritorio = 0;
        foreach (var t in Territories)
        {
            if (t.player == turn)
            {
                contaTerritorio++;
            }
        }
        if ((contaTerritorio > 25) || (contaTerritorio <= 23)) 
        {
            return true;
        }
        return false;
    }


    
}


