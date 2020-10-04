using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : Singleton<UIManager> {

    [SerializeField] PlayableDirector transition;
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _winMenu;

    public void OnButtonStart()   => StartCoroutine(StartGame());
    public void OnButtonRestart() => StartCoroutine(RestartGame());
    public void OnButtonExit()    => Application.Quit();
    public void ShowWinMenu()     => _winMenu.SetActive(true);

    private IEnumerator RestartGame() {
        transition.Play();
        yield return new WaitForSeconds(1f);
        _winMenu.SetActive(false);
        GameField.GetInstance().RestartGame();
    }

    private IEnumerator StartGame() {
        transition.Play();
        yield return new WaitForSeconds(1f);
        _startMenu.SetActive(false);
        GameField.GetInstance().StartNewGame();
    }
}