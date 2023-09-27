using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switchlevel : MonoBehaviour
{
    public int LoadLevelID;
    GameManager manager;
    GameObject PLAYER;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        PLAYER = manager.playerInstance.gameObject;
    }


    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject == PLAYER || other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(LoadLevelID);
        }

    }


}
