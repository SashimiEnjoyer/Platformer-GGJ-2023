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

    int season = 0; //1 = Fall, 2 = Winter, 3 = Spring, 4 = Summer

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        SeasonManager.instance.onSeasonChange += ChangeSeason;
    }

    private void OnDisable()
    {
        SeasonManager.instance.onSeasonChange -= ChangeSeason;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Debug.Log(doubleJump);
        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            animator.SetBool("IsJumping", false);
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && season != 3)
        {
            animator.SetBool("IsJumping", true);
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                if(season == 2)
                {
                    doubleJump = !doubleJump;
                }
            }
        }

        if (Input.GetKeyDown("f"))
        {
            season = 1;
            transform.localScale = new Vector3(0.6f, 0.6f, 0);
            seasonTxt.text = season.ToString("F0");
        }
        if (Input.GetKeyDown("g"))
        {
            season = 2;
            transform.localScale = new Vector3(1f, 1f, 0);
            seasonTxt.text = season.ToString("F0");
        }
        if (Input.GetKeyDown("h"))
        {
            season = 3;
            transform.localScale = new Vector3(2f, 2f, 0);
            seasonTxt.text = season.ToString("F0");
        }
        if (Input.GetKeyDown("j"))
        {
            season = 4;
            transform.localScale = new Vector3(1f, 1f, 0);
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

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void ChangeSeason(Season _season)
    {
        season = (int)_season;

        switch (season)
        {
            case 1: //Winter
                break;
            case 2://Spring
                break;
            case 3://Summer
                break;
            case 4://Fall
                break;
            default:
                break;
        }
    }
}

