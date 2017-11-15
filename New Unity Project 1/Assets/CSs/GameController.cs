
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


