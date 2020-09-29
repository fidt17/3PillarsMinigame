using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _winMenu;

    public void OnButtonStart() {
        FieldSetup.GetInstance().StartNewGame();
        _startMenu.SetActive(false);
    }

    public void OnButtonRestart() {
        FieldSetup.GetInstance().RestartGame();
        _winMenu.SetActive(false);
    }

    public void OnButtonExit() {
        Application.Quit();
    }

    public void ShowWinMenu() {
        _winMenu.SetActive(true);
    }
}
