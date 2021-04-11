using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float Speed = 3f;
    public float jumpHeight = 3f;
    public float RotationSpeed = 10f;

    CharacterController controller;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionMove = new Vector3(0,0, Input.GetAxisRaw("Vertical"));
        transform.Rotate(transform.up * Input.GetAxisRaw("Horizontal") * RotationSpeed * Time.deltaTime);

        directionMove = transform.TransformVector(directionMove);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            directionMove.y += Mathf.Sqrt(jumpHeight * 3.0f);
        }

        if (directionMove!= Vector3.zero)
        {
            animator.SetBool("Run",true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        controller.Move(directionMove * Speed * Time.deltaTime);
    }
}
