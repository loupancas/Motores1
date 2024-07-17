using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour, ICollectible
{
    //public GameObject other;
    public void Collect()
    {      
        gameObject.SetActive(false);
    }

    //public void ActiveWeapon()
    //{
    //    if (other.CompareTag("Telekinesis"))
    //    {
    //        Telekinesis telekinesis = other.GetComponent<Telekinesis>();
    //        telekinesis.enabled = true;
    //        telekinesis.GetComponent<MeshRenderer>().enabled = true;
    //    }
    //    else if (other.CompareTag("ShootGun"))
    //    {
    //        Shoot shootGun = other.GetComponent<Shoot>();
    //        shootGun.enabled = true;
    //        shootGun.GetComponent<MeshRenderer>().enabled = true;
    //    }
      
    //}


}



