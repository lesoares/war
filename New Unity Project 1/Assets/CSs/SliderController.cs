using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CSs
{
    class SliderController : MonoBehaviour
    {
         
        void SettingPlayers(Single value)
        {
            int qtIA = 1;
            int qtPlayer = 2;
            GameController game = new GameController();
            game.Configure(qtIA, qtPlayer);
        }


    }
}