using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entities.WeaponHolder;
using UnityEngine.Events;

public class Player_Move : MonoBehaviour
{
    WeaponHolder weaponholder;

    [Header("movement")]
    public float Movespeed;
    public Transform Orientation;

    float horizontalimput;
    float verticalimput;

    Vector3 Movedirection;

    public float sprintspeed;
    public float crouchspeed;
    public float walkspeed;
    public float climbspeed;
    public float groundmult=10f;

    Rigidbody Rb;

    public float rotation;

    [Header("Eventos")]
    public UnityEvent FallingEvent;
    public UnityEvent StopFallingEvent;
    public UnityEvent WalkingEvent;
    public UnityEvent StopWalking;
    public UnityEvent SprintEvent;
    public UnityEvent JumpEvent;
    public UnityEvent StopSprint;

    [Header("keycodes")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;

    [Header("jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMult;
    [SerializeField] bool readyjump;

    [Header("crouching")]
    public float crouchYscale;
    public float baseYscale;

    [Header("slope handler")]
    public float maxSlopeAngle;
    private RaycastHit slopehit; //voy a usar un raycasta para detectar y calcular el angulo de subida
    [SerializeField] bool exitingslope;


    [Header("Ground Check")] //chequear que el personaje este en el suelo
    public float playerheight;

    public LayerMask ground;
    public LayerMask Walls;

    public bool grounded;
    [SerializeField] float grounddrag = 1;

    public Movementstate state; // variable enum, para guardar el estado del enum actual

    [SerializeField] bool entryimputs;

    [Header("references")]
    public Player_Climbing player_Climbing;
    public Player_Ledge_Grab ledge_Grab;
    public Transform modeltransform;
    public Animator animatore;
    public GameManager Manager;
    public SoundManager SoundManager;
    

    [Header("Movementsituation")]
    public bool sprinting = false;
    public bool crouching = false;

    public enum Movementstate //enum que se encarga de indicar el estado de movimiento del PJ
    {
        walking,
        sprinting,
        jumping,
        crouching,
        climbing,
        jumpwall,
        freeze,
        unlimited,
        idle,
    }

    public bool climbing;
    public bool freeze;
    public bool unlimited;

    public bool restricted;

    public void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true; //evitar rotacion :)
        ledge_Grab = GetComponent<Player_Ledge_Grab>();

        walkspeed = Movespeed;

        readyjump = true;

        baseYscale = transform.localScale.y; //guardar la escala Y base del jugador
        crouchYscale = baseYscale / 2;
        animatore = GetComponentInChildren<Animator>();

        
    }


    public void Update()
    {

        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, ground);
        Myinput();
        Statehandler();
        Clampvelocity();
        debuff();
        

        //drag (evitar que el PJ resbale )

        if (Onslope() && grounded)
        {
            Rb.drag = grounddrag + (8 * 2f);
        }
        else if (grounded && !Onslope())
        {
            Rb.drag = grounddrag;
        }
        else
        {
            Rb.drag = 0;
        }

        animatore.SetFloat("ejeX", horizontalimput);
        animatore.SetFloat("EjeY", verticalimput);
        if(grounded)
        {
            animatore.SetBool("onground",true);
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
        // print("velocidad"+Rb.velocity.magnitude);
        print("rotation" + rotation);
        falling();

    }

    private void Myinput() //recibir inputs
    {
        horizontalimput = Input.GetAxisRaw("Horizontal");
        verticalimput = Input.GetAxisRaw("Vertical");
        //jump
        if (Input.GetKey(jumpKey) && readyjump && grounded && !player_Climbing.exitingwall && !ensnared)
        {
            readyjump = false;
            
            jump();

            Invoke(nameof(resetjump), jumpCooldown); //llamar resetjump con un delay de cooldown (here be code dragons)
        }

        // start crouch

        if (Input.GetKeyDown(crouchKey) && !sprinting) //transformar el tamaño Y
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            Rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); //empujar al player al suelo :)
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey)) //detectar que el PJ se levanto
        {
            transform.localScale = new Vector3(transform.localScale.x, baseYscale, transform.localScale.z);
        }

