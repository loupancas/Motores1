using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechanger : MonoBehaviour
{
    public GameObject instances;

    KeyCode menu = KeyCode.Escape;

    public void Awake()
    {
        if(instances !=null && instances != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instances = this.gameObject;
        }
    }

    public void LoadPato()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadNano()
    {
        SceneManager.LoadScene(2);
    }

    public void Update()
    {
        if(Input.GetKeyDown(menu))
        {
            SceneManager.LoadScene(0);
        }

    }

    public void oops()
    {
        Application.Quit();
    }


}
