using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public const float SPEED = 0.3f, AIRSPEED = 0.07f, ROLLSPEED = 2f, MAX_HP = 100;
    private const float JUMP_GRACE = 0.3f;
    private const float JUMP_RELEASE_REDUCE = 0.75f;
    public static float JUMP_MAX = 0f;
    public Transform camt;
    public Platonic[] platonics;
    public KeyCode[] platonicCodes;

    public Platonic platonic;
    public bool landed = false;
    public Rigidbody rigid;
    public Collider col;

    public enum State{
        NONE,
        IDLE,
        JUMP
    }

    public State state = State.NONE;
    public State nextState = State.NONE;
    public float stateTime = 0f; //time after a state change
    public float health = 0f;
    private bool jumpReleased = false;
    private float jumpPressTimer = JUMP_GRACE + 0.1f;

    void Start() {
        reset();
        SetPlatonic(platonics[0]);
    }

    void Update() {
        Vector3 vel = rigid.velocity; 
        bool inputJump = Input.GetKey(KeyCode.Space);
        stateTime += Time.deltaTime;
        CheckLanded();

        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpPressTimer = 0f;
        }
        else if (jumpPressTimer < JUMP_GRACE) {
            jumpPressTimer += Time.deltaTime;
        }

        //AUTO State CHANGE
        if (nextState == State.NONE) {
            switch (state) {
                case State.IDLE:
                    if(landed) {
                        if (jumpPressTimer < JUMP_GRACE) {
                            //jump
                            jumpPressTimer = JUMP_GRACE + 0.1f;
                            nextState = State.JUMP;
                        }
                    }
                    break;
                case State.JUMP:
                    if (landed) nextState = State.IDLE;
                    break;
            }
        }

        //State INIT (per state)
        if (nextState != State.NONE) {
            state = nextState;
            nextState = State.NONE;
            switch (state) {
                case State.JUMP:
                    vel.y = JUMP_MAX;
                    jumpReleased = false;
                    break;
            }
            stateTime = 0f;
        }

        //UPDATE (per state)
        switch (state) {
            case State.IDLE:
                break;
            case State.JUMP:
                if (inputJump || jumpReleased) break;
                if (vel.y <= 0f) {
                    jumpReleased = true;
                    break; //nothing to do here
                }
                vel.y *= JUMP_RELEASE_REDUCE; //early jump key up; reduce jump height
                jumpReleased = true;
                break;
        };
        UpdateInput(ref vel);

        rigid.velocity = vel;
    }

    private void UpdateInput(ref Vector3 vel) {
        if (Input.GetKey(KeyCode.LeftShift)) {
            platonic.Shift();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            platonic.ShiftDown();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            platonic.ShiftUp();
        }

        if(!landed || platonic.moveType == Platonic.MoveType.SLIDE || platonic.moveType == Platonic.MoveType.HOVER) {
            bool hover = platonic.moveType == Platonic.MoveType.HOVER;
            if (Input.GetKey(KeyCode.UpArrow)) {
                vel += ForwardVector() * (landed ? SPEED : AIRSPEED);
                if(hover) rigid.AddTorque(new Vector3(0, 2.5f, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                vel += ForwardVector() * (landed ? SPEED : AIRSPEED) * -1;
                if (hover) rigid.AddTorque(new Vector3(0, -2.5f, 0));
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                vel += RightVector() * (landed ? SPEED : AIRSPEED);
                if (hover) rigid.AddTorque(new Vector3(0, 2.5f, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                vel += RightVector() * (landed ? SPEED : AIRSPEED) * -1;
                if (hover) rigid.AddTorque(new Vector3(0, -2.5f, 0));
            }
        }
        else {
            if (Input.GetKey(KeyCode.UpArrow)) {
                vel += ForwardVector() * (landed ? SPEED : AIRSPEED);
            }
        }

        for (int i = 0; i < platonics.Length; i++) {
            if(Input.GetKeyDown(platonicCodes[i])) SetPlatonic(platonics[i]);
        }
    }

    private Vector3 ForwardVector() {
        Vector3 v = camt.forward;
        v.y = 0;
        v.Normalize();
        return v;
    }

    private Vector3 RightVector() {
        Vector3 v = camt.right;
        v.y = 0;
        v.Normalize();
        return v;
    }

    public void SetPlatonic(Platonic platonic) {
        this.platonic = platonic;
        Destroy(transform.GetChild(0).gameObject);
        GameObject p = Instantiate(platonic.prefab, Vector3.zero, Quaternion.identity);
        p.transform.SetParent(transform, false);
        col = p.GetComponent<Collider>();
    }

    private void CheckLanded() {
        landed = false;
        if (!Physics.CheckSphere(new Vector3(col.bounds.center.x, col.bounds.center.y - 0.1f, col.bounds.center.z), 0.5f, 0b1011)) return;

        //there is a floor under me
        if (state == State.JUMP && stateTime < Time.deltaTime * 3f) {
            return; //don't check for ground right after a jump so the player *can* jump
        }
        landed = true;
    }

    public void reset() {
        state = State.NONE;
        nextState = State.IDLE;
        jumpPressTimer = JUMP_GRACE + 0.1f;

        health = MAX_HP;
        SetJumpHeight(3f);
    }

    public void SetJumpHeight(float h) {
        JUMP_MAX = heightToVel(h);
    }

    public float heightToVel(float h) {
        Debug.Log(Physics2D.gravity.ToString());
        return Mathf.Sqrt(Mathf.Abs(2f * Physics2D.gravity.y * h)) * Mathf.Sign(-Physics2D.gravity.y * h);//v = sqrt(2gh)
    }
}
