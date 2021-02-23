using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Joystick joystick;
    private Rigidbody rigidbody;
    public float speed;
   private Animator animator;
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    
    void FixedUpdate()
    {
        transform.forward = rigidbody.velocity;
        rigidbody.velocity = new Vector3(-joystick.Vertical * speed, rigidbody.velocity.y, joystick.Horizontal * speed);
        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.magnitude));
    }
}
