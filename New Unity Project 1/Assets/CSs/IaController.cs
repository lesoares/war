using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory
{
    public int player;
    public int count;
    public string name;
    public List<Territory> neighborhood;

    public Territory(int player, int count, string name)
    {
        this.player = player;
        this.count = count;
        this.name = name;
        this.neighborhood = new List<Territory>();
    }


}

public class IaController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int IaDistribuicao(List<Territory> territorios)
    {
        return territorios[0].count;
    }

}
