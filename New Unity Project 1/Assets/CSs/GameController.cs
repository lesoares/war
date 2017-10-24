using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GameController : MonoBehaviour
{
    public static bool isPlayerTurn = true;
    public static List<GameObject> playerTerritories = new List<GameObject>();
    public static List<GameObject> iaTerritories = new List<GameObject>();
    public List<GameObject> Territories = new List<GameObject>();
    public static Texture2D playerColor = null; 

    void Start()
    {
        DistributeTerritories();
    }

    void DistributeTerritories()
    {
        var limit = 47;
        List<int> usedNumbers = new List<int>();
        for (int i = 0; i < 24; i++)
        {
            var number = UnityEngine.Random.Range(0, limit);
            playerTerritories.Add(Territories[number]);
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
}
