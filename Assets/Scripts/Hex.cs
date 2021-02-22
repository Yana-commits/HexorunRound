using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{
    int[] points = new int[3] { 0, 1, 2 };

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
        int y = points[Random.Range(0,3)] ;
        endPosition = new Vector3(transform.position.x, y, transform.position.z);
        transform.DOMove(endPosition, 2);
    }
}
