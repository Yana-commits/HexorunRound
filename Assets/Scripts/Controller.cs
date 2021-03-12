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
    //private Player player;
    [SerializeField]
    private CameraController perspCamera;

    [SerializeField]
    private CameraController ortoCamera;

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

    private int koeff ;

    private float camKoeff;

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
                        Loose?.Invoke();
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
        koeff = (int)HUD.Instance.areaFactor.value;
        camKoeff = HUD.Instance.cameraFactor.value;
        if (camKoeff == 1)
        {
            perspCamera.gameObject.SetActive(false);
            ortoCamera.gameObject.SetActive(true);
            camera = ortoCamera;
        }
        else 
        {
            ortoCamera.gameObject.SetActive(false);
            perspCamera.gameObject.SetActive(true);
            camera = perspCamera;
        }
        InitializeLevel(koeff);
       
        map = Map.Create(level, hexPrefab);
        PlayerInit(level);
    }
    public void PlayerInit(LevelParameters level)
    {
        int xHeight = level.XHeight;
        float xOffset = level.XOffset;

        float playerX = xHeight * xOffset / 2;
        var player = (GameObject)Instantiate(Resources.Load("Prefabs/Player"), new Vector3(playerX, 0.03f, 3f), Quaternion.identity);
        camera.player = player.transform;
    }

    public void InitializeLevel(int koeff)
    { 
      level = new LevelParameters(koeff);
    }
   
    public void Victory()
    {
        Win?.Invoke();
        stopTime = false;
        gameState = GameState.doNotPlay;
    }
    public void Lost()
    {
        Loose?.Invoke();
        stopTime = false;
    }
}
