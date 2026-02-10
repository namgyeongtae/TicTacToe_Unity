using UnityEngine;

public abstract class BaseState
{
    public abstract void OnEnter(GameLogic gameLogic);
    public abstract void HandleMove(GameLogic gameLogic, int index);
    public abstract void OnExit(GameLogic gameLogic);
    public abstract void HandleNextTurn(GameLogic gameLogic);

    public void ProcessMove(GameLogic gamelogic,int index,Constants.PlayerType playerType)
    {
        //특정위치에 마커가 표시가 되어 있다면.
        if(gamelogic.PlaceMarker(index, playerType))
        {
            var gameResult = gamelogic.CheckGameResult();

            if(gameResult == GameLogic.GameResult.None)
                HandleNextTurn(gamelogic);                  //턴전환
            else
            {
                Debug.Log("Game Result: " + gameResult);
                gamelogic.EndGame(gameResult);
            }
        }        
    }
}
