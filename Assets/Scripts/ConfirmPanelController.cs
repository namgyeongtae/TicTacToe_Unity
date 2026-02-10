using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    public delegate void OnConfirmButtonClicked();

    public OnConfirmButtonClicked onConfirmButtonClicked;

    public void Show(string message, OnConfirmButtonClicked onConfirmButtonClicked = null)
    {
        this.onConfirmButtonClicked = onConfirmButtonClicked;

        messageText.text = message;
        Show();
    }

    public void OnClickConfirmButton()
    {
        Hide(() =>
        {
            onConfirmButtonClicked?.Invoke();
        });
    }
}
