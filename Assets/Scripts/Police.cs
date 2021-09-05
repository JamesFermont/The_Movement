using System;
using UnityEngine;

public class Police : MonoBehaviour {
    public static event Action PlayerCaught;
    public float WalkTime = 2f;

    private float liveTime;

    private Animator policeAnimator;

    private AudioManager audioManager;

    private void Start()
    {
        policeAnimator = GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update() {
        liveTime += Time.deltaTime;
        transform.position += new Vector3(transform.localScale.x, 0f, 0f) * Time.deltaTime * (liveTime > WalkTime ? 10.5f : 2.5f);
        policeAnimator.SetFloat("Speed", liveTime > WalkTime ? 10.5f : 2.5f);
    }

    private void OnEnable() {
        liveTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            audioManager.Play("ScratchLose");
            PlayerCaught?.Invoke();
            Debug.Log("We gottem Sir!");
        }
    }
}
