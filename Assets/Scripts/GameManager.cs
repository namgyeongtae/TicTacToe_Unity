using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject settingsPanelPrefab;
    [SerializeField] private GameObject confirmPanelPrefab;
    
    private Canvas _canvas;
    private GamePanelController _gamePanelController;

    private GameLogic _gameLogic;

    // 게임의 종류 (싱글, 듀얼)
    private GameType _gameType;

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {       
        // 새로운 씬에서 Canvas 참조 가져오기기
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == SCENE_GAME)
        {
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController)
            {
                blockController.InitBlocks();
            }

            _gamePanelController = FindFirstObjectByType<GamePanelController>();

            _gameLogic = new GameLogic(_gameType, blockController);
        }
    }

    public void SetGameTurn(Constants.PlayerType playerType)
    {
        _gamePanelController.SetPlayerTurnPanel(playerType);
    }

    // Settings 패널 열기
    public void OpenSettingsPanel()
    {
        var settingsPanelObject = Instantiate(settingsPanelPrefab, _canvas.transform);
        settingsPanelObject.GetComponent<SettingsPanelController>().Show();
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked = null)
    {
        var confirmPanelObject = Instantiate(confirmPanelPrefab, _canvas.transform);
        confirmPanelObject.GetComponent<ConfirmPanelController>().Show(message, onConfirmButtonClicked);
    }

    // 씬 전환 (Main > Game)
    public void ChangeToGameScene(GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene(SCENE_GAME);       
    }

    // 씬 전환 (Game > Main)
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene(SCENE_MAIN);
    }
}
