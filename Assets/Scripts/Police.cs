using System;
using UnityEngine;

public class Police : MonoBehaviour {
    public static event Action PlayerCaught;
    public float WalkTime = 2f;

    private float liveTime;
    
    private void Update() {
        liveTime += Time.deltaTime;
        transform.position += Vector3.right * Time.deltaTime * (liveTime > WalkTime ? 10.5f : 2.5f);
    }

    private void OnEnable() {
        liveTime = 0f;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerCaught?.Invoke();
            Debug.Log("We gottem Sir!");
        }
    }
}
