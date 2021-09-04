using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float timeOffset;
    [SerializeField] private Vector2 positionOffset;

    // Update is called once per frame
    void Update() 
    {
        Vector3 startPosition = transform.position;

        Vector3 endPosition = player.transform.position;
        endPosition.x += positionOffset.x;
        endPosition.y += positionOffset.y;
        endPosition.z = -10;

        transform.position = Vector3.Lerp(startPosition, endPosition, timeOffset * Time.deltaTime);
    }
}
