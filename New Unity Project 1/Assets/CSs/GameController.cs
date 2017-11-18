
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
    public static int state = 0;
    public Dictionary<GameObject, int> exercitos;
    public Dictionary<GameObject, int> exercitosAdd;



    void Start()
    {
        players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(0, new Color(0, 0, 1)));
        players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(1, new Color(1, 0, 0)));
        //players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(new Color(1, 1, 0)));

        DistributeTerritories();
        turn = UnityEngine.Random.Range(0, players.Count - 1);
        turn = 0;
        StartTurn();
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

    void StartTurn()
    {
        exercitos = new Dictionary<GameObject, int>();
        exercitosAdd = new Dictionary<GameObject, int>();
        int world = Convert.ToInt32(Math.Max(3, Math.Round(Territories.Sum(t => (t.player == turn) ? 0.5 : 0.0))));
        exercitos[this.gameObject] = world;
        exercitosAdd[this.gameObject] = 0;
        state = 0;
    }

    public bool AttackState()
    {
        foreach (KeyValuePair<GameObject, int> entry in exercitos) {
            if (entry.Value - exercitosAdd[entry.Key] > 0) {
                return false;
            }
        }
        state = 1;
        return true;
    }

    void Update()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        Debug.Log(turn + " " + state + " " + player.numTurn);

        if (state == 0){
            player.Distribute(this, exercitos, exercitosAdd);
        }else if (state == 1){
            //player.Attack(this);
        }else if (state == 2){

        }
    }

    public void EndTurn()
    {
        if (checkObjective())
        {
            players[turn].GetComponent<PlayerBase>().EndGame();
        }
        turn = (turn + 1) % players.Count;
        StartTurn();
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

    public void CreateTroop(TerritoryController territory, Vector3 position)
    {
        GameObject continent = territory.transform.parent.gameObject;
        if (exercitos.ContainsKey(continent) && exercitos[continent] - exercitosAdd[continent] >= 1) {
            exercitosAdd[continent] += 1;
            territory.CreateTroop(position);
        }else if(exercitos[this.gameObject] - exercitosAdd[this.gameObject] >= 1) {
            exercitosAdd[this.gameObject] += 1;
            territory.CreateTroop(position);
        }
    }


}


