using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    public static HUD Instance
    {
        get
        {
            return instance;
        }

    }

    public Slider timer;

    public Slider playerSpeed;

    public Slider changesTime;

    public Slider areaFactor;

    public Slider cameraFactor;

    public Slider holes;

    public Button startButton;

    public GameObject startPanel;

    public GameObject gameplayPanel;

    public Text timeText;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startButton.onClick.AddListener(() => StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreValue(float value)
    {
        timeText.text = Mathf.Round(value).ToString();
    }

    private void StartGame()
    {
        startPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        Controller.Instance.Game();
    }
}
