using Assets.CSs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryController : MonoBehaviour {

    //lista de vizinhos do territorio
    public List<TerritoryController> neighborhood;
    public List<GameObject> tropasNormais = new List<GameObject>();
    public List<GameObject> tropasGrandes = new List<GameObject>();
    public List<GameObject> tropasSelecionadas = new List<GameObject>();
    public GameObject Tropa;
    public int player;
    public int index;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public int getTropas ()
    {
        return tropasNormais.Count + tropasGrandes.Count * 5;
    }

    /// <summary>
    /// Ao reconhecer o clique do mouse, printo as informações do território
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("região " + name + " clicada");
        Debug.Log("região pertence a " + transform.parent.name);
        SendMessage("CheckAvailableTerritories");
        //foreach (var item in neighborhood)
        //{
        //    Debug.Log( item.name +  " pertence a sua vizinhança");
        //}
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
                if (player == 1) { 
                    CreateTroop(position);
                }
            }
        }
    }

    public Vector3 SpiralPoint(Vector3 center, int t, float a)
    {
        float x = a * t * Mathf.Cos(t);
        float y = a * t * Mathf.Sin(t);

        return new Vector3(center.x + x, center.y + y, center.z - 1);
    }

    public Vector3 PointInArea()
    {
        var collider = this.gameObject.GetComponent<PolygonCollider2D>();
        var bounds = collider.bounds;

        return SpiralPoint(bounds.center, tropasNormais.Count + tropasGrandes.Count, 1.2f);
    }


    public void CreateTroop(Vector3 position)
    {
        //bool tN;
        //bool tG;
        GameObject t;
        if (tropasNormais.Count < 4)
        {
            t = Instantiate(Tropa, position, Quaternion.identity);
            t.GetComponent<TropaController>().configure(this);
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
            tropaG.GetComponent<TropaController>().configure(this);
            tropaG.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            tropasGrandes.Add(tropaG);
        }
        
    }

    public void DestroyTroop()
    {
        GameObject t, m;
        if (tropasNormais.Count == 0 && tropasGrandes.Count > 0) {
            t = tropasGrandes[tropasGrandes.Count - 1];
            tropasGrandes.Remove(t);
            t.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < 5; i++) {
                m = Instantiate(Tropa, this.SpiralPoint(t.transform.position, i, 1.2f), Quaternion.identity);
                m.GetComponent<TropaController>().configure(this);
                tropasNormais.Add(m);
            }
            Destroy(t);
        }
        if (tropasNormais.Count > 0) {
            t = tropasNormais[tropasNormais.Count - 1];
            tropasNormais.Remove(t);
            t.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(t);
        }
    }

    void OnMousedrag()
    {
        MapDrag.isDragging = true;
    }

    void CheckAvailableTerritories()
    {

        if (this.player != GameController.turn) { Debug.Log("não pertence a região"); return; }
        foreach (var t in neighborhood)
        {
            if (t.player == GameController.turn)
            {
                Debug.Log(t.name);
            }
        }
      
        
    }
}
