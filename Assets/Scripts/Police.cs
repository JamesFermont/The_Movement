using System;
using UnityEngine;

public class Police : MonoBehaviour {
    public static event Action PlayerCaught;
    public float WalkTime = 2f;

    private float liveTime;

    private Animator policeAnimator;

    private AudioManager audioManager;

    private bool gameOver;

    private void Start()
    {
        policeAnimator = GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update() {
        liveTime += Time.deltaTime;
        policeAnimator.SetFloat("Speed", (liveTime > WalkTime ? 10.5f : 2.5f) * (gameOver ? 0 : 1));
        transform.position += new Vector3(transform.localScale.x, 0f, 0f) * Time.deltaTime * (liveTime > WalkTime ? 10.5f : 2.5f) * (gameOver ? 0 : 1);
    }

    private void OnEnable() {
        liveTime = 0f;
        GameSystem.HasReset += OnReset;
        GameSystem.HasGameOver += OnGameOver;
    }

    private void OnGameOver() {
        gameOver = true;
    }

    private void OnReset() {
        Destroy(gameObject);
    }

    private void OnDisable() {
        GameSystem.HasReset -= OnReset;
        GameSystem.HasGameOver -= OnGameOver;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            audioManager.Play("ScratchLose");
            PlayerCaught?.Invoke();
        }
    }
}
