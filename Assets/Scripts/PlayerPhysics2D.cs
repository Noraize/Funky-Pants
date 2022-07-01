using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics2D : MonoBehaviour
{
    [SerializeField] private Transform characterRoot;
    [SerializeField] private Transform characterPivot;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float maxTorque = 500;
    [SerializeField] private float torqueAcceleration = 250;
    private float torque = 0;
    [SerializeField] private float maxAngularVelocity = 5000;
    [SerializeField] private float backflipSpeed = 0.75f;
    [SerializeField] private float xMultiplier = 1.5f;
    [SerializeField] private float yMultiplier = 2f;
    [SerializeField] private float rampMultiplier = 1.3f;
    [SerializeField] private Rigidbody2D rigid;


    private int moveInput = 0;
    private int direction = 1;
    private bool isGrounded = false;
    private bool isWallJumping = false;
    private int wallCollision = 0;
    private bool waitForJumpReleased = false;
    private bool isJumping = false;
    private float jumpTimer = 0;
    private bool jumpStartTriggered = false;
    private bool jumpEndTriggered = false;
    private bool slidingInput = false;
    private bool isRollingBack = false;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D circleCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = characterRoot.GetComponentInChildren<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        Collider2D[] cols = characterRoot.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D col in cols)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }
    }
    
    private void FixedUpdate()
    {
        characterRoot.position = transform.position;

        if (rb.angularVelocity > maxAngularVelocity)
        {
            rb.angularVelocity = maxAngularVelocity;
        }
        else if (rb.angularVelocity < -maxAngularVelocity)
        {
            rb.angularVelocity = -maxAngularVelocity;
        }

        RotateFace();

        CheckGroundBelow();

        CheckGroundBeside();
    }

    private void Update()
    {
        if (jumpEndTriggered)
        {
            waitForJumpReleased = false;
        }

        if (isGrounded)
        {
            if (isWallJumping)
            {
                WallInput();
            }
            else
            {
                GroundInput();
            }

            if (jumpStartTriggered && !waitForJumpReleased)
            {
                isJumping = true;
                jumpTimer = 0;
            }
        }
        else
        {
            AirInput();
            AirPhysics();
        }

        JumpingBehaviour();

        anim.SetFloat("speedX", rb.velocity.sqrMagnitude);
        anim.SetFloat("speedY", rb.velocity.y);
        anim.SetFloat("torque", Mathf.Abs(torque));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isSliding", slidingInput && Mathf.Abs(rb.velocity.x) > 5);
        anim.SetBool("isRollingBack", isRollingBack && !isWallJumping);
        anim.SetBool("isWallGrabbing", isWallJumping && wallCollision != 0);
    }
    public float velocityx(){
        return Mathf.Abs(rb.velocity.x);
    }
    private void RotateFace()
    {
        Vector3 newRotEuler = characterPivot.localRotation.eulerAngles;
        // newRotEuler.y = Mathf.Lerp(newRotEuler.y, direction == 1 ? 0 : 180, 8 * Time.deltaTime);
        // characterPivot.localRotation = Quaternion.Euler(newRotEuler);
        if (direction > 0)
        {
            newRotEuler.y = 0;
        }
        else if (direction < 0)
        {
            newRotEuler.y = 180;
        }
        characterPivot.localRotation = Quaternion.Euler(newRotEuler);
    }

    private void JumpingBehaviour()
    {
        if (isJumping)
        {
            Vector2 vel = rb.velocity;

            if (isWallJumping)
            {
                waitForJumpReleased = true;



                if ((moveInput == 1 && wallCollision == -1) ||
                    (moveInput == -1 && wallCollision == 1))
                {
                    // xMultiplier = 1.5f;
                    // yMultiplier = 2f;
                }

                vel.x = 10f * -1 * wallCollision * xMultiplier;
                vel.y = 10f * yMultiplier;
                direction = -wallCollision;
            }
            else
            {
                vel.y = 10 * yMultiplier;
            }

            if (jumpEndTriggered)
            {
                vel = rb.velocity;
                vel.y /= 2;
                isJumping = false;
                jumpEndTriggered = false;
            }

            jumpTimer += Time.deltaTime;
            rb.velocity = vel;

            if (jumpTimer >= 0.2f)
            {
                waitForJumpReleased = true;
                isJumping = false;
            }
            isWallJumping = false;
        }
        jumpStartTriggered = false;
        jumpEndTriggered = false;
    }

    private void AirPhysics()
    {
        bool facingDirection = (direction > 0 && rb.velocity.x > 0) || (direction < 0 && rb.velocity.x < 0);
        if (rb.velocity.x > 5)
        {
            characterRoot.Rotate(new Vector3(0, 0, backflipSpeed * 360 * Time.deltaTime * (facingDirection ? 0 * rb.velocity.x : -1)));
        }
        else if (rb.velocity.x < -5)
        {
            characterRoot.Rotate(new Vector3(0, 0, backflipSpeed * 360 * Time.deltaTime * (facingDirection ? 0 * rb.velocity.x : 1)));
        }
    }

    private void WallInput()
    {
        if ((moveInput == -1 && wallCollision == -1) ||
            (moveInput == 1 && wallCollision == 1))
        {
            Vector2 vel = rb.velocity;
            vel.y = Mathf.Lerp(vel.y, -1, 10 * Time.deltaTime);
            rb.velocity = vel;
        }
        else if ((moveInput == 1 && wallCollision == -1) ||
            (moveInput == -1 && wallCollision == 1))
        {

        }
    }

    private void GroundInput()
    {
        isRollingBack = false;

        if (!slidingInput)
        {
            if (moveInput == 1 && wallCollision != 1)
            {
                direction = 1;
                if (torque > 0) torque = 0;
                torque -= torqueAcceleration * Time.deltaTime;
                if (torque < -maxTorque) torque = -maxTorque;
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
            else if (moveInput == -1 && wallCollision != -1)
            {
                direction = -1;
                if (torque < 0) torque = 0;
                torque += torqueAcceleration * Time.deltaTime;
                if (torque > maxTorque) torque = maxTorque;
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
            else
            {
                torque = 0;
                //Check roll back animation
                if ((direction == -1 && rb.velocity.x > 1) ||
                    (direction == 1 && rb.velocity.x < -1))
                {
                    isRollingBack = true;
                }
            }
        }
        else
        {
            if (direction == 1 && wallCollision != 1)
            {
                torque += torqueAcceleration * Time.deltaTime;
                if (torque > 0) torque = 0;
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
            else if (direction == -1 && wallCollision != -1)
            {
                torque -= torqueAcceleration * Time.deltaTime;
                if (torque < 0) torque = 0;
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
        }


    }

    private void AirInput()
    {
        if (moveInput == 1 && wallCollision != 1)
        {
            if (torque > 0) torque = 0;
            torque -= torqueAcceleration * Time.deltaTime;
            if (torque < -maxTorque) torque = -maxTorque;
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
        else if (moveInput == -1 && wallCollision != -1)
        {
            if (torque < 0) torque = 0;
            torque += torqueAcceleration * Time.deltaTime;
            if (torque > maxTorque) torque = maxTorque;
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D cont in collision.contacts)
        {
            if (((1 << cont.collider.gameObject.layer) & groundLayerMask) != 0)
            {
                SetGrounded(cont.normal, false);
            }
            else if (((1 << cont.collider.gameObject.layer) & wallLayerMask) != 0)
            {
                SetGrounded(cont.normal, true);
            }
        }
    }

    private void CheckGroundBeside()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, characterRoot.right * 1, circleCollider.radius + 0.1f, wallLayerMask);
        Debug.DrawLine(transform.position, transform.position + characterRoot.right * 1 * (circleCollider.radius + 0.1f), Color.yellow);
        if (hit)
        {
            wallCollision = 1;
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, characterRoot.right * -1, circleCollider.radius + 0.1f, wallLayerMask);
            Debug.DrawLine(transform.position, transform.position + characterRoot.right * -1 * (circleCollider.radius + 0.1f), Color.yellow);
            if (hit)
            {
                wallCollision = -1;
            }
            else
            {
                wallCollision = 0;
            }
        }
    }

    private void CheckGroundBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, characterRoot.up * -1, circleCollider.radius + 0.01f, groundLayerMask);
        Debug.DrawLine(transform.position, transform.position + characterRoot.up * -1 * (circleCollider.radius + 0.01f), Color.cyan);
        if (hit)
        {
            SetGrounded(hit.normal, false);
        }
        else
        {
            isGrounded = false;
        }
    }

    private void SetGrounded(Vector2 _normal, bool _isWall)
    {
        isGrounded = true;
        isJumping = false;
        Quaternion newRot = characterRoot.rotation;
        newRot = Quaternion.FromToRotation(Vector3.up, _normal);
        if (_isWall)
        {
            if (newRot.eulerAngles.z >= 90 && newRot.eulerAngles.z <= 270)
            {
                isWallJumping = true;
                newRot = Quaternion.FromToRotation(Vector3.up, Vector2.zero);
                characterRoot.rotation = Quaternion.Lerp(characterRoot.rotation, newRot, 20 * Time.deltaTime);
                return;
            }
        }
        isWallJumping = false;
        characterRoot.rotation = Quaternion.Lerp(characterRoot.rotation, newRot, 10 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ramp")
        {
            maxAngularVelocity = maxAngularVelocity*rampMultiplier;
            //rigid.gravityScale = rigid.gravityScale - 3f;
        }
        else if(other.gameObject.tag == "Ramp1")
        {
            maxAngularVelocity = maxAngularVelocity*(rampMultiplier+0.8f);
            //rigid.gravityScale = rigid.gravityScale - 3f;
        }
        else if(other.gameObject.tag == "Ramp2")
        {
            maxAngularVelocity = maxAngularVelocity*(rampMultiplier+1.2f);
            //rigid.gravityScale = rigid.gravityScale - 3f;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ramp"){
            maxAngularVelocity = maxAngularVelocity / rampMultiplier;
            //rigid.gravityScale = rigid.gravityScale + 3f;
        }
        else if(other.gameObject.tag == "Ramp1"){
            maxAngularVelocity = maxAngularVelocity / (rampMultiplier+0.8f);
            //rigid.gravityScale = rigid.gravityScale + 3f;
        }
        else if(other.gameObject.tag == "Ramp2"){
            maxAngularVelocity = maxAngularVelocity*(rampMultiplier-1.2f);
            //rigid.gravityScale = rigid.gravityScale + 3f;
        }
    }
    public void SetMovementDirection(int _direction)
    {
        moveInput = _direction;
    }
    public void JumpStart()
    {
        jumpStartTriggered = true;
    }
    public void JumpEnd()
    {
        jumpEndTriggered = true;
    }
    public void SlidingStart()
    {
        slidingInput = true;
    }
    public void SlidingEnd()
    {
        slidingInput = false;
    }
}
