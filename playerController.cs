using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float Speed = 3f;
    public float jumpHeight = 3f;
    bool isJump = false;
    float currentJumpHeight = 0;
    public float RotationSpeed = 10f;

    Rigidbody controller;
    float distToGround;
    Animator animator;


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0,0.2f,0), -Vector3.up, distToGround-1f);
    }

    bool isOnWall()
    {
        return Physics.Raycast(transform.position, transform.TransformVector(Vector3.forward), distToGround - 0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformVector(Vector3.forward), Color.red, distToGround - 0.5f);

        Vector3 directionMove = new Vector3(0,0, Input.GetAxisRaw("Vertical"));
        transform.Rotate(transform.up * Input.GetAxisRaw("Horizontal") * RotationSpeed * Time.deltaTime);
        

        if (directionMove!= Vector3.zero)
        {
            animator.SetBool("Run",true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        Debug.Log(isOnWall());
        if (IsGrounded())
        {
            animator.SetBool("Wall", false);
            animator.SetBool("Jump", false);

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                animator.SetBool("Jump", true);
                controller.AddForce(new Vector3(0,jumpHeight,0),ForceMode.Impulse);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("Attack");
            }
        }
        else if(isOnWall())
        {
            directionMove = Vector3.zero;
            animator.SetBool("Wall", true);

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jumping");
                animator.SetBool("Jump", true);
                
                controller.AddForce(transform.TransformVector(new Vector3(0, jumpHeight, -2))/2, ForceMode.VelocityChange);
            }
        }
        else
        {
            animator.SetBool("Wall", false);
        }


        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Ekard_Attack_01_h"))
        {
            directionMove = Vector3.zero;
        }

        transform.Translate(directionMove * Speed * Time.deltaTime,Space.Self);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position+transform.forward+transform.up, 1);
    }


}
