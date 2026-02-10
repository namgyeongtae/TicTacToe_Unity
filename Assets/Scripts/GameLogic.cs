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

    public void CheckGameResult()
    {

    }

    /* public bool CheckGameWin(PlayerType playerType,)
    {

    } */
}
