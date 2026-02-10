using UnityEngine;

public class PlayerState : BaseState
{
    private Constants.PlayerType _playerType;

    public PlayerState(bool isFirstPlayer)
    {
        _playerType = isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }
    
    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic, index, _playerType);
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();
    }

    public override void OnEnter(GameLogic gameLogic)
    {
        gameLogic.blockController.onBlockClicked = (blockIndex) => 
        {
            // 블록이 클릭되었을 때 처리할 로직
            HandleMove(gameLogic, blockIndex);
        };
    }

    public override void OnExit(GameLogic gameLogic)
    {
        
    }
}
