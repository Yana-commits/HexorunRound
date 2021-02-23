using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{
   float[] points = new float[3] { 0, 0.5f, 1 };

    private Vector3 startPosition;
    private Vector3 endPosition;
  
    void Start()
    {
        StartCoroutine(HexBehavor());
    }

    private IEnumerator HexBehavor()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Move();
        }
    }

    public void Move()
    {
        float y = points[Random.Range(0,3)] ;
        endPosition = new Vector3(transform.position.x, y, transform.position.z);
        transform.DOMove(endPosition, 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
