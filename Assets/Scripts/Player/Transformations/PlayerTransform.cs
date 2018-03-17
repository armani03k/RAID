using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerTransform : MonoBehaviour {
    //basic needs
    PlayerMovement pMovement;
    BetterJump bJump;

    [Header("Skill Perks")]
    public bool WallHop;
    public bool WallRun;
    private bool wRunning;
    public bool FlameOn;
    //public bool Glide;

    #region Hopper
    [Header("Hopper")]
    public float HopForce;
    private bool hopable;

    private float hopperDashCD;
    public float HopperDashCD;
    private bool hopCDOff;

    private float hopDelay;
    public float HopDelay;
    #endregion


    #region Tiger
    [Header("Tiger")]
    public float WRunDescend;
    private float descendTick = 0;
    private float descendTimer = 0.5f;
    private bool descending;
    #endregion

    [Header("Forms")]
    public Transformation Current;
    public List<Transformation> Forms;

    SpriteRenderer PlayerSprite;


    [Header("debug")]
    public TextMeshProUGUI Description;
    public bool canHop;

    void Start() {
        Current.gameObject.SetActive(true);
        bJump = GetComponent<BetterJump>();
        pMovement = GetComponent<PlayerMovement>();

        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerSprite.color = Current.BaseColor;

        //debug
        Description.text = Current.Desc;
    }

    void Update() {
        if (Input.GetButtonDown("LB")) ChangePrevious();
        if (Input.GetButtonDown("RB")) ChangeNext();

        if (Input.GetButtonDown("Ability") && pMovement.GetGround) Current.UseGroundAbility();
        if (Input.GetButtonDown("Ability") && !pMovement.GetGround) Current.UseAerialAbility();

        //hopper
        if (hopCDOff) {
            hopperDashCD += Time.deltaTime;
            if (hopperDashCD >= HopperDashCD) {
                hopperDashCD = 0;
                hopCDOff = false;
                pMovement.SetDash = true;
            }
        }
    }

    private void LateUpdate() {
        if (hopable && Input.GetButtonDown("Jump")) {
            pMovement.SetDash = false;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.transform.localScale.x, Vector2.up.y) * HopForce;
            pMovement.Flip();
            pMovement.SetFaceDir = !pMovement.SetFaceDir;
            hopable = false;

            //dash cooldown
            hopperDashCD = 0;
            hopCDOff = true;
        }
    }


    void ChangeNext() {
        Current.gameObject.SetActive(false); //disable prefab
        Transformation temp = Current;
        Current = Forms[0];
        Forms.Remove(Forms[0]);
        Forms.Add(temp);
        Current.gameObject.SetActive(true); //enable prefab

        PlayerSprite.color = Current.BaseColor;

        //turn back params (good good practice)
        pMovement.JumpForce = pMovement.DefaultJumpForce;
        bJump.FallMultiplier = bJump.FallDefault;

        //tiger
        this.GetComponent<Rigidbody2D>().drag = 0;
        wRunning = false;

        //debug
        Description.text = Current.Desc;
    }

    void ChangePrevious() {
        Current.gameObject.SetActive(false); //disable prefab
        Transformation temp = Current;
        Current = Forms[Forms.Count - 1];
        Forms.Remove(Forms[Forms.Count - 1]);
        Forms.Insert(0,temp);
        Current.gameObject.SetActive(true); //enable prefab

        PlayerSprite.color = Current.BaseColor;

        //params
        pMovement.JumpForce = pMovement.DefaultJumpForce;
        bJump.FallMultiplier = bJump.FallDefault;

        //tiger
        this.GetComponent<Rigidbody2D>().drag = 0;
        wRunning = false;

        //debug
        Description.text = Current.Desc;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Wall") && WallRun) {
            pMovement.JumpForce = 6.0f;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        #region Hopper
        if (other.gameObject.CompareTag("WallPlatform") && WallHop && !pMovement.GetGround && !hopable) {
            if (hopDelay < HopDelay) hopDelay += Time.deltaTime;
            else hopable = true;
        } else if (other.gameObject.CompareTag("WallPlatform") && WallHop && pMovement.GetGround && hopable) {
            hopDelay = 0;
            hopable = false;
        }
        #endregion

        #region Tiger
        if (other.gameObject.CompareTag("Wall") && WallRun) pMovement.JumpForce = 6.0f;

        if (other.gameObject.CompareTag("Wall") && Input.GetButton("Jump") && WallRun) {
            //descent prompt
            if (descendTick < descendTimer) descendTick += Time.deltaTime;
            else if (!descending && descendTick >= descendTimer) {
                this.GetComponent<Rigidbody2D>().drag = 10;
                descending = true;
            }
            //trigger wall run
            if (!wRunning) {
                bJump.FallMultiplier = WRunDescend;
                wRunning = true;
                return;
            }
        }
        if (other.gameObject.CompareTag("Wall") && !Input.GetButton("Jump") && WallRun) {
            if (wRunning) {
                wRunning = false;
                bJump.FallMultiplier = bJump.FallDefault;

                //return descent
                descending = false;
                descendTick = 0;
                this.GetComponent<Rigidbody2D>().drag = 0;
                return;
            }
        }
        #endregion
    }

    private void OnTriggerExit2D(Collider2D other) {
        #region hopper
        if (other.gameObject.CompareTag("WallPlatform") && WallHop && hopable) {
            hopDelay = 0;
            hopable = false;
        }
        #endregion

        #region tiger
        if (other.gameObject.CompareTag("Wall") && WallRun) {
            wRunning = false;
            pMovement.JumpForce = pMovement.DefaultJumpForce;
            bJump.FallMultiplier = bJump.FallDefault;

            //return descent
            descending = false;
            descendTick = 0;
            this.GetComponent<Rigidbody2D>().drag = 0;
        }
        #endregion

    }
}
