using UnityEngine;

public class GameStart : MonoBehaviour {
    [SerializeField] private Player playerScript;
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private GameObject gamePlay;
    
    private void OnEnable() {
        if (!playerScript)
            playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (playerScript)
            playerScript.enabled = false;
        if (!playerMovementScript)
            playerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovementScript)
            playerMovementScript.enabled = false;
        Time.timeScale = 0.1f;
        gamePlay.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            gameObject.SetActive(false);
    }

    private void OnDisable() {
        gamePlay.SetActive(true);
        if (!playerScript)
            playerScript = GameObject.FindWithTag("Player")?.GetComponent<Player>();
        if (playerScript)
            playerScript.enabled = true;
        if (!playerMovementScript)
            playerMovementScript = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovementScript)
            playerMovementScript.enabled = true;
        Time.timeScale = 1f;
    }
}
