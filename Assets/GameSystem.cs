using System;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    public static event Action HasReset;

    public GameObject gameOver;
    public GameObject gamePlay;
    public GameObject victory;

    public void ResetGame() {
        HasReset?.Invoke();
        gameOver.SetActive(false);
        victory.SetActive(false);
        gamePlay.SetActive(true);
        Time.timeScale = 1f;
    }

    private void OnEnable() {
        InfluenceHandler.PlayerWon += HandleVictory;
        Police.PlayerCaught += HandleGameOver;
        gameOver.SetActive(false);
        victory.SetActive(false);
    }

    private void HandleGameOver() {
        Time.timeScale = 0.1f;
        gameOver.SetActive(true);
        gamePlay.SetActive(false);
    }

    private void HandleVictory() {
        victory.SetActive(true);
        gamePlay.SetActive(false);
    }

    private void OnDisable() {
        InfluenceHandler.PlayerWon -= HandleVictory;
        Police.PlayerCaught -= HandleGameOver;
    }
}
