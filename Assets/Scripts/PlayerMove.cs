using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 18f;
    private bool isFacingRight = true;
    public Text seasonTxt;

    public bool doubleJump, isTouching, isGrabbed;
    Collider2D objCollider2D;
    bool isStop = false;

    int season = 0; //1 = Fall, 2 = Winter, 3 = Spring, 4 = Summer

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platfromLayer;
    [SerializeField] private Animator animator;

    private void Start()
    {
        SeasonManager.instance.onSeasonChange += ChangeSeason;
        InGameTracker.instance.onStateChange += ChangeState;
    }

    private void OnDisable()
    {
        SeasonManager.instance.onSeasonChange -= ChangeSeason;
        InGameTracker.instance.onStateChange -= ChangeState;
    }

    void Update()
    {
        if (isStop)
        {
            horizontal = 0;
            rb.velocity = Vector2.zero;
            return;
        }

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

        if (Input.GetKeyDown("e") && isTouching && !isGrabbed && season == 3) 
        {
            objCollider2D.gameObject.GetComponent<GrabableScript>().ExecuteInteractable(transform, true);
            isGrabbed = true;
        }else if(Input.GetKeyDown("e") && isGrabbed)
        {
            objCollider2D.gameObject.GetComponent<GrabableScript>().ExecuteInteractable(transform, false);
            isGrabbed = false;
            objCollider2D = null;
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

    private bool isPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, platfromLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {

            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);

        }

        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    private void ChangeSeason(Season _season)
    {
        season = (int)_season;

        if (isGrabbed)
        {
            objCollider2D.gameObject.GetComponent<GrabableScript>().ExecuteInteractable(transform, false);
            isGrabbed = false;
            objCollider2D = null;
        }

        switch (season)
        {
            case 0: //Winter
                season = 2;
                speed = 6f;
                animator.SetBool("isFall", false);
                animator.SetBool("isSpring", false);
                transform.localScale = new Vector3(1f, 1f, 0);
                break;
            case 1://Spring
                season = 3;
                speed = 4f;
                animator.SetBool("isFall", false);
                animator.SetBool("isSpring", true);
                transform.localScale = new Vector3(2f, 2f, 0);
                break;
            case 2://Summer
                season = 2;
                speed = 8f;
                animator.SetBool("isFall", false);
                animator.SetBool("isSpring", false);
                transform.localScale = new Vector3(1f, 1f, 0);
                break;
            case 3://Fall
                season = 1;
                speed = 10f;
                animator.SetBool("isFall", true);
                animator.SetBool("isSpring", false);
                transform.localScale = new Vector3(0.6f, 0.6f, 0);
                break;
            default:
                break;
        }
    }

    void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                isStop = false;
                break;
            case GameState.Dialogue:
                isStop=true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushedObject"))
        {
            isTouching = true;
            objCollider2D = collision;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PushedObject"))
        {
            isTouching = false;
            //objCollider2D = null;
        }
    }
}

