using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player player;

    [Space]

    private int speed;

    private void Start()
    {
        speed = player.speed;
    }

    void Update()
    {
        if(player.dancing == false)
        {
            //Move Character
            transform.position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

            //Flip Character
            Vector3 characterScale = transform.localScale;

            if (Input.GetAxis("Horizontal") < 0)
            {
                characterScale.x = -1;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                characterScale.x = 1;
            }

            transform.localScale = characterScale;
        }

        //control Player_Run Animation
        player.playerAnimator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        player.playerAnimator.speed = Mathf.Abs(Input.GetAxis("Horizontal"));
    }
}
