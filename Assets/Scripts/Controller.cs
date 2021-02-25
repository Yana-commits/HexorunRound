using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private static Controller instance;

    public static Controller Instance
    {
        get
        {
            return instance;
        }
    }

    public float gameTime;

    public delegate void WinDelegate();
    public event WinDelegate Win;
    public delegate void LooseDelegate();
    public event LooseDelegate Loose;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this && instance != null) Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
       gameTime = gameTime -1 * Time.deltaTime;

        if (gameTime <= 0)
        {
           Debug.Log("Loose");
           Loose();
        }
    }
    public void Victory()
    {
        Win();
    }
}
