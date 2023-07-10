using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartFallTrigger : MonoBehaviour
{

    public string PlayerTag;
    public UnityEvent PlayerIntro;
    [SerializeField] bool Singleton = true;
    public void OnTriggerEnter(Collider other)
    {
        if (Singleton)
        {
            if (other.gameObject.tag == PlayerTag)
            {
                PlayerIntro.Invoke();

                Singleton = false;

            }
        }
       

    }


}
