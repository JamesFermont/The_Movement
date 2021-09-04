using UnityEngine;

public class Warp : MonoBehaviour {
    public Transform target;

    private void OnTriggerEnter2D(Collider2D other) {
        Transform collidee = other.transform;
        Vector3 warpTarget = collidee.position;
        warpTarget.x = target.transform.position.x;
        collidee.position = warpTarget;
    }
}
