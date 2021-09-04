using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Animator playerAnimator;

    public int speed;
    public bool dancing = false;

    public static event Action<bool> DancingStateChanged;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dance();
        }
    }

    void Dance()
    {
        dancing = !dancing;
        DancingStateChanged?.Invoke(dancing);

        playerAnimator.SetBool("IsDancing", dancing);
    }

    void Hide()
    {

    }
}
