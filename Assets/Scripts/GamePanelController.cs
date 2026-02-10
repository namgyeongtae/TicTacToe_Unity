using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private Image playerATurnImage;
    [SerializeField] private Image playerBTurnImage;


    void Start()
    {
    }

    // 뒤로가기 버튼 클릭
    public void OnClickBackButton()
    {
        /* GameManager.Instance.ChangeToMainScene(); */

        GameManager.Instance.OpenConfirmPanel("게임을 종료하시겠습니까?", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    // 설정 팝업 표시
    public void OnClickSettingsButton()
    {
        GameManager.Instance.OpenSettingsPanel();
    }

    public void SetPlayerTurnPanel(Constants.PlayerType playerType)
    {
        switch(playerType)
        {
            case Constants.PlayerType.None:
                playerATurnImage.color = Color.white;
                playerBTurnImage.color = Color.white;
                break;
            case Constants.PlayerType.PlayerA:
                playerATurnImage.color = Color.blue;
                playerBTurnImage.color = Color.white;
                break;
            case Constants.PlayerType.PlayerB:
                playerATurnImage.color = Color.white;
                playerBTurnImage.color = Color.red; 
                break;
        }
    }
}
