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

    public Text timeText;
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreValue(float value)
    {
        timeText.text = Mathf.Round(value).ToString();
    }
}
