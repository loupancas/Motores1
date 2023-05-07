using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{
    [Header("References")]
    public Transform playerorientation;
    public Transform playerobj;
    private Rigidbody rb;
    private Player_Move player_Move;

    [Header("sliding")]
    [SerializeField] float maxslidetime;
    [SerializeField] float slideforce;
    private float slideduration;

    public float slideYscale;
    private float baseYscale;

    [Header("keycodes")]

    public KeyCode slidekey;
    private float horizontalinput;
    private float verticalinput;

    private bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_Move= GetComponent<Player_Move>();

        baseYscale= playerobj.transform.localScale.y;//guardo tamaño original del RB
    }

    private void Update()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal"); //inputs
        verticalinput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slidekey)&& (horizontalinput !=0 || verticalinput != 0  )) //chequeo si tengo imputs correctos
        {
            StartSlide();
        }

        if(Input.GetKeyUp(slidekey)&& isSliding)
        {
            StopSlide();
        }

    }

    private void FixedUpdate()
    {
        if(isSliding) 
        {
            SlidingMovement(); //llamo movimiento del slide
        }

    }


    private void StartSlide()
    {
        isSliding = true;

        //calculos para reducir el tamaño del PJ y empujarlo al suelo
        playerobj.localScale = new Vector3(playerobj.localScale.x, slideYscale, playerobj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideduration = maxslidetime;
    }

    private void SlidingMovement()
    {
        Vector3 inputdirection = playerorientation.forward * verticalinput + playerorientation.right * horizontalinput;


        //deslizar suelo llano
        if(!player_Move.Onslope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(inputdirection.normalized * slideforce, ForceMode.Force);

            slideduration -= Time.deltaTime;
        }

        //deslizar por una pendiente 
        else
        {
            rb.AddForce(player_Move.GetSlopeProjectAngle(inputdirection) * (slideforce*5f), ForceMode.Force);
        }

            if(slideduration <= 0 )
            {
            StopSlide(); 
            }
    }

    private void StopSlide()
    {
        isSliding =false;

        playerobj.localScale= new Vector3(playerobj.localScale.x, baseYscale, playerobj.localScale.z);
    }

}





