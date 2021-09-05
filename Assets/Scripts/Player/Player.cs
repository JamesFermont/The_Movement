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
    private bool canDance;
    public static bool dancing = false;
    public static event Action<bool> DancingStateChanged;
    [Header("Hide")]
    private bool canHide = false;
    public bool hidden = false;
    [SerializeField] private AnimationCurve jumpCurve;

    public bool movementEnabled = true;

    private GameObject ghettoblaster;

    float timer = 0f;
    float jumpTime = 0.5f;
    float lerpRatio;
    Vector3 LerpOffset;

    void Start()
    {
        ghettoblaster = transform.Find("Ghettoblaster").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(canDance == true)
            {
                Dance();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (canHide == true)
            {
                Hide();

                timer = 0f;
                lerpRatio = 0f;
                LerpOffset = Vector3.zero;
                StartCoroutine(Jump());
            }
        }

        if (hidden == true)
        {
            movementEnabled = false;
            canDance = false;
            canHide = true;
        }
        else if (dancing == true)
        {
            movementEnabled = false;
            canDance = true;
            canHide = false;
        }
        else
        {
            movementEnabled = true;
            canDance = true;
        }

        timer += Time.deltaTime;

        lerpRatio = timer / jumpTime;
    }

    void Dance()
    {
        dancing = !dancing;
        DancingStateChanged?.Invoke(dancing);

        playerAnimator.SetBool("IsDancing", dancing);

        ghettoblaster.SetActive(dancing);

        if (dancing == true)
        {
            audioManager.Play("Dance");
            audioManager.Play("Mumble");
            audioManager.Pause("BGM");
            audioManager.Pause("Pedestrian");
        }
        else
        {
            audioManager.Play("BGM");
            audioManager.Play("Pedestrian");
            audioManager.Pause("Dance");
            audioManager.Pause("Mumble");
        }
    }

    void Hide()
    {
        hidden = !hidden;

        manholeAnimator.SetBool("Open", hidden);
        playerAnimator.SetBool("Hide", hidden);

        audioManager.Play("Manhole");
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

    IEnumerator Jump()
    {
        while (lerpRatio <= 1)
        {
            LerpOffset = new Vector3(0f, jumpCurve.Evaluate(lerpRatio) * 0.1f, 0f);
            transform.position = transform.position + LerpOffset;
            yield return null;
        }
    }
}
