using System;
using UnityEngine;

public class GameSystem : MonoBehaviour {
    public static event Action HasReset;
    public static event Action HasGameOver;
    
    public GameObject gameOver;
    public GameObject gamePlay;
    public GameObject victory;

    [SerializeField] private Player playerScript;
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private AudioManager audioManagerScript;

    public void ResetGame() {
        HasReset?.Invoke();

        if (!playerScript)
            playerScript = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        if (playerScript)
            playerScript.enabled = true;
        if (!playerMovementScript)
            playerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovementScript)
            playerMovementScript.enabled = true;

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

        if (!playerScript)
            playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (playerScript)
            playerScript.enabled = false;
        if (!playerMovementScript)
            playerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovementScript)
            playerMovementScript.enabled = false;

        if (!audioManagerScript)
            audioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (audioManagerScript)
        {
            audioManagerScript.StopAll();
            audioManagerScript.Play("BGM");
            audioManagerScript.Play("Pedestrian");
        }

            gameOver.SetActive(true);
        gamePlay.SetActive(false);
        HasGameOver?.Invoke();
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
