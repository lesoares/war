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
        var limit = 47;
        List<int> usedNumbers = new List<int>();
        for (int i = 0; i < 24; i++)
        {
            var number = UnityEngine.Random.Range(0, limit);
            Territories[number].isPlayerDono = true;
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
            item.isPlayerDono = false;
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
            if (t.isPlayerDono)
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


