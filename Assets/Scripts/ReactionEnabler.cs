using System.Collections;
using UnityEngine;

public class ReactionEnabler : MonoBehaviour {
    [SerializeField] private ReactionRenderer reactionRenderer;
    [SerializeField] private SpriteRenderer reactionBoxRenderer;

    [SerializeField] private float fadeTime = 0.8f;
    
    // Start is called before the first frame update
    void Start() {
        Player.DancingStateChanged += ToggleReactionRenderer;
        reactionBoxRenderer.color = new Color(0.9f, 0.9f, 0.9f, 0);
        reactionRenderer.gameObject.SetActive(false);
    }

    private void ToggleReactionRenderer(bool isDancing) {
        StartCoroutine(isDancing ? FadeIn() : FadeOut());
    }

    private IEnumerator FadeIn() {
        reactionRenderer.gameObject.SetActive(true);
        float timeElapsed = 0f;
        Color color = new Color(0.9f, 0.9f, 0.9f, 0);
        reactionBoxRenderer.color = color;

        while (timeElapsed < fadeTime) {
            color.a = timeElapsed / fadeTime;
            reactionBoxRenderer.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut() {
        float timeElapsed = 0f;
        Color color = new Color(0.9f, 0.9f, 0.9f, 1);
        reactionBoxRenderer.color = color;
        
        while (timeElapsed < fadeTime) {
            color.a = 1 - timeElapsed / fadeTime;
            reactionBoxRenderer.color = color;;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        reactionRenderer.gameObject.SetActive(false);
    }
}
