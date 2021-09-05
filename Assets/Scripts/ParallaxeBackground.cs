using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxeBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier, 0f);
        lastCameraPosition = cameraTransform.position;
    }
}
