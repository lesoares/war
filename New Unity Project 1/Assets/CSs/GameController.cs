using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameController : MonoBehaviour
{
    public static bool isPlayerTurn = true;
    public static List<TerritoryController> playerTerritories = new List<TerritoryController>();
    public static List<TerritoryController> iaTerritories = new List<TerritoryController>();
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
        var limit = 47;
        List<int> usedNumbers = new List<int>();
        for (int i = 0; i < 24; i++)
        {
            var number = UnityEngine.Random.Range(0, limit);
            playerTerritories.Add(Territories[number]);
            GameObject t = Instantiate(Tropa, Territories[number].transform.position, Quaternion.identity);
            t.transform.SetParent(Territories[number].transform);
            Territories[number].tropasNormais.Add(t);
            Debug.Log("add player " + Territories[number].name);
            Territories.RemoveAt(number);
            usedNumbers.Add(number);
            limit--;
        }

        foreach (var item in Territories)
        {
            Debug.Log("add ia " + item.name);
            iaTerritories.Add(item);
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
        List<TerritoryController> territories = isPlayerTurn ? playerTerritories : iaTerritories;
        if (territories.Count >= 20)
        {
            //modificar objetivos
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


