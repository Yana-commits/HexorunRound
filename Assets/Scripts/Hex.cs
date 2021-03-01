using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hex : MonoBehaviour
{
    float[] points = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0.5f, 1 };


    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool permission = true;
    public bool end = true;
    private float changeTime;

    void Start()
    {
        //changeTime = HUD.Instance.changesTime.value;
        //StartCoroutine(HexBehavor());
    }

    private IEnumerator HexBehavor()
    {
        while (permission)
        {
            if (!end)
            {
                break;
            }

            if (permission)
            {
                Move();
            }
            yield return new WaitForSeconds(changeTime);
        }
    }

    public void Move()
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
            if (!end)
            {
                //StopAllCoroutines();
                Controller.Instance.Victory();

            }
            permission = false;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        permission = true;
       
    }

}
