using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class IaTest
{

    public IaController ia;
    public List<Territory> territorio;


    [SetUp]
    public void Init()
    {
        var go = new GameObject();
        go.AddComponent<IaController>();
        this.ia = go.GetComponent<IaController>();

        var got = GameObject.Find("GoT");
        GameController gc = got.GetComponent<GameController>();

        Dictionary<TerritoryController, Territory> mapa = new Dictionary<TerritoryController, Territory>();

        this.territorio = new List<Territory>();
        foreach (var t in gc.Territories)
        {
            System.Console.WriteLine(t.name);
            var territory = new Territory(1, 1, t.name);
            this.territorio.Add(territory);
            mapa[t] = territory;
        }
        foreach (var controller in gc.Territories)
        {
            var territory = mapa[controller];
            foreach (var n in controller.neighborhood)
            {
                territory.neighborhood.Add(mapa[n]);
            }
        }
    }


    [Test]
    public void TestaIA1()
    {
        this.territorio[0].count = 2;
        Assert.AreEqual(2, this.ia.IaDistribuicao(this.territorio));
        // Use the Assert class to test conditions.
    }

    [Test]
    public void TestaIA2()
    {
        Assert.AreEqual(1, this.ia.IaDistribuicao(this.territorio));
        // Use the Assert class to test conditions.
    }
}
