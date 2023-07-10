using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Ledge_Grab : MonoBehaviour
{

    [Header("references")]
    public Player_Move player_move;
    public Transform orientation;
    public Transform ledgecamera;
    public Player_Climbing player_Climbing;
    [SerializeField] Rigidbody Rb;

    [Header("ledge grab")]
    [SerializeField] float movetoledgespeed; //velocidad de movimiento para agarrarte a un ledge
    [SerializeField] float maxlengthgrabledge; //rango maximo para agarrarte a un ledge

    [SerializeField] float minimumtimeledge; //tiempo minimo de agarre para permitir movimiento
    private float currenttimeledge;

    public bool holdingledge; //bool para ver si estas agarrado a un ledge

    [Header("ledge jump")]
    public KeyCode ledgejump = KeyCode.Space;
    [SerializeField] float ledgejumpforwardforce;
    [SerializeField] float ledgejumpUpforce;

    [Header("exiting")]

    public bool exitingledge;
    [SerializeField] float exitledgetime;
    private float exitledgetimer;

    [Header("ledge Detection")]
    [SerializeField] float ledgedetectionlength;
    [SerializeField] float ledgespherecastradius;
    public LayerMask Ledges;

    private Transform lastledge;
    private Transform currentledge;

    private RaycastHit ledgehit;

    private void Start()
    {
        player_Climbing= GetComponent<Player_Climbing>();
    }

    private void Update()
    {
        LedgeDetection();

        StateMachine();
    }

    private void StateMachine()
    {
        float horizontalinput = Input.GetAxisRaw("Horizontal");
        float verticalinput = Input.GetAxisRaw("Vertical");

        bool anyinputpressed = horizontalinput != 0 || verticalinput != 0; // check que no haya actividad en inputs

        //estado 1 - Holding to Ledge
        if (holdingledge)
        {
            FreezeRigidBody();

            currenttimeledge += Time.deltaTime;

            if (currenttimeledge > minimumtimeledge && anyinputpressed) //check si paso un tiempo minimo entre que el jugador se agarro a un ledge y presiono una accion
            {
                LeaveledgeHold();
            }

            if (Input.GetKeyDown(ledgejump))
            {
                ledgeJumping();
            }
        }

        //estado salir del ledge
        else if(exitingledge)
        {
            if (exitledgetimer > 0) exitledgetimer -= Time.deltaTime;
            else exitingledge = false;

        }    
        

    }

    private void LedgeDetection()//detectar un ledge

    {
        //guardar el dato y chequear si es correcto que estamos viendo un ledge
        bool ledgedetected = Physics.SphereCast(transform.position, ledgespherecastradius, ledgecamera.forward, out ledgehit, ledgedetectionlength, Ledges);

        if (!ledgedetected) //return para evitar conflictos
        {
            return;
        }


        //calcular la distancia entre el player y el ledge detectado
        float distancetoledge = Vector3.Distance(transform.position, ledgehit.transform.position);

        if (ledgehit.transform == lastledge) // chequear que el transform del ledge no sea el mismo que el anterior
        {
            return;
        }

        if (distancetoledge < maxlengthgrabledge && !holdingledge) //check de distancia para ejecutar un ledgegrab
        {
            EnterLedgeHold();
        }
    }

   private void ledgeJumping()
   {
        LeaveledgeHold();

        Invoke(nameof(DelayedLedgeJumo),0.1f);
   }

    private void DelayedLedgeJumo()
    {

        Vector3 forcetojump = ledgecamera.forward * ledgejumpforwardforce + orientation.up * ledgejumpUpforce;
        Rb.velocity = Vector3.zero;

        print(forcetojump);


        Rb.AddForce(forcetojump, ForceMode.Impulse);
    }



    private void EnterLedgeHold() //comenzar a agarrarte a un ledge
    {
        holdingledge = true;

        //player_move.unlimited = true;
        player_move.restricted = true;

        player_move.grounded = true;

        currentledge = ledgehit.transform; //guardar posicion del ledge
        lastledge = ledgehit.transform;

        Rb.useGravity = false; //desactivar gravedad
        Rb.velocity = Vector3.zero; //matar momento

    }

    private void FreezeRigidBody() //congelar Rigidbody
    {
        Rb.useGravity = false;
        Vector3 directiontoledge = currentledge.position - transform.position; // calcular la direccion hacia el ledge detectado
        float distancetoledge = Vector3.Distance(transform.position, currentledge.position); // calcular la distancia hacia ledge detectado

        if(distancetoledge > 1f) //check distancia
        {
            if(Rb.velocity.magnitude < movetoledgespeed) //check para agregar velocidad al RB. 
            {
                Rb.AddForce(directiontoledge.normalized * movetoledgespeed * 1000f * Time.deltaTime);
            }
           
            
        }
        else //el PJ agarro un ledge
        {
            if (!player_move.freeze)
            {
                player_move.freeze = true;
                
            }
            if(player_move.unlimited) player_move.unlimited = false; // evitar que el movimiento no se vea afectado por la velocidad de Rigid
        }

        if(distancetoledge > maxlengthgrabledge) // check para soltar un ledge en caso de algun problema
        {
            LeaveledgeHold();
        }


    }

    private void LeaveledgeHold() // salir de un ledge
    {
        exitingledge = true;
        exitledgetimer = exitledgetime;

        holdingledge = false;
        currenttimeledge = 0f;

        player_move.restricted = false;
        //player_move.unlimited = false;
        player_move.freeze = false;
        player_move.grounded = false;

        Rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f);
    }

    private void ResetLastLedge()
    {
        lastledge = null;
    }


}
