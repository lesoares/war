
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
    public static int substate = 0;
    public Dictionary<GameObject, int> exercitos;
    public Dictionary<GameObject, int> exercitosAdd;

    private TerritoryController attackSource;
    private TerritoryController attackTarget;
    private List<int> attackDice;
    private List<int> defenseDice;
    private Dictionary<TerritoryController, int> redistributed;



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
            territory.index = range[i];
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
        substate = 0;
        return true;
    }

    public void AttackSubstate()
    {
        substate = 0;
    }

    public void ConquestSubstate()
    {
        substate = 3;
    }

    public void RedistributeState()
    {
        redistributed = new Dictionary<TerritoryController, int>();
        foreach (var t in Territories) {
            redistributed[t] = 0;
        }
        state = 2;
    }

    void Update()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        Debug.Log(turn + " " + state + " " + player.numTurn);

        if (state == 0) {
            player.Distribute(this, exercitos, exercitosAdd);
        } else if (state == 1) {
            if (substate == 0) {
                player.Attack(this);
            } else if (substate == 1) {
                player.ShowAttack(this, attackSource, attackTarget, attackDice, defenseDice, false);
            } else if (substate == 2) {
                player.ShowAttack(this, attackSource, attackTarget, attackDice, defenseDice, true);
            } else if (substate == 3) {
                player.Conquest(this, attackSource, attackTarget);
            }
        } else if (state == 2){
            player.Redistribute(this, redistributed);
        }
    }



    public void EndTurn()
    {
        /*
        if (checkObjective())
        {
            players[turn].GetComponent<PlayerBase>().EndGame();
        }*/
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

    public void Attack(TerritoryController source, TerritoryController target)
    {
        if(source.player != turn || target.player == turn) {
            return;
        }
        var attack = Math.Min(3, source.getTropas() - 1);
        var defense = Math.Min(3, target.getTropas());
        attackDice = new List<int>();
        defenseDice = new List<int>();
        for(var i = 0; i < attack; i++) {
            attackDice.Add(UnityEngine.Random.Range(1, 6));
        }
        for (var i = 0; i < defense; i++) {
            defenseDice.Add(UnityEngine.Random.Range(1, 6));
        }
        attackDice.Sort();
        defenseDice.Sort();
        attackDice.Reverse();
        defenseDice.Reverse();
        for(var i = 0; i < Math.Min(attack, defense); i++) {
            if(attackDice[i] > defenseDice[i]) {
                target.DestroyTroop();
            } else {
                source.DestroyTroop();
            }
        }
        this.attackSource = source;
        this.attackTarget = target;
        if (target.getTropas() == 0) {
            target.player = turn;
            substate = 2;
        } else {
            substate = 1;
        }
    }

    public bool MoveConquest(int troops)
    {
        if(attackSource.getTropas() > troops && troops <= 3) {
            for (var i = 0; i < troops; i++) {
                attackTarget.CreateTroop(attackTarget.PointInArea());
                attackSource.DestroyTroop();
            }
            return true;
        }
        return false;
    }

    public bool Redistribute(TerritoryController source, TerritoryController target, int quantity)
    {
        if (System.Object.ReferenceEquals(source, target)) {
            return false;
        }
        if (source.player != turn || target.player != turn) {
            return false;
        }
        if (source.getTropas() <= quantity) {
            return false;
        }
        if (source.getTropas() - redistributed[source] < quantity) {
            return false;
        }
        for (var i = 0; i < quantity; i++) {
            target.CreateTroop(target.PointInArea());
            redistributed[target] += 1;
            source.DestroyTroop();
        }
        return true;
    }

}


