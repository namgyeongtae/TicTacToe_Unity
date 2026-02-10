using Unity.VisualScripting;
using UnityEngine;
using static Constants;


public class GameLogic
{
    public BlockController blockController;

    private PlayerType[,] _board;

    public BaseState playerAState;
    public BaseState playerBState;

    private BaseState _currentState;

    public enum GameResult { None, Win, Lose, Draw }

    public GameLogic(GameType gameType, BlockController blockController)
    {
        this.blockController = blockController;

        _board = new PlayerType[BOARD_SIZE, BOARD_SIZE];

        switch (gameType)
        {
            case GameType.SinglePlay:
                playerAState = new PlayerState(true);
                playerBState = new PlayerState(false);

                SetState(playerAState);
                break;
            case GameType.DualPlay:
                playerAState = new PlayerState(true);
                playerBState = new PlayerState(false);

                SetState(playerAState);
                break;
        }
    }

    public void SetState(BaseState newState)
    {
        _currentState?.OnExit(this);
        _currentState = newState;
        _currentState.OnEnter(this);
    }

    // 마커 표시를 위한 메서드
    public bool PlaceMarker(int index, PlayerType playerType)
    {
        var row = index / BOARD_SIZE;
        var col = index % BOARD_SIZE;

        if (_board[row, col] != PlayerType.None) return false;

        blockController.PlaceMarker(index, playerType);
        _board[row, col] = playerType;

        return true;
    }

    public void ChangeGameState()
    {
        if(_currentState == playerAState)
        {
            Debug.Log("Change to Player B");
            SetState(playerBState);
        }
        else
        {
            Debug.Log("Change to Player A");
            SetState(playerAState);
        }
    }

    public GameResult CheckGameResult()
    {
        if (CheckGameWin(PlayerType.PlayerA, _board)) { return GameResult.Win; }
        if (CheckGameWin(PlayerType.PlayerB, _board)) { return GameResult.Lose; }
        if (CheckGameDraw(_board)) { return GameResult.Draw; }
        return GameResult.None;
    }

    public bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[1, col] == playerType &&
                board[2, col] == playerType)
            {
                return true;
            }
        }
        if (board[0,0] == playerType &&
            board[1,1] == playerType &&
            board[2,2] == playerType)
        {
            return true;
        }
        if (board[0,2] == playerType &&
            board[1,1] == playerType &&
            board[2,0] == playerType)
        {
            return true;
        }
        return false;
    }

    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }
        return true;
    }

    public void EndGame(GameResult gameResult)
    {
        string resultStr = "";
        switch(gameResult)
        {
            case GameResult.Win:
                resultStr = "Player A 승리";
                break;
            case GameResult.Lose:
                resultStr = "Player B 승리";
                break;
            case GameResult.Draw:
                resultStr = "무승부";
                break;
        }

        GameManager.Instance.OpenConfirmPanel(resultStr, () => 
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }
}
