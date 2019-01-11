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

    Rigidbody2D body;
    PlayerData data;
    Transform tr;

    bool grounded;
    bool previousTickGrounded;

    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        data = GetComponent<PlayerData>();
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();

        if (Input.GetKey(respawn)) {
            data.ExitDeathmode();
        }

        if (tr.position.y < -100) {
            Scenes.ResetLevel();
        }

# if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R)) {
            Scenes.ResetLevel();
        }
# endif
        if (PlayerData.CirnoDead && PlayerData.ClownDead) {
            Scenes.ResetLevel();
        }
    }

    private void ProcessMovement() {
        if (data.Dead)
            return; //Dead people can't move. (alternatively, a different moveset sometime?)
        // If there's no vertical movement for two ticks in a row, it's safe to assume the player is on the ground.
        bool notFalling = Mathf.Abs(body.velocity.y) <= 0.05;
        grounded = notFalling && previousTickGrounded;
        Vector2 force = Vector2.zero;
        // Process regular movement input
        if (Input.GetKey(left)) {
            force += Vector2.left * speed;
        }
        if (Input.GetKey(right)) {
            force += Vector2.right * speed;
        }
        if (Input.GetKey(up) && grounded) {
            jumping = true;
            force += Vector2.up * jump;
        }
        body.velocity += force; //totally how physics works
        if (body.velocity.x < -maxHorizontalSpeed) {
            body.velocity = new Vector2(-maxHorizontalSpeed, body.velocity.y);
        } else if (body.velocity.x > maxHorizontalSpeed) {
            body.velocity = new Vector2(maxHorizontalSpeed, body.velocity.y);
        }

        // Want to stop jumping when the player releases "jump"
        if (Input.GetKeyUp(up) && jumping) {
            jumping = false;
            body.velocity = new Vector2(body.velocity.x, body.velocity.y/3);
        }
        previousTickGrounded = notFalling;
    }

    public void ResetMovement() {
        body.velocity = Vector2.zero;
    }
}
