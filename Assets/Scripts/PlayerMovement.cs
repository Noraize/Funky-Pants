using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject Screen;
    [SerializeField] float walkSpeed;
    Rigidbody2D rigid;
    private float xAxis;
    private float yAxis;
    [SerializeField] float jumpForce;
    [SerializeField] Animator anim;
    private string currentAnimaton;
    private bool isJumpPressed;
    //private int groundMask;
    private bool isGrounded, Check = false;
    //Animation States

    [SerializeField] float HOR;

    const string PLAYER_IDLE = "idle";
    const string PLAYER_RUN = "running";
    const string PLAYER_JUMP = "jump";
    const string PLAYER_DUCK = "duck";

    [SerializeField] Transform playerray;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //groundMask = 1 << LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //Checking for inputs
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        //Debug.Log("veolicty " + rigid.horizontal);
        HOR = rigid.velocity.x;
        //space jump key pressed?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundMask);
        RaycastHit2D hit = Physics2D.Raycast(playerray.position, Vector2.down, 1f);

        //Debug.Log(hit.collider.gameObject);
        if (hit.collider.tag == "Ground")
        {
            Debug.DrawLine(playerray.position, hit.transform.position, Color.red);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //Check update movement based on input
        Vector2 vel = new Vector2(0, rigid.velocity.y);

        if (xAxis < 0)
        {
            vel.x = -walkSpeed;
            transform.localScale = new Vector2(-1, 1);
        }
        else if (xAxis > 0)
        {
            vel.x = walkSpeed;
            transform.localScale = new Vector2(1, 1);

        }
        else
        {
            vel.x = 0;

        }

        if (isGrounded)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {

                //ChangeAnimationState(PLAYER_DUCK);
                anim.SetTrigger("duck");
            }
            // if (Input.GetKeyUp(KeyCode.DownArrow))
            // {
            //     anim.SetTrigger("duck");
            // }
        }

        if (isJumpPressed && isGrounded)
        {
            //rigid.velocity += jumpForce * Vector2.up;
            //rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rigid.AddForce(new Vector2(0, jumpForce));
            isJumpPressed = false;
            anim.SetTrigger("jmp");
            if (Check == false)
            {
                Screen.GetComponent<SFX>().JumpFunction();
                Check = true;
            }
        }

        //assign the new velocity to the rigidbody
        rigid.velocity = vel;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
        Check = false;
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetTrigger("idleback");
        }
    }
}

