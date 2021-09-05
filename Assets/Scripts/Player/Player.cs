using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [Space]
    public Animator playerAnimator;
    private Animator manholeAnimator;
    [Header("Movement")]
    public int speed;
    //[Header("Dance")]
    public static bool dancing = false;
    public static event Action<bool> DancingStateChanged;
    [Header("Hide")]
    public bool canHide = false;
    public bool hidden = false;

    public bool movementEnabled = true;

    private GameObject ghettoblaster;

    void Start()
    {
        ghettoblaster = transform.Find("Ghettoblaster").gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Dance();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Hide();
        }
        
        if(hidden == true || dancing == true)
        {
            movementEnabled = false;
            playerAnimator.speed = 1;
        }
        else
        {
            movementEnabled = true;
            //playerAnimator.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
        }
    }

    void Dance()
    {
        dancing = !dancing;
        DancingStateChanged?.Invoke(dancing);

        playerAnimator.SetBool("IsDancing", dancing);

        ghettoblaster.SetActive(dancing);

        if(dancing == true)
        {
            audioManager.Play("Dance");
            audioManager.Pause("BGM");
        }
        else
        {
            audioManager.Play("BGM");
            audioManager.Pause("Dance"); 
        }
    }

    void Hide()
    {
        if(canHide == true)
        {
            hidden = !hidden;

            manholeAnimator.SetBool("Open", hidden);
            playerAnimator.SetBool("Hide", hidden);

            audioManager.Play("Manhole");}
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        manholeAnimator = collision.gameObject.GetComponent<Animator>();
        canHide = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        canHide = false;
    }
}
