using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Game_Scene");
        }
    }
}
