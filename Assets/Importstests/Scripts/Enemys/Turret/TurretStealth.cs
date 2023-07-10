using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TurretStealth : MonoBehaviour
{

    [Header("References")]
    public GameObject Target;
    public GameManager GameManager;
    public GameObject TurretObj;
    [SerializeField] protected AIState state;
    public GameObject bullet;
    public VisionAgent VisionAgent;

    [Header("Variables")]
    public float AggroRange;
    public float AggroAngle;
    public float DetectionTimer;
    [SerializeField] protected float FireRate;
    protected float pulsefire = 0;
    [SerializeField] protected float GuardRotateSpeed, ShootingRotateSpeed;

    [Header("Variables - guarding")]
    [SerializeField] float MaxGuardAngle;
    [SerializeField] float HitInfoRaycastLenght;
    public LayerMask Player;
    public RaycastHit HitInfo;
    public bool detectedplayer;

    Vector3 originalRot;
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.FindObjectOfType<GameManager>();
        Target = GameManager.playerInstance.gameObject;
        state = AIState.Idle;
        VisionAgent = GetComponentInChildren<VisionAgent>();

        originalRot = TurretObj.transform.localRotation.eulerAngles;
    }

    protected enum AIState //estado de la torreta
    {
        Idle,
        guarding,
        tracking,
        shooting,
        dead,
    }

    protected virtual void Update()
    {
        StateHandler();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (state == AIState.tracking || state == AIState.shooting && detectedplayer)
        {
            //girar la torreta al jugador
            TurnTurretToPlayer();
        }
        else if (detectedplayer == false)
        {
            state = AIState.guarding;
            Guardingmovement();
        }
        else if(detectedplayer == true)
        {
            state = AIState.tracking;
            TurnTurretToPlayer();
        }

    }

    protected virtual void StateHandler()
    {
        if (state == AIState.dead)
        {
            return;
        }
        // calculo de aggrorange
        float distanceaggro = Vector3.Distance(Target.transform.position, this.transform.position);

        if (distanceaggro >= AggroRange)
        {
            state = AIState.Idle;
            VisionAgent.TargetInRange = false;
        }
        else
        {
            state = AIState.guarding;
            VisionAgent.TargetInRange = true;
        }
    }
    protected virtual void TurnTurretToPlayer()
    {
        Quaternion Lookrotation = Quaternion.LookRotation(Target.transform.position - TurretObj.transform.position);

        TurretObj.transform.rotation = Quaternion.Slerp(this.transform.rotation,Lookrotation,ShootingRotateSpeed * Time.deltaTime);



        if (pulsefire > FireRate)
        {
            ShootTurret();
            state = AIState.shooting;
        }
        else
        {
            pulsefire = pulsefire + 1 * Time.deltaTime;
        }

    }

    
    public void Guardingmovement()
    {
        TurretObj.transform.localRotation = Quaternion.Euler(originalRot.x, Mathf.PingPong(Time.time * GuardRotateSpeed, MaxGuardAngle * 2) -MaxGuardAngle, 1f);

        /*if(Physics.Raycast(transform.position,TurretObj.transform.forward,out HitInfo,HitInfoRaycastLenght,Player))
        {
            print("he visto al player");
            

        }

        Debug.DrawLine(transform.position,HitInfo.point,Color.red);*/
       
    }

    protected virtual void ShootTurret()
    {
        print("bang del turret");

    }

    protected virtual void Deactivate()
    {
        state = AIState.dead;
    }




}

  