        //test 
        if(Input.GetKeyDown(KeyCode.P))
        {

            debuffname = "ensnare";
            debuffduration= 1f;
            debuffpower= 1f;
            debuff();
            
        }
    }



    private void Statehandler() //para navegar los distintos estados del PJ
    {
        //estado - congelado
        if (freeze)
        {
            state = Movementstate.freeze;
            Rb.velocity = Vector3.zero;
            StopWalking.Invoke();
            StopSprint.Invoke();
        }

        // estado -  sin limites
        else if (unlimited)
        {
            state = Movementstate.unlimited;
            Movespeed = 999f;
            StopWalking.Invoke();
            StopSprint.Invoke();
            return;
           
        }

        //estado - trepar
        else if (climbing)
        {
            state = Movementstate.climbing;
            Movespeed = climbspeed;
            StopWalking.Invoke();
            StopSprint.Invoke();
        }

        //estado - saliendo trepar
        else if (climbing && player_Climbing.exitingwall)
        {
            state = Movementstate.jumpwall;
            StopWalking.Invoke();
            StopSprint.Invoke();
        }

        //estado - correr
        else if (grounded && Input.GetKey(sprintKey) && !crouching)
        {
            state = Movementstate.sprinting;
            Movespeed = sprintspeed;
            sprinting = true;
            crouching = false;
            SprintEvent.Invoke();
            StopWalking.Invoke();
            animatore.SetBool("movement",true);
            animatore.SetBool("running", true);
            animatore.SetBool("crouched", false);
            animatore.SetBool("jump", false);

        }
        //estado -agachado
        else if (grounded && Input.GetKey(crouchKey) && !sprinting && Rb.velocity != Vector3.zero)
        {
            state = Movementstate.crouching;
            Movespeed = crouchspeed;
            crouching = true;
            sprinting = false;

            if(Rb.velocity != Vector3.zero)
            {
                animatore.SetBool("movement", true);
                animatore.SetBool("running", false);
                animatore.SetBool("crouched", true);
                animatore.SetBool("jump", false);
            }
        }
        //estado - caminando
        else if (grounded && Rb.velocity != Vector3.zero)
        {
            state = Movementstate.walking;
            Movespeed = 7;
            WalkingEvent.Invoke();
            StopSprint.Invoke();
       

            crouching = false;
            sprinting = false;

            animatore.SetBool("movement", true);
            animatore.SetBool("running", false);
            animatore.SetBool("crouched", false);
            animatore.SetBool("jump", false);
        }
        else if(Rb.velocity==Vector3.zero)
        {
            state = Movementstate.idle;
            StopWalking.Invoke();
            StopSprint.Invoke();
            if (crouching)
            {
                animatore.SetBool("movement", false);
                animatore.SetBool("running", false);
                animatore.SetBool("crouched", true);
                animatore.SetBool("jump", false);
            }
            else
            {
                animatore.SetBool("movement", false);
                animatore.SetBool("running", false);
                animatore.SetBool("crouched", false);
                animatore.SetBool("jump", false);
            }
        }
        //estado-en el aire
        else
        {
            state = Movementstate.jumping;
            StopWalking.Invoke();
            StopSprint.Invoke();
            animatore.SetBool("movement", false);
            animatore.SetBool("running", false);
            animatore.SetBool("crouched", false);
            animatore.SetBool("jump", true);
        }


    }

    bool isidle;
    private void PlayerMove() //movimiento del player
    {
        if (player_Climbing.exitingwall || restricted)
        {
            return;
        }



        //calcular la direccion de movimiento en el suelo
        Movedirection = Orientation.forward * verticalimput + Orientation.right * horizontalimput;

        if (Onslope() && !exitingslope) //modificar movimiento para aplicarse en angulo
        {
            Rb.AddForce(GetSlopeProjectAngle(Movedirection) * Movespeed * (4f*groundmult), ForceMode.Force);

            if (Rb.velocity.y > 0) //solucionar un problema causado por desactivar gravedad
            {
                Rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        //en el suelo
        else if (grounded)
        {
            Rb.AddForce(Movedirection.normalized * Movespeed * (groundmult), ForceMode.Force);
        }
        //en el aire
        else if (!grounded)
        {
            Rb.AddForce(Movedirection.normalized * Movespeed * 10f * airMult, ForceMode.Force);
        }

        Rb.useGravity = !Onslope(); //desactivar gravedad para solucionar un problema con raycasting y angulos raros

        

       
    }

    private void Clampvelocity() //para evitar que la velocidad supere el maximo admitido
    {

        if (Onslope() && !exitingslope)//solucionar aceleracion en slopes
        {
            if (Rb.velocity.magnitude > Movespeed)
            {
                Rb.velocity = Rb.velocity.normalized * Movespeed;
            }
        }
        else
        {
            Vector3 flatvelocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

            //limitar velocidad maxima

            if (flatvelocity.magnitude > Movespeed)
            {
                Vector3 limitedvel = flatvelocity.normalized * Movespeed; //basicamente reduzco la velocidad actual a la limitada :) (menos Y)

                Rb.velocity = new Vector3(limitedvel.x, Rb.velocity.y, limitedvel.z);
            }

        }


    }

    private void jump() //salto del PJ
    {
        if (player_Climbing.exitingwall||ledge_Grab.exitingledge || ledge_Grab.holdingledge)
        {
            return;
        }

        exitingslope = true;

       

        //resetear la velocidad Y del Pj
        Rb.velocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

        Rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


    }

    private void resetjump() //resetear salto 
    {
        readyjump = true;

        exitingslope = false;
    }

    public bool Onslope()//booleano para detectar que estamos tocando una rampa (here de code dragons)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopehit, playerheight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopehit.normal);
            return angle < maxSlopeAngle && angle != 0;

        }

        return false;
    }

    public Vector3 GetSlopeProjectAngle(Vector3 direction) //proyectar el angulo de movimiento en el angulo de la rampa, para permitir que se mueva correctamente (here be code dragons)
    {
        return Vector3.ProjectOnPlane(direction, slopehit.normal).normalized;



    }

    private void falling()
    {
        if(Rb.velocity.y < -0.1f)
        {
            animatore.SetBool("falling", true);

            if(Rb.velocity.y < -1.0F)
            {
                FallingEvent.Invoke();
            }
        }
        else
        {
            StopFallingEvent.Invoke();
            animatore.SetBool("falling",false);
            return;
        }
    }


    [Header("Debuffs")]
    public bool ensnared;
    public string debuffname;
    public float debuffduration, debuffpower;

    public void Snare(bool active)
    {
        groundmult = active ? groundmult * (0.2f) : 10f ;
    }


    float debufftimer, debuffpulse;
    public void debuff()
    {

        switch (debuffname)
        {

            case "ensnare":
                debufftimer = debuffduration;
                if (debuffpulse <= debufftimer)
                {
                    debuffpulse += Time.deltaTime;
                    
                    ensnared= true;
                }
                else
                {
                    debuffpulse = 0;
                    
                    ensnared = false;
                    debuffname = null; break;
                }


                break;

            default:
                debuffname = null;
                return;
                
        }



    }


}
