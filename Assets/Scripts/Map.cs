using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject hexPrefab;

    int width = 20;
    int height = 20;

    float oddRowXOffset = 0.5f;
    float zOffset = 0.45f;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y <height; y++)
            {
                float xPos = x;

                if (y % 2 == 1)
                {
                    xPos += oddRowXOffset;
                }
                Instantiate(hexPrefab, new Vector3(xPos*1.53f, 0, y * zOffset), Quaternion.identity);
            }
        }
    }
}
