using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Turret : MonoBehaviour
{
    [Header("References")]
    public GameObject Target;
    public GameManager GameManager;
    public GameObject TurretObj;
    [SerializeField]protected AIState state;
    public GameObject bullet;

    [Header("Variables")]
    [SerializeField] protected float AggroRange;
    [SerializeField] protected float FireRate;
    protected float pulsefire = 0;
    
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.FindObjectOfType<GameManager>();
        Target = GameManager.playerInstance.gameObject;
        state = AIState.Idle;
    }

    protected enum AIState //estado de la torreta
    {
        Idle,
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
        if (state == AIState.tracking || state == AIState.shooting) 
        {
            TurnTurret();
        }
       
    }

    protected virtual void StateHandler()
    {
        if(state == AIState.dead)
        {
            return;
        }
        // calculo de aggrorange
        float distanceaggro = Vector3.Distance(Target.transform.position, this.transform.position);

        if (distanceaggro >= AggroRange)
        {
            state = AIState.Idle;
        }
        else
        {
            state = AIState.tracking;
        }
    }
    protected virtual void TurnTurret()
    {
        Quaternion Lookrotation = Quaternion.LookRotation(Target.transform.position - TurretObj.transform.position);

        TurretObj.transform.rotation = Lookrotation;

        

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

    protected virtual void ShootTurret()
    {
        print("bang del turret");

    }

    protected virtual void Deactivate()
    {
        state = AIState.dead;
    }
}
