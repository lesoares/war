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
            Color color = this.GetComponent<SpriteRenderer>().color;
            switch (territory.player)
            {
                case 0:
                    color.r = 0;
                    color.b = 1;
                    color.g = 0;
                    break;
                case 1:
                    color.r = 1;
                    color.b = 0;
                    color.g = 0;
                    break;
                case 2:
                    color.r = 0;
                    color.b = 0;
                    color.g = 1;
                    break;
                case 3:
                    color.r = 1;
                    color.b = 1;
                    color.g = 1;
                    break;
                case 4:
                    color.r = 1;
                    color.b = 0;
                    color.g = 1;
                    break;
                case 5:
                    color.r = 1;
                    color.b = 1;
                    color.g = 0;
                    break;
                case 6:
                    color.r = 0;
                    color.b = 1;
                    color.g = 1;
                    break;
                default:
                    color.r = 0;
                    color.b = 0;
                    color.g = 0;
                    break;
            }
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
