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
    private bool stopTime = true;

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
        if (stopTime)
        {
            gameTime = gameTime - 1 * Time.deltaTime;

            HUD.Instance.UpdateScoreValue(gameTime);

            if (gameTime <= 0)
            {
                Loose();
                gameTime = 0;
            }
        }
    }
   
    public void Victory()
    {
        Win();
        stopTime = false;
    }
}
