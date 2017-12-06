
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

    public override string Text()
    {
        return "IA";
    }

    public override void EndGame()
    {
        Vencedor.number = numTurn;
        Vencedor.color = color;
        SceneManager.LoadScene(2);
    }

    public override void Distribute(GameController got, Dictionary<GameObject, int> exercitos, Dictionary<GameObject, int> exercitosAdd)
    {
        foreach (KeyValuePair<GameObject, int> entry in exercitos)
        {
            if (entry.Value - exercitosAdd[entry.Key] < 1) {
                continue;
            }
            List<TerritoryController> territories;
            bool isContinent = false;
            if (System.Object.ReferenceEquals(entry.Key, got.gameObject)) {
                territories = got.Territories;
            } else {
                territories = new List<TerritoryController>();
                for( var i = 0; i < entry.Key.transform.childCount; i++) {
                    territories.Add(entry.Key.transform.GetChild(i).GetComponent<TerritoryController>());
                }
                isContinent = true;
            }
            DistributeIA(got, territories, entry.Value - exercitosAdd[entry.Key], isContinent);
        }
        got.AttackState();
    }

    void DistributeIA(GameController got, List<TerritoryController> territories, int add, bool isContinent)
    {
        var territory = territories.OrderBy(t => {
            if (t.player != this.numTurn) return 1000000000;
            var enemy = t.neighborhood.Sum(n => (n.player != this.numTurn) ? 1 : 0);
            if (enemy == 0) return 1000000;
            var result = (Convert.ToDouble(t.neighborhood.Sum(n => (n.player != numTurn) ? n.getTropas() : 0)) - t.getTropas()) / Convert.ToDouble(enemy)
            - Convert.ToDouble(enemy);
            return result;
        }).First();
        for (var i = 0; i < add; i++) {
            var position = territory.PointInArea();
            got.CreateTroop(territory, position);
        }
    }

    public override void Attack(GameController got)
    {
        Attack choose = null;
        foreach(TerritoryController t in got.Territories) {
            if(t.player != this.numTurn) {
                continue;
            }
            foreach(TerritoryController n in t.neighborhood) {
                if (n.player == this.numTurn) {
                    continue;
                }
                if (t.getTropas() <= n.getTropas()) {
                    continue;
                }
                Attack novo = new Attack(t, n, t.getTropas() - n.getTropas());

                if ( choose == null || choose.pont < novo.pont) {
                    choose = novo;
                }
            }
        }
        if(choose != null) {
            this.selectedTerritory = choose.source;
            this.otherTerritory = choose.target;
            got.Attack(choose.source, choose.target);
        } else {
            got.RedistributeState();
        }
    }

    public override void Conquest(GameController got, TerritoryController source, TerritoryController target)
    {
        bool enemy = false;
        foreach(var n in target.neighborhood) {
            if(n.player != this.numTurn) {
                enemy = true;
            }
        }
        if (enemy) {
            if (got.MoveConquest(Math.Min(3, source.getTropas() - 1))) {
                got.AttackSubstate();
            }
        } else {
            if (got.MoveConquest(1)) {
                got.AttackSubstate();
            }
        }
    }

    public override void Redistribute(GameController got, Dictionary<TerritoryController, int> redistributed)
    {
        bool changed = false;
        foreach (TerritoryController t in got.Territories) {
            if (t.player != this.numTurn || t.getTropas() - redistributed[t] == 0 || t.getTropas() == 1) {
                continue;
            }
            // Busca em largura

            List<Relocation> options = new List<Relocation>();

            Queue<Relocation> queue = new Queue<Relocation>();
            foreach (var n in t.neighborhood) {
                Relocation relocation = new Relocation(n, n, 1);
                if (n.player != this.numTurn) {
                    options.Add(new Relocation(t, t, 0));
                } else {
                    queue.Enqueue(relocation);
                }
            }

            if (options.Count == 0) {
                while (queue.Count > 0) {
                    Relocation current = queue.Dequeue();
                    foreach (var n in current.territory.neighborhood) {
                        Relocation relocation = new Relocation(n, current.origin, current.distance + 1);
                        if (n.player != this.numTurn) {
                            if (options.Count == 0 || options[options.Count - 1].distance == current.distance) {
                                options.Add(current);
                            } else {
                                queue.Clear();
                                break;
                            }
                        } else {
                            queue.Enqueue(relocation);
                        }
                    }
                }
            }
            if (options.Count > 0) {
                Relocation selected = options.OrderBy(o => {
                    var enemy = o.territory.neighborhood.Sum(n => (n.player != this.numTurn) ? n.getTropas() : 0);
                    return -enemy;
                }).First();
                int quantity; 
                if (redistributed[t] == 0) {
                    quantity = t.getTropas() - 1;
                } else {
                    quantity = t.getTropas() - redistributed[t];
                }
                changed |= got.Redistribute(t, selected.origin, quantity);
            }
        }
        if (!changed) {
            got.EndTurn();
        }
    }
}


class Attack
{
    public TerritoryController source;
    public TerritoryController target;
    public int pont;

    public Attack (TerritoryController source, TerritoryController target, int pont)
    {
        this.source = source;
        this.target = target;
        this.pont = pont;
    }
}

class Relocation
{
    public TerritoryController territory;
    public TerritoryController origin;
    public int distance;

    public Relocation(TerritoryController territory, TerritoryController origin, int distance)
    {
        this.territory = territory;
        this.origin = origin;
        this.distance = distance;
    }
}