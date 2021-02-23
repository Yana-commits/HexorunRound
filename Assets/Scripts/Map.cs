using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject hexPrefab;

    int width = 16;
    int height = 20;

    float xOffset = 1.505f;
    float zOffset = 0.434f;

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
                float xPos = x*xOffset;

                if (y % 2 == 1)
                {
                    xPos += xOffset/2f;
                }
              GameObject hex_go =  (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0, y*zOffset ), Quaternion.identity);

                hex_go.name = "Hex_" + x + "_" + y;
                hex_go.GetComponent<Hex>();
                hex_go.transform.SetParent(this.transform);
            }
        }
    }
}
