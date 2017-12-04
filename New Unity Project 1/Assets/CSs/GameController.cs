
using Assets.CSs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<TerritoryController> Territories = new List<TerritoryController>();
    public static Texture2D playerColor = null;

    public List<GameObject> AttackDiceObject;
    public List<GameObject> DefenseDiceObject;
    public GameObject Select;
    public GameObject Other;
    public GameObject Tropa;
    public GameObject Player;
    public GameObject IA;
    public GameObject log;
    public GameObject slider;
    public GameObject playerText;
    public GameObject nextButton;
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
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(0, new Color(0, 0, 1)));
        players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(0, new Color(0, 1, 0)));

        //players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(2, new Color(0, 1, 0)));
        players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(1, new Color(0, 1, 1)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(3, new Color(1, 0, 0)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(4, new Color(1, 0, 1)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(5, new Color(1, 1, 0)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(6, new Color(1, 1, 1)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(7, new Color(0.0f, 0.0f, 0.5f)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(8, new Color(0, 0.5f, 0)));
        //players.Add(Instantiate(IA).GetComponent<PlayerBase>().configure(9, new Color(0, 0.5f, 0.5f)));
        //players.Add(Instantiate(Player).GetComponent<PlayerBase>().configure(new Color(1, 1, 0)));

        for (var i = 0; i < 3; i++) {
            var apos = AttackDiceObject[i].transform.position;
            var dpos = DefenseDiceObject[i].transform.position;
            AttackDiceObject[i].transform.position = new Vector3(apos.x, apos.y, 700);
            DefenseDiceObject[i].transform.position = new Vector3(dpos.x, dpos.y, 700);
        }
        this.slider.SetActive(false);

        for (int i = 0; i < players.Count; i++) {
            var temp = players[i];
            var randomIndex = UnityEngine.Random.Range(i, players.Count);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
            players[i].GetComponent<PlayerBase>().numTurn = i;
        }

        DistributeTerritories();
        turn = 0;
        state = -1;
        StartTurn();
    }

    void DistributeTerritories()
    {
        var range = Enumerable.Range(0, Territories.Count).ToList();
        for (int i = 0; i < range.Count; i++) {
            var temp = range[i];
            var randomIndex = UnityEngine.Random.Range(i, range.Count);
            range[i] = range[randomIndex];
            range[randomIndex] = temp;
        }
        for (int i = 0; i < range.Count; i++) {
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
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        player.selectedTerritory = null;
        player.otherTerritory = null;
        this.playerText.GetComponent<Text>().text = player.Text() + " " + (player.numTurn + 1);
        this.playerText.GetComponent<Text>().color = player.color;
        this.log.GetComponent<Text>().text = "";
        exercitos = new Dictionary<GameObject, int>();
        exercitosAdd = new Dictionary<GameObject, int>();
        int territories = Territories.Sum(t => (t.player == turn) ? 1 : 0);
        if (territories == 0) {
            EndTurn();
        }
        int world = Math.Max(3, territories / 2);
        exercitos[this.gameObject] = world;
        exercitosAdd[this.gameObject] = 0;

        for (var i = 0; i <  this.gameObject.transform.childCount; i++) {
            var continent = this.gameObject.transform.GetChild(i).GetComponent<ContinentController>();
            var owner = true;
            for (var j = 0; j < continent.gameObject.transform.childCount; j++) {
                var territory = continent.gameObject.transform.GetChild(j).GetComponent<TerritoryController>();
                if (territory.player != turn) {
                    owner = false;
                    break;
                }
            }
            if (owner) {
                exercitos[continent.gameObject] = continent.units;
                exercitosAdd[continent.gameObject] = 0;
            }
        }

    }

    public bool AttackState()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        player.selectedTerritory = null;
        player.otherTerritory = null;
        foreach (KeyValuePair<GameObject, int> entry in exercitos) {
            if (entry.Value - exercitosAdd[entry.Key] > 0) {
                return false;
            }
        }

        if (state == -1) {
            EndTurn();
        } else {
            state = 1;
            substate = 0;
        }
        return true;
    }

    public void AttackSubstate()
    {
        for (var i = 0; i < 3; i++) {
            var apos = AttackDiceObject[i].transform.position;
            var dpos = DefenseDiceObject[i].transform.position;
            AttackDiceObject[i].transform.position = new Vector3(apos.x, apos.y, 700);
            DefenseDiceObject[i].transform.position = new Vector3(dpos.x, dpos.y, 700);
        }
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        player.selectedTerritory = null;
        player.otherTerritory = null;
        this.slider.SetActive(false);
        substate = 0;
    }

    public void ConquestSubstate()
    {
        substate = 3;
    }

    public void RedistributeState()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        player.selectedTerritory = null;
        player.otherTerritory = null;
        redistributed = new Dictionary<TerritoryController, int>();
        foreach (var t in Territories) {
            redistributed[t] = 0;
        }
        state = 2;
        substate = 0;
    }

    public void RedistributeSelectedSubstate()
    {
        state = 2;
        substate = 1;
    }

    public void RedistributeSubstate()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();

        player.selectedTerritory = null;
        player.otherTerritory = null;
        this.slider.SetActive(false);
        state = 2;
        substate = 0;
    }

    void Update()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        var selectedSprite = this.Select.GetComponent<SpriteRenderer>();
        var selectedColor = player.color;
        if (player.selectedTerritory != null) {
            selectedColor.a = 1;
            var selectedPos = player.selectedTerritory.transform.position;
            this.Select.transform.position = new Vector3(
                selectedPos.x, selectedPos.y, this.Select.transform.position.z
            );
        } else {
            selectedColor.a = 0;
        }
        selectedSprite.color = selectedColor;

        var otherSprite = this.Other.GetComponent<SpriteRenderer>();
        var otherColor = player.color;
        if (player.otherTerritory != null) {
            otherColor.a = 1;
            var otherPos = player.otherTerritory.transform.position;
            this.Other.transform.position = new Vector3(
               otherPos.x, otherPos.y, this.Other.transform.position.z
            );
        } else {
            otherColor.a = 0;
        }
        otherSprite.color = otherColor;

        this.nextButton.SetActive(player.showNext);


        if (state == -1 || state == 0) {
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
        } else if (state == 2) {
            if (substate == 0) {
                player.Redistribute(this, redistributed);
            } else {
                player.RedistributeSelected(this, redistributed);
            }
        }
    }



    public void EndTurn()
    {
        
        if (checkObjective())
        {
            players[turn].GetComponent<PlayerBase>().EndGame();
        }
        turn = (turn + 1) % players.Count;
        if (state != -1 || turn == 0) {
            state = 0;
        }
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
        if ((contaTerritorio > 40)) //|| (contaTerritorio <= 23) 
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

    public bool Attack(TerritoryController source, TerritoryController target)
    {
        if (source.player != turn || target.player == turn) {
            return false;
        }
        bool isNeighborhood = false;
        foreach (var n in source.neighborhood) {
            if (System.Object.ReferenceEquals(n, target)) {
                isNeighborhood = true;
                break;
            }
        }
        if (!isNeighborhood) {
            return false;
        }
        var attack = Math.Min(3, source.getTropas() - 1);
        var defense = Math.Min(3, target.getTropas());
        attackDice = new List<int>();
        defenseDice = new List<int>();
        for (var i = 0; i < attack; i++) {
            attackDice.Add(UnityEngine.Random.Range(1, 7));
        }
        for (var i = 0; i < defense; i++) {
            defenseDice.Add(UnityEngine.Random.Range(1, 7));
        }
        attackDice.Sort();
        defenseDice.Sort();
        attackDice.Reverse();
        defenseDice.Reverse();
        for (var i = 0; i < Math.Min(attack, defense); i++) {
            if (attackDice[i] > defenseDice[i]) {
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
        //this.GetComponent<MapController>().resetCamera();
        for (var i = 0; i < 3; i++) {
            var apos = AttackDiceObject[i].transform.position;
            var dpos = DefenseDiceObject[i].transform.position;
            AttackDiceObject[i].transform.position = new Vector3(apos.x, apos.y, 700);
            DefenseDiceObject[i].transform.position = new Vector3(dpos.x, dpos.y, 700);
        }
        for (var i = 0; i < attack; i++) {
            var apos = AttackDiceObject[i].transform.position;
            AttackDiceObject[i].GetComponent<DiceController>().roll(attackDice[i]);
            AttackDiceObject[i].transform.position = new Vector3(apos.x, apos.y, 200);
        }
        for (var i = 0; i < defense; i++) {
            var dpos = DefenseDiceObject[i].transform.position;
            DefenseDiceObject[i].GetComponent<DiceController>().roll(defenseDice[i]);
            DefenseDiceObject[i].transform.position = new Vector3(dpos.x, dpos.y, 200);
        }
        return true;
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
        var found = false;
        foreach (var n in source.neighborhood) {
            if (System.Object.ReferenceEquals(n, target)) {
                found = true;
                break;
            }
        }
        if (!found) {
            return false;
        }
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



    public void NextPlayerState()
    {
        PlayerBase player = players[turn].GetComponent<PlayerBase>();
        if (state == -1 || state == 0) {
            AttackState();
        } else if (state == 1) {
            if (substate == 0) {
                RedistributeState();
            } else if (substate == 3 && this.MoveConquest(Convert.ToInt32(this.slider.GetComponent<Slider>().value))) {
                AttackSubstate();
            }
        } else if (state == 2) {
            if (substate == 0) {
                EndTurn();
            } else if (substate == 1 && this.Redistribute(player.selectedTerritory, player.otherTerritory, Convert.ToInt32(this.slider.GetComponent<Slider>().value))) {
                RedistributeSubstate();
            }
        }
    }
}


