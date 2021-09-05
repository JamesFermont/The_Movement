using UnityEngine;

public class Restart : MonoBehaviour {
    public GameSystem system;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            system.ResetGame();
        }
    }
}
