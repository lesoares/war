using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    class TerritoryControllerTest
    {
        public static bool checkObjective(bool isPlayerTurn, int playerTerritories, int iaTerritories)
        {
            int territories = (isPlayerTurn) ? playerTerritories : iaTerritories;

            if (territories >= 20)
            {
                return true;
            }
            return false;
        }
    }
}
