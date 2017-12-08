namespace UnitTestProject
{
    public class TerritoryControllerTest
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
