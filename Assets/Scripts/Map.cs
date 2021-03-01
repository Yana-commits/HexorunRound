using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Hex hexPrefab;

    int width = 16;
    int height = 20;

    float xOffset = 1.505f;
    float zOffset = 0.434f;

    public List<Hex> hexes = new List<Hex>();
    private float changeTime;
    private bool permission = true;
    void Start()
    {
        changeTime = HUD.Instance.changesTime.value;
        Init();
    }

    private void FixedUpdate()
    {
        if (Controller.Instance.gameState == GameState.doPlay)
        {
            changeTime = changeTime - 1 * Time.deltaTime;
            if (changeTime <= 0)
            {
                for (int i = 0; i < hexes.Count; i++)
                {
                    if (permission)
                    {
                        hexes[i].Move();
                    }
                    
                }
                changeTime = HUD.Instance.changesTime.value;
            }
        }
           
    }

    public void Init()
    {
        int pointX = Random.Range(1, 2);
        int pointY = Random.Range(4, 15);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;

                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }
                var hex_go = Instantiate(hexPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity)as Hex;

                hex_go.name = "Hex_" + x + "_" + y;
                //var cmp =  hex_go.GetComponent<Hex>();
                if (x == pointX && y == pointY)
                {
                    hex_go.GetComponent<MeshRenderer>().material.color = Color.red;
                    hex_go.end = false;
                }
                hex_go.transform.SetParent(this.transform);
                hexes.Add(hex_go);
            }
        }
    }
}
