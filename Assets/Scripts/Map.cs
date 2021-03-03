using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Material redMaterial;
    public List<Hex> hexes = new List<Hex>();
    private float changeTime;
    float[] points = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 1 };

    void Start()
    {
        changeTime = HUD.Instance.changesTime.value;
        //Init();
        hexes = Controller.Instance.hexes;
    }

    private void FixedUpdate()
    {
        if (Controller.Instance.gameState == GameState.doPlay)
        {
            changeTime = changeTime - 1 * Time.deltaTime;
            if (changeTime <= 0)
            {
                Debug.Log("Map");
                for (int i = 0; i < hexes.Count; i++)
                {
                    if (hexes[i].permission && hexes[i].end)
                    {
                        hexes[i].Move(points);
                    }
                }
                changeTime = HUD.Instance.changesTime.value;
            }
        }
    }

   

    public static Map Create(int width,int height,float xOffset,float zOffset ,int holesNomber,Hex hexPrefab, List<Hex> hexes)
    {
        Vector3 fieldPosition = Vector3.zero;

        var mapPrefab = Resources.Load<Map>("Prefabs/Map");


        var map = Instantiate(mapPrefab, fieldPosition, Quaternion.identity);

        int pointX = Random.Range(1, 2);
        int pointY = Random.Range(4, 15);
        float haight;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xPos = x * xOffset;

                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }

                bool isActive = true;
                int destiny = Random.Range(0, 100);

                if (holesNomber > 0 && destiny % 70 == 0)
                {
                    haight = -0.5f;
                    holesNomber--;
                    isActive = false;
                }
                else
                {
                    haight = 0;
                }
                var hex_go = Instantiate(hexPrefab, new Vector3(xPos, haight, y * zOffset), Quaternion.identity) as Hex;

                if (y % 2 == 0)
                {
                    hex_go.name = "Hex_" + (x * 2) + "_" + (y / 2);
                }
                else
                {
                    hex_go.name = "Hex_" + ((x * 2) + 1) + "_" + (y / 2);
                }

                //var cmp =  hex_go.GetComponent<Hex>();

                if (x == pointX && y == pointY)
                {
                    var rend = hex_go.GetComponent<Renderer>();
                    rend.materials = new[] { null, map.redMaterial };
                    /*
                    foreach (var material in rend.materials)
                    {
                        material.SetColor("Color", Color.red);
                        material.SetColor("Color 2", Color.red);
                    }    
                    */
                    hex_go.end = false;
                }

                if (isActive == false)
                {
                    hex_go.end = false;
                }

                //hex_go.transform.SetParent(this.transform);
                hexes.Add(hex_go);
            }
        }
       
        return map;
    }
}
