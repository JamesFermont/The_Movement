using UnityEngine;

public class GameStart : MonoBehaviour {
    [SerializeField] private Player playerScript;
    [SerializeField] private GameObject gamePlay;
    
    private void OnEnable() {
        if (!playerScript)
            playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (playerScript)
            playerScript.enabled = false;
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
        Time.timeScale = 1f;
    }
}
