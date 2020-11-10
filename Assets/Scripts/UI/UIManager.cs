using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] PlayableDirector transition;
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _winMenu;
    [SerializeField] GameObject _heartPS;
    [SerializeField] Button _restartButton;

    public void OnButtonStart()   => StartCoroutine(StartGame());
    public void OnButtonRestart() => StartCoroutine(RestartGame());
    public void OnButtonExit()    => Application.Quit();
    public void OnButtonDonate(Transform button)  => Donate(button);
    public void ShowWinMenu()     => _winMenu.SetActive(true);

    public void ShowHeartParticlesAt(Transform parent) {
        GameObject obj = Instantiate(_heartPS, parent);
        Destroy(obj, 2);
    }

    private IEnumerator RestartGame() {
        if (isStartingGame) {
            yield break;
        }
        isStartingGame = true;

        transition.Play();
        yield return new WaitForSeconds(1f);
        _winMenu.SetActive(false);
        GameField.GetInstance().RestartGame();

        yield return new WaitForSeconds(1f);
        isStartingGame = false;
    }

    private bool isStartingGame = false;
    private IEnumerator StartGame() {
        if (isStartingGame) {
            yield break;
        }
        isStartingGame = true;

        transition.Play();
        yield return new WaitForSeconds(1f);
        _startMenu.SetActive(false);
        GameField.GetInstance().StartNewGame();

        yield return new WaitForSeconds(1f);
        isStartingGame = false;
    }

    private void Donate(Transform button) {
        Debug.Log("DONATE");
        Purchaser.GetInstance().BuyDonation(button);
    }
}