using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //TPFinal - Lourdes Pando - Enum para los estados del juego, sobre todo pausa y reanudar
    public static GameManager instance;
    public GameObject playerInstance;
    public Player player;
    private GameState state;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject); para usarse en escenas posteriores si se requiere
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    
    
    public Transform camPos;
    public Transform weaponpos;
    
    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    public Player GetPlayer()    //para que el enemigo lo use 
    {
        return player;
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
    

    public void Start()
    {
        state = GameState.Initializing;
        Invoke("StartGame",0.1f);

    }

    public void StartGame()
    {
        player.subscribeToDeath(onPlayerDeath);
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].Initialize();
        }
        state = GameState.Playing;
    }

    void onPlayerDeath()
    {
        SceneManager.LoadScene("GameOver");
        state = GameState.GameOver;
    }


    bool pause = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && state == GameState.Playing)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.P) && state == GameState.Paused)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (state == GameState.Playing)
        {
            Time.timeScale = 0f;
            state = GameState.Paused;
        }
    }

    public void ResumeGame()
    {
        if (state == GameState.Paused)
        {
            Time.timeScale = 1f;
            state = GameState.Playing;
        }
    }
    public void Pause(bool pause)
    {
        if(pause)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Pause();
            }

        }
        else
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Resume();
            }
        }
    }

    List<PlayObject> objects = new List<PlayObject>();
    public void AddPlayObject(PlayObject element)
    {
        if (!objects.Contains(element))
        {
            objects.Add(element);
        }
        
    }
    public void RemovePlayObject(PlayObject element)
    {
        if (objects.Contains(element)) 
        {
            objects.Remove(element);
        }

    }



}

public enum GameState
{
    Initializing,
    Playing,
    Paused,
    GameOver
}

