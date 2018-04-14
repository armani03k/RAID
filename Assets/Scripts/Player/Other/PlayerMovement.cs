using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float Speed;
    public float JumpForce;
    public float DefaultJumpForce;

    [Header("Dash")]
    public float DashSpd;
    public float DashDuration;
    private bool dashAnim = false;

    Animator anim;
    Rigidbody2D rb;

    #region movement
    private bool isMoving = false;

    private bool canDash = true;
    public bool SetDash {
        set {
            canDash = value;
        }
    }
    public bool GetDash { get { return canDash; } }

    private bool canJump = true;
    public bool CheckJump {
        get {
            return canJump;
        }
    }
    public bool SetJump {
        set {
            canJump = value;
        }
    }
    #endregion

    #region Other
    private bool faceRight = true;
    public bool SetFaceDir {
        get {
            return faceRight;
        }
        set {
            faceRight = value;
        }
    }

    private bool grounded = true;
    public bool GetGround {
        get {
            return grounded;
        }
    }
    public bool SetGround {
        set {
            grounded = value;
        }
    }
    #endregion

    void Start () {
        JumpForce = DefaultJumpForce;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        SetGround = true;
	}

    void Update() {
        //sprite flipping
        if (Input.GetAxisRaw("Horizontal") > 0 && !faceRight && canDash) {
            Flip();
            faceRight = true;
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && faceRight && canDash) {
            Flip();
            faceRight = false;
        }

        //animation proc
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsMoving", isMoving);
        anim.SetFloat("Velocity", rb.velocity.y);
        anim.SetBool("Dashing", dashAnim);
    }

    void LateUpdate() {
        //dash and jump
        if (Input.GetButtonDown("Dash") && canDash) {
            StartCoroutine(Dash(DashDuration));
        }

        if (Input.GetButtonDown("Jump") && canJump) {
            rb.velocity = Vector2.up * JumpForce;
            canJump = false;
            grounded = false;
        }

        //raycasts
        //ground check
        if (grounded && rb.velocity.y != 0) grounded = false;
        if (!grounded) {
            RaycastHit2D groundray = Physics2D.Linecast(rb.position, rb.position + -Vector2.up * 0.1f, 1 << LayerMask.NameToLayer("Ground"));
            if (groundray.collider != null) {
                if (groundray.collider.gameObject.CompareTag("Ground") && rb.velocity.y <= 0) OnPlatformHit();
            }
            
        }
    }

    //physics calculation
    void FixedUpdate () {
        //Horizontal Movement
        if (Input.GetAxisRaw("Horizontal") != 0 && canDash) {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, rb.velocity.y);
            if (!isMoving) isMoving = true;

        } else {
            isMoving = false;
            if (canDash && grounded) rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    IEnumerator Dash(float dashDur) {
        float dashTime = 0;
        canDash = false;
        anim.SetTrigger("Dash");
        dashAnim = true;

        //dash logic
        while (dashTime < dashDur) {
            dashTime += Time.deltaTime;
            rb.velocity = new Vector2(DashSpd * transform.localScale.x, rb.velocity.y);
            yield return 0;
        }

        //check if player is grounded
        dashAnim = false;
        if (grounded && !canDash) canDash = true;

        //revert back to normal movement
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed, rb.velocity.y);
    }

    public void Flip() {
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void OnPlatformHit() {
        canJump = true;
        canDash = true;
        grounded = true;
    }
}
