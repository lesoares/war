using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class IaTest
{

    public IaController ia;


    [SetUp]
    public void Init()
    {
        var go = new GameObject();
        go.AddComponent<IaController>();
        this.ia = go.GetComponent<IaController>();

        var got = GameObject.Find("GoT");
        GameController gc = got.GetComponent<GameController>();

    }


    [Test]
    public void TestaIA1()
    {
        // Use the Assert class to test conditions.
    }

    [Test]
    public void TestaIA2()
    {
        // Use the Assert class to test conditions.
    }
}
