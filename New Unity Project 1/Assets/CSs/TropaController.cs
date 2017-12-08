using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CSs
{
    public class TropaController : MonoBehaviour
    {

        public TerritoryController territory;
        

        public GameObject configure(TerritoryController territory)
        {
            this.transform.SetParent(territory.transform);
            this.transform.Translate(0, 0, -1);
            this.GetComponent<TropaController>().territory = territory;
            Color color = GameController.players[territory.player].GetComponent<PlayerBase>().color;
            
            this.GetComponent<SpriteRenderer>().color = color;

            return this.gameObject;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnMouseDown()
        {
            var a = this.GetComponent<SpriteRenderer>().color.a;
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            if (a == 1)
            {
                tmp.a = 0.5f;
            }
            else
            {
                tmp.a = 1;
            }
            this.GetComponent<SpriteRenderer>().color = tmp;
        }
    }
}
