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
    private bool once = true;
    //[SerializeField]
    //private Map map;

    //[SerializeField]
    //private Player player;
    [SerializeField]
    private CameraController camera;

    public GameState gameState = GameState.doNotPlay;

    private Map map;
    public Map Map
    {
        get
        {
            return map;
        }

        set
        {
            map = value;
        }
    }

    public Hex hexPrefab;

    //public int zWidth = 30;
    //public int xHeight = 20;

    //float xOffset = 0.753f;
    //float zOffset = 0.868f;

    public List<Hex> hexes = new List<Hex>();
   
    //private int holesNomber = 5;

    [SerializeField]
    private LevelParameters level;
    public LevelParameters Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    private int koeff = 2;

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
                    if (once)
                    {
                        Loose();
                        once = false;
                    }
                    gameTime = 0;
                }
            }
        }
    }
    public void Game()
    {
        gameState = GameState.doPlay;

        InitializeLevel(koeff);
       
        map = Map.Create(level, hexPrefab, hexes);
        PlayerInit(level);
    }
    public void PlayerInit(LevelParameters level)
    {
        int xHeight = level.XHeight;
        float xOffset = level.XOffset;

        float playerX = xHeight * xOffset / 2;
        var player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(playerX, 0.03f, 1f), Quaternion.identity);
        camera.player = player.transform;
    }

    public void InitializeLevel(int koeff)
    { 
    level = new LevelParameters(koeff);
    }
   
    public void Victory()
    {
        Win();
        stopTime = false;
        gameState = GameState.doNotPlay;
    }
}
