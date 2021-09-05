using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadUIScene : MonoBehaviour
{
    private void Start() {
        SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
    }
}
