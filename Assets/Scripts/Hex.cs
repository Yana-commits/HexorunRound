using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{
  
   public Vector3 endPosition;
    public bool permission = true;
    public bool end = true;
   
    void Start()
    {
        transform.SetParent(Controller.Instance.Map.transform);
        
    }


    public void Move(float[]points)
    {
        float y = points[Random.Range(0, 10)];
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
