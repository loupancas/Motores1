using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VisionAgent : MonoBehaviour
{
    [Header("references")]
    public GameManager Manager;
    public GameObject Target;
    public UnityEvent Detected,LostDetection;
    public TurretStealth TurretScript;
    public CentreofMass TargetCentre;
    [SerializeField] Transform LineOfSightPivot;
    public LayerMask player;

    [Header("variables")]
    [SerializeField] float Viewangle;
    [SerializeField] float Viewdistance;
    public bool TargetInRange;
    [SerializeField] float DetectionTimer;
    float detectionpulse;


    public void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        TurretScript = GetComponentInParent<TurretStealth>();
        Target = Manager.playerInstance;

        Viewdistance = TurretScript.AggroRange;
        Viewangle = TurretScript.AggroAngle;
        DetectionTimer = TurretScript.DetectionTimer;
        
    }

    public void Update()
    {
        if(Target == null) // check target existente
        {
            Debug.Log("vision detector target is null ");
            return;
        }

        bool b;

        if(TargetInRange == false) //check target esta cerca del objeto
        {
            b = false; //fracaso check
            detectionpulse = 0f;
            TurretScript.detectedplayer = false;
            LostDetection.Invoke();
            return;
        }

        b = Checkangle(); //check target esta a cierto angulo del objeto

        if(b==false)
        {
            detectionpulse = 0f;
            TurretScript.detectedplayer = false;
            LostDetection.Invoke();
            return;
        }

        b = LineOfSight(); // CHECK target esta visible

        if (b == false)
        {
            detectionpulse = 0f;
            TurretScript.detectedplayer = false;
            LostDetection.Invoke();
            return;
        }

        if (b == true)
        {
           if(detectionpulse < DetectionTimer)
           {
                detectionpulse = detectionpulse + 1 * Time.deltaTime;

           }
           else
           {
                Detected.Invoke();
                TurretScript.detectedplayer = true;
                
           }
 
        }


    }

    bool Checkangle()
    {
        Vector3 targetdirection = Target.transform.position-this.transform.position;
        float targetangle = Vector3.Angle(targetdirection, transform.forward);
        return targetangle < Viewangle;
    }

    bool LineOfSight()
    {
        TargetCentre = Target.GetComponentInChildren<CentreofMass>();

        Vector3 direction = (TargetCentre.CentrePoint.position - LineOfSightPivot.position).normalized;
        Ray ray = new Ray(LineOfSightPivot.position, direction);

        RaycastHit rayhit;
        Debug.DrawRay(transform.position, direction,Color.red);
        if(Physics.Raycast(ray, out rayhit,player))
        {
            if(rayhit.transform== Target.transform)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        //dibujo un gizmo para saber el angulo de deteccion

        Gizmos.DrawWireSphere(this.transform.position, Viewdistance); // creo esfera de aggro

        Quaternion rotation = Quaternion.AngleAxis(Viewangle, Vector3.up); //obtengo angulo en base a un eje (Y)
        Vector3 endpoint = this.transform.position + (rotation * this.transform.forward * Viewdistance); // obtengo distancia maxima
        
        Gizmos.DrawLine(transform.position, endpoint); // dibujo linea 1

        // lo mismo que arriba solo que dibujo la linea contrarea
        rotation = Quaternion.AngleAxis(- Viewangle, Vector3.up);
        endpoint = this.transform.position + (rotation * this.transform.forward * Viewdistance);

        Gizmos.DrawLine(transform.position, endpoint);
    }

}
