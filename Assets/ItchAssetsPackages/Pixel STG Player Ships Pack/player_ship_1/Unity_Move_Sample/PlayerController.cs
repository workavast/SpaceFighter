using System.Collections;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector2 moveDirection;
    private float rightTimer = 0f;
    private float leftTimer = 0f;
    private bool isCheckingLeft = false;
    private bool isCheckingRight = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // void OnMove(InputValue value)
    // {
    //     moveDirection = value.Get<Vector2>();
    //     if (moveDirection.x > 0.1)
    //     {
    //         animator.SetBool("isRight", true);
    //         animator.SetBool("isLeft", false);
    //     }
    //     else if (moveDirection.x < -0.1)
    //     {
    //         animator.SetBool("isRight", false);
    //         animator.SetBool("isLeft", true);
    //     }
    //     else
    //     {
    //         animator.SetBool("isRight", false);
    //         animator.SetBool("isLeft", false);
    //     }
    // }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveDirection * speed * Time.deltaTime);
    }

    void Update()
    {
        if (animator.GetBool("isRight"))
        {
            rightTimer += Time.deltaTime;
            if (rightTimer >= 0.3f && rightTimer <= 0.6f)
            {
                StartCoroutine(CheckLeft());
            }
        }
        else
        {
            rightTimer = 0f;
            isCheckingLeft = false;
        }

        if (animator.GetBool("isLeft"))
        {
            leftTimer += Time.deltaTime;
            if (leftTimer >= 0.3f && leftTimer <= 0.6f)
            {
                StartCoroutine(CheckRight());
            }
        }
        else
        {
            leftTimer = 0f;
            isCheckingRight = false;
        }
    }

    IEnumerator CheckLeft()
    {
        if (!isCheckingLeft)
        {
            isCheckingLeft = true;
            float startTime = Time.time;
            bool isLeftTriggered = false;
            while (Time.time - startTime < 0.2f)
            {
                if (animator.GetBool("isLeft"))
                {
                    isLeftTriggered = true;
                }
                yield return null;
            }
            if (isLeftTriggered)
            {
                animator.SetBool("roll_left", true);
                yield return new WaitForSeconds(0.5f);
                animator.SetBool("roll_left", false);
            }
            isCheckingLeft = false;
        }
    } 

    IEnumerator CheckRight()
    { 
       if (!isCheckingRight) 
        { 
            isCheckingRight = true; 
            float startTime = Time.time;
            bool isRightTriggered = false;
            while (Time.time - startTime < 0.2f)
            {
                if (animator.GetBool("isRight"))
                {
                    isRightTriggered = true;
                }
                yield return null;
            }
            if (isRightTriggered)
            { 
                animator.SetBool("roll_right", true); 
                yield return new WaitForSeconds(0.5f); 
                animator.SetBool("roll_right", false); 
            } 
            isCheckingRight = false;
        }
   } 
}