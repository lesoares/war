using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

    //lista de vizinhos do territorio
    public List<GameObject> neighborhood;
    public List<GameObject> tropasNormais;
    public List<GameObject> tropasGrandes;
    public GameObject Tropa;

    // Use this for initialization
    void Start () {
        tropasNormais = new List<GameObject>();
        tropasGrandes = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// Ao reconhecer o clique do mouse, printo as informações do território
    /// </summary>
    void OnMouseDown()
    {
        SendMessage("CheckAvailableTerritories", GameController.isPlayerTurn);
        Debug.Log("região " + name + " clicada");
        Debug.Log("região pertence a " + transform.parent.name);
        foreach (var item in neighborhood)
        {
            Debug.Log( item.name +  " pertence a sua vizinhança");
        }
    }

    void OnMouseOver()
    {
        if (!MapDrag.isDragging)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                       Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                                                       this.transform.position.z - 1);
                CreateTroop(position);
            }
        }
    }

    void CreateTroop(Vector3 position)
    {
        //bool tN;
        //bool tG;
        GameObject t;

        if (tropasNormais.Count < 4)
        {
            t = Instantiate(Tropa,position, Quaternion.identity);
            tropasNormais.Add(t);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                t = tropasNormais[0];
                tropasNormais.Remove(t);
                t.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(t);
            }

            var tropaG = Instantiate(Tropa, position, Quaternion.identity);
            tropaG.transform.localScale += new Vector3(3, 3, 3);
            tropasGrandes.Add(tropaG);
        }
    }

    void OnMousedrag()
    {
        MapDrag.isDragging = true;
    }

    void CheckAvailableTerritories(bool isPlayer)
    {
        if (isPlayer)
        {
            if (!GameController.playerTerritories.Contains(this.gameObject)) { Debug.Log("não pertence a região"); return; }
            foreach (var t in neighborhood)
            {
                if (GameController.playerTerritories.Contains(t))
                {
                    Debug.Log(t.name);
                }
            }
        }
        else
        {
            if (!GameController.iaTerritories.Contains(this.gameObject)) { Debug.Log("não pertence a região"); return; }
            foreach (var t in neighborhood)
            {
                if (GameController.iaTerritories.Contains(t))
                {
                    Debug.Log(t.name);
                }
            }
        }
        
    }
}
