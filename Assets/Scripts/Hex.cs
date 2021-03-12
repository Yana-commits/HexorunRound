using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{

    public Vector3 endPosition;
    public bool permission = true;
    public bool end = true;
    //public bool hole = true;
    public Vector3Int cube_coord;
    public HexState state = HexState.NONE;

   

    public void Move(float[]points)
    {
        float y = points[Random.Range(0, 10)];
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
                if (!end)
                {
                    Controller.Instance.Victory();
                }
                //else if (!hole)
                //{
                //    Controller.Instance.Lost();
                //}
                permission = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (player)
            {
                permission = true;
            }
        }
    }
}

public enum HexState
{
    NONE,
    UP,
    DOWN
}
