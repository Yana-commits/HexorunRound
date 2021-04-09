using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{

    public Vector3 endPosition;
    public bool permission = true;
    public bool end = true;
    public bool neihbourship = true;
    public Vector3Int cube_coord;
    public HexState state = HexState.NONE;
    public IEnumerable<Vector3Int> neihbours;
    public Vector2Int index;



    public void Move(float[]points)
    {
        float y = points[Random.Range(0, points.Length)];
        state = y == 0 ? HexState.NONE : (y == -3 ? HexState.DOWN : HexState.UP);
        endPosition = new Vector3(transform.position.x, y, transform.position.z);
        if (transform != null)
            transform?.DOMove(endPosition, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (player)
            {
                //if (!end)
                //{
                //    Controller.Instance.Victory();
                //}
                //for (int i = 0; i < neihbours.ToArray().Length; i++)
                //{
                //    Debug.Log("Player " + cube_coord + " Neihbour  " +i +" "+ neihbours.ToArray()[i]);
                //}
             
                //permission = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {   
                permission = true;        
        }
    }

}

public enum HexState
{
    NONE,
    UP,
    DOWN
}
