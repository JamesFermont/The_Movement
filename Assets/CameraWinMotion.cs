using System.Collections;
using UnityEngine;

public class CameraWinMotion : MonoBehaviour {
    public GameObject winbg;

    public float animDuration;
    
    private CameraFollow2D _follow2D;

    private void Start() {
        _follow2D = GetComponent<CameraFollow2D>();
    }

    private void OnEnable() {
        InfluenceHandler.PlayerWon += HandleVictory;
    }

    private void OnDisable() {
        InfluenceHandler.PlayerWon -= HandleVictory;
    }

    private void HandleVictory() {
        StartCoroutine(AnimateWin());
    }

    private IEnumerator AnimateWin() {
        _follow2D.enabled = false;

        float timeElapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 localPos = winbg.transform.localPosition;
        Vector3 targetPos = winbg.transform.position;
        targetPos.z = -10f;

        while (timeElapsed < animDuration) {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / animDuration);
            winbg.transform.localPosition =
                Vector3.Lerp(localPos, Vector3.forward, timeElapsed / animDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        
        Debug.Log("Animation finished!");
    }
}
