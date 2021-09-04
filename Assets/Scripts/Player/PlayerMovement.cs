using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator playerAnimator;

    [Space]

    [SerializeField] private int speed;

    void Update()
    {
        //Move Character
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0 , 0);

        //Flip Character
        Vector3 characterScale = transform.localScale;

        if(Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
        }

        transform.localScale = characterScale;

        //control Player_Run Animation
        playerAnimator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        playerAnimator.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
    }
}
