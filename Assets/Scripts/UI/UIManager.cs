using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject popup;

    private TextMeshProUGUI popupText;

    public delegate void RestartClickEvent();

    public event RestartClickEvent OnRestartClick;

    private void Start()
    {
        popupText = popup.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnRestart()
    {
        OnRestartClick?.Invoke();
    }

    public void SetPopupText(string text)
    {
        popupText.text = text;
    }

    public void SetPopupState(bool state)
    {
        popup.SetActive(state);
    }
}
