using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerData))]
public class Movement : MonoBehaviour
{
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode respawn = KeyCode.DownArrow;

    public float speed;
    public float jump;

    public float maxHorizontalSpeed;
    public float maxUpSpeed;

    Rigidbody2D body;
    PlayerData data;
    Transform tr;
    SpriteAnimation anim;

    public bool grounded;
    public bool wallFront;

    bool jumping = false;
    int jumpcooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        data = GetComponent<PlayerData>();
        anim = GetComponent<SpriteAnimation>();
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Scenes.LoadLevel("Main Menu");
        }

        if (jumpcooldown > 0)
            jumpcooldown--;

        if (data.Dead) {
            anim.currentAnimation = 2;
        } else {
            anim.currentAnimation = 0;
        }
        ProcessMovement();

        if (Input.GetKeyDown(respawn)) {
            data.ExitDeathmode();
        }

        if (tr.position.y < -100) {
            Scenes.ResetLevel();
        }

        if (Input.GetKey(KeyCode.R)) {
            Scenes.ResetLevel();
        }

        if (PlayerData.CirnoDead && PlayerData.ClownDead) {
            Scenes.ResetLevel();
        }
    }

    private void ProcessMovement() {
        if (data.Dead)
            return; //Dead people can't move. (alterna

        Vector2 force = Vector2.zero;
        // Process regular movement input
        if (Input.GetKey(left) && !wallFront) {
            anim.currentAnimation = 1;
            force += Vector2.left * speed;
            Vector3 scale = tr.localScale;
            scale.x = -1;
            tr.localScale = scale;
        }
        if (Input.GetKey(right) && !wallFront) {
            anim.currentAnimation = 1;
            force += Vector2.right * speed;
            Vector3 scale = tr.localScale;
            scale.x = 1;
            tr.localScale = scale;
        }
        if (Input.GetKey(up) && grounded && jumpcooldown <= 0) {
            jumping = true;
            force += Vector2.up * jump;
            jumpcooldown = 5;
        }
        body.velocity += force; //totally how physics works
        if (body.velocity.x < -maxHorizontalSpeed) {
            body.velocity = new Vector2(-maxHorizontalSpeed, body.velocity.y);
        } else if (body.velocity.x > maxHorizontalSpeed) {
            body.velocity = new Vector2(maxHorizontalSpeed, body.velocity.y);
        }
        if (body.velocity.y > maxUpSpeed) {
            body.velocity = new Vector2(body.velocity.x, maxUpSpeed);
        }

        // Want to stop jumping when the player releases "jump"
        if (Input.GetKeyUp(up) && jumping) {
            jumping = false;
            if (body.velocity.y > 0)
                body.velocity = new Vector2(body.velocity.x, body.velocity.y/3);
        }
        // Want to stop moving horizontally if no < or > pressed
        if ((Input.GetKeyUp(left) && !Input.GetKey(right)) || (Input.GetKeyUp(right) && !Input.GetKey(left))) {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        grounded = false; // set to true again in GroundChecker if there's ground below, that executes before any of this.
        wallFront = false;
    }

    public void ResetMovement() {
        body.velocity = Vector2.zero;
    }
}
