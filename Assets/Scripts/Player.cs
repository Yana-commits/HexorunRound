using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    protected Joystick joystick;
    private Rigidbody rigidbody;
    public float speed;
   private Animator animator;
    public float xMin, xMax, zMin, zMax;
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        transform.position = new Vector3(22f, 0.03f, 2.5f);
        Controller.Instance.Win += Win;
        Controller.Instance.Loose += Loose;

    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(-joystick.Vertical * speed, rigidbody.velocity.y, joystick.Horizontal * speed);

        rigidbody.velocity = velocity;

        if (velocity.magnitude > 0)
            rigidbody.rotation = Quaternion.LookRotation(rigidbody.velocity);

        rigidbody.position = new Vector3
            (
            Mathf.Clamp(rigidbody.position.x, xMin, xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, zMin, zMax)
            );

        animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.magnitude));
    }
    public void Win()
    {
        StartCoroutine(Winner());
    }
    private IEnumerator Winner()
    {
        Debug.Log("Win!");
        speed = 0;
        animator.SetTrigger("Win");
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Loose()
    {
        StartCoroutine(Looser());
    }
    private IEnumerator Looser()
    {
        Debug.Log("Loose!");
        speed = 0;
        animator.SetTrigger("Loose");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        Controller.Instance.Win -= Win;
        Controller.Instance.Loose -= Loose;
    }
}
