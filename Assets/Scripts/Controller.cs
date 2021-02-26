using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { doPlay, doNotPlay };
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
    [SerializeField]
    private Map map;

    [SerializeField]
    private Player player;

    public GameState gameState = GameState.doNotPlay;

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
        gameTime = HUD.Instance.timer.value;
    }

    void FixedUpdate()
    {
        if (gameState == GameState.doPlay)
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
       
    }
    public void Game()
    {
        gameState = GameState.doPlay;
        map.Init();
        player.Init();
    }
   
    public void Victory()
    {
        Win();
        stopTime = false;
        gameState = GameState.doNotPlay;
    }
}
