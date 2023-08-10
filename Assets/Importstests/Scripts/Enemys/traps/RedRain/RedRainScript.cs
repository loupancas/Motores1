using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedRainScript : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] GameObject PlayerObj;
    [SerializeField] Player PlayerScript;
    [SerializeField] Vector3 currposition;
    public bool CanRun;
    [SerializeField]float TickAmount;
    [SerializeField]int DMGtick;
    float pulse;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
        PlayerObj = manager.playerInstance;
        PlayerScript = manager.player;
    }


    public void Update()
    { 
        if(!CanRun) 
        {
            return;
        }


        currposition = new Vector3(PlayerObj.transform.position.x , this.transform.position.y, PlayerObj.transform.position.z);

        this.transform.position = currposition;

        if(pulse < TickAmount)
        {
            pulse = pulse + 1 * Time.deltaTime;
        }
        else
        {

            PlayerScript.TakeDamage(DMGtick);
            pulse = 0;
        }

    }

    public void UpdateBool(bool input)
    {
        CanRun = input;
    }


}
