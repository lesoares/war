using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.CSs
{
    class TropaController : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {

        }

        void OnMouseDown()
        {
            var a = this.GetComponent<SpriteRenderer>().color.a;
            if (a == 1)
            {
                Color tmp = this.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.5f;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
            else
            {
                Color tmp = this.GetComponent<SpriteRenderer>().color;
                tmp.a = 1;
                this.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
    }
}
