using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    [SerializeField] Material redMaterial;
    public List<Hex> hexes = new List<Hex>();
    private float changeTime = 1;
    float[] points = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 1 };
    float[] minusePoints = new float[10] { -3, -3, -3, -3, -3, -3, -3, -3, -3, -3 };

    int gridSize = 10;
    float hexLength = 0.9755461f;
    
    private float holesNomber;

    private Vector2Int size;

    public Hex this[Vector3Int index] => hexes[ConvertToArrayIndex(index)];
    public Hex this[Vector2Int index] => hexes[ConvertToArrayIndex(index)];

    private int ConvertToArrayIndex(Vector3Int index) => ConvertToArrayIndex(ConvertCoordToAxial(index));
    private int ConvertToArrayIndex(Vector2Int index) => index.x * size.x + index.y;


    void Start()
    {
        holesNomber = HUD.Instance.holes.value;
        
    }

    private void Initializie(LevelParameters level, Hex hexPrefab)
    {
        var hexHeightDistance = (hexLength*2);
        var hexWidthDistance = Mathf.Sqrt(3) * hexLength ;
        var rowlengthAddition = 0;

        //size = new Vector2Int(level.ZWidth, level.XHeight);

        var gridStart = new Vector3(0, 0, 0);
        //float z = 0;
        //float x = 0;

        //for (int r = 0; r < (gridSize * 2) + 1 - Mathf.Abs(gridSize - r); r++)
        //{
        //    for (int q = 0; q < gridSize - r; q++)
        //    {
        //        var position = new Vector3(r, 0, q + z);
        //        var hex_go = Instantiate(hexPrefab, position, Quaternion.identity);


        //        hex_go.name = "Hex_" + q + "_" + r;
        //    }

        //    //if (r >= gridSize - 1)
        //    //{

        //    //    y += hexWidthDistance / 2 ;
        //    //   x += (hexHeightDistance * 0.75f) / 2 ;
        //    //}
        //    //else
        //    //{
        //    x += hexWidthDistance / 2;
        //    z += (hexHeightDistance * 0.75f) / 2 / 1.7f;
        //    //}
        //}

        for (int q = -gridSize; q <= gridSize; q++)
        {
            int r1 = Mathf.Max(-gridSize, -q - gridSize);
            int r2 = Mathf.Min(gridSize, -q + gridSize);
            for (int r = r1; r <= r2; r++)
            {
                Vector3Int cube = new Vector3Int(q, r, -q - r);
                 Vector2Int n = new Vector2Int(q, r);
                var hex_go = Instantiate(hexPrefab);
                hex_go.transform.SetParent(transform);


                hex_go.transform.localPosition = Hexagonal.Cube.HexToPixel(
                    cube,
                    Vector2.one * hexLength/2f); ;

                hex_go.index =  Hexagonal.Offset.QFromCube(cube);
                hex_go.name = "Hex_" + q + "_" + r;
                hex_go.cube_coord = cube;

                //Debug.Log($"{cube}");
                //Debug.Log($"{n}");
                //Debug.Log($"{hex_go.index}");

            }
        }

        return;
        for (int r = 0; r < (gridSize * 2) - 1; r++)
        {
            for (int q = 0; q < gridSize + rowlengthAddition; q++)
            {

                var position = gridStart;

                var hex_go = Instantiate(hexPrefab, position, Quaternion.identity);


                hex_go.transform.position = new Vector3(position.z, 0, (position.x += (hexWidthDistance / 2 * q)));
                hex_go.index = new Vector2Int(q, r);
                hex_go.name = "Hex_" + q + "_" + r;
                hex_go.cube_coord = Hexagonal.Offset.RToCube(hex_go.index);

                //hex_go.neihbours = GetNeighbour(hex_go.cube_coord);
                //Debug.Log($"{hex_go.neihbours.ToArray()[1]}");

                hex_go.transform.SetParent(transform);
                hexes.Add(hex_go);
            }

            if (r >= gridSize - 1)
            {
                rowlengthAddition -= 1;
                gridStart.z += hexWidthDistance / 2 / 1.15f;
                gridStart.x += (hexHeightDistance * 0.75f) / 2 / 1.7f;
            }
            else
            {
                rowlengthAddition += 1;
                gridStart.z += hexWidthDistance / 2 / 1.15f;
                gridStart.x -= (hexHeightDistance * 0.75f) / 2 / 1.7f;
            }
        }

        //var target = hexes
        //    .Where(h => h.cube_coord.z > size.x - 10)
        //    .OrderBy(v => Random.value)
        //    .First();


        //var rend = target.GetComponent<Renderer>();
        //rend.materials = new[] { null, redMaterial, redMaterial };
        //target.end = false;
    }

    private void FixedUpdate()
    {
        //MooveHexes();
    }

    public static Map Create(LevelParameters level, Hex hexPrefab)
    {
        Vector3 fieldPosition = Vector3.zero;
        var mapPrefab = Resources.Load<Map>("Prefabs/Map");
        

        var map = Instantiate(mapPrefab, fieldPosition, Quaternion.identity);
        map.Initializie(level, hexPrefab);
        
        return map;
    }

    public void MooveHexes()
    {
        if (Controller.Instance.gameState == GameState.doPlay)
        {
            changeTime = changeTime - 1 * Time.deltaTime;
            if (changeTime <= 0)
            {
                var ignorHexArray = hexes
                    .Where(h => h.permission == false || !h.end).
                    SelectMany(h => GetNeighbour(h.cube_coord))
                    .Select(ind => this[ind]);

                ignorHexArray = hexes.Where(h => h.permission == false || !h.end).Union(ignorHexArray);

                foreach (var item in ignorHexArray)
                {
                    item.Move(new float[] { 0, 0 });
                }

                var tt = hexes.Except(ignorHexArray);
                foreach (var item in tt)
                {
                    item.Move(points);
                }

                var list = hexes.Where(x => x.state == HexState.NONE).Except(ignorHexArray).ToList();

                for (int i = 0; i < holesNomber; i++)
                {
                    int index = Random.Range(0, list.Count);

                    if (list[index].state != HexState.NONE)
                    {
                        i--;
                    }
                    else
                    {
                        list[index].Move(minusePoints);
                    }
                }
                //changeTime = HUD.Instance.changesTime.value;
                changeTime = 2;
            }
        }
    }

    static Vector3Int ToCube(int xPos, int yPos)
    {
        var x = xPos;
        var z = yPos;
        var y = -x - z;

        return new Vector3Int(x, y, z);
    }

    private Vector2Int ConvertCoordToAxial(Vector3Int index)
    {
        var q = index.x;
        var r = index.z;
        return new Vector2Int(q, r);
    }


    private IEnumerable<Vector3Int> _directions
    {
        get
        {
            yield return new Vector3Int(1, -1, 0);
            yield return new Vector3Int(1, -2, 1);
            yield return new Vector3Int(0, 1, -1);

            yield return new Vector3Int(-1, 1, 0);
            yield return new Vector3Int(-1, 0, 1);
            yield return new Vector3Int(0, -1, 1);
        }
    }

    public IEnumerable<Vector3Int> GetNeighbour(Hex hex)
        => GetNeighbour(hex.cube_coord);

    public IEnumerable<Vector3Int> GetNeighbour(Vector3Int index)
        => _directions.Select(v => index + v);

}
