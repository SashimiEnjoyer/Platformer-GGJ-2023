using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Text seasonTxt;

    private bool doubleJump;

    int season = 0; //1 = spring, 2 = summer, 3 = fall, 4 = winter

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(doubleJump);
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                if(season == 1)
                {
                    doubleJump = !doubleJump;
                }
            }
        }

        if (Input.GetKeyDown("f"))
        {
            season = 1;
            seasonTxt.text = season.ToString("F0");
        }
        if (Input.GetKeyDown("g"))
        {
            season = 2;
            seasonTxt.text = season.ToString("F0");
        }
        if (Input.GetKeyDown("h"))
        {
            season = 3;
            seasonTxt.text = season.ToString("F0");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        //animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }
}

