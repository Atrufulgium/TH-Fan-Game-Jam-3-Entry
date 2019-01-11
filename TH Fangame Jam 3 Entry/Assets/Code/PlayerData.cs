using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerData : MonoBehaviour
{
    int harmLayerID;
    int cirnoLayerID;
    int clownLayerID;
    int finishLayerID;
    // Invulnerability, -1 = no invulnerability, any more is the amount of gameticks left
    int iframes = -1;
    // Whether in death mode, -1 = not in death mode, any more is the amount of ticks left
    int deathmode = -1;

    public bool Dead { get => deathmode != -1; }

    private static int deathmodeDuration = 300;
    private static int iframeduration = 30;

    GameObject aura;
    SpriteRenderer auraRenderer;
    Rigidbody2D body;

    GameObject otherPlayer;

    Movement movement;
    bool isCirno = false;

    public static bool CirnoDead = false;
    public static bool ClownDead = false;

    public static Transform CirnoTr;
    public static Transform ClownTr;

    // Start is called before the first frame update
    void Start()
    {
        CirnoDead = false;
        ClownDead = false;
        harmLayerID = LayerMask.NameToLayer("Harmful");
        cirnoLayerID = LayerMask.NameToLayer("Cirno");
        clownLayerID = LayerMask.NameToLayer("Clownpiece");
        finishLayerID = LayerMask.NameToLayer("Finish");
        if (gameObject.layer == cirnoLayerID) {
            isCirno = true;
            aura = GameObject.FindWithTag("shittyworkaround");
            CirnoTr = transform;
            otherPlayer = GameObject.FindWithTag("Clownpiece");
        } else {
            aura = GameObject.FindWithTag("shittyworkaround2");
            otherPlayer = GameObject.FindWithTag("Cirno");
            ClownTr = transform;
        }
        auraRenderer = aura.GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathmode == 0) {
            ExitDeathmode();
        } else if (deathmode > 0) {
            body.simulated = false;
            Color col = auraRenderer.color;
            col.a = 0.5f - (deathmode / (2f * deathmodeDuration));
            auraRenderer.color = col;
            deathmode--;
        }

        if (iframes >= 0)
            iframes--;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Sign") {
            other.GetComponent<SignPop>().PopSign();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Sign") {
            other.GetComponent<SignPop>().PutSign();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == finishLayerID) {
            Scenes.LoadNextLevel();
        }
        if (other.gameObject.layer == harmLayerID && iframes < 0 && deathmode < 0) {
            Bullet bul = other.gameObject.GetComponent<Bullet>();
            if (bul != null) // There are now bullets and spikes that are both harmful
                other.gameObject.GetComponent<Bullet>().StartShrink();
            EnterDeathmode();
        }
    }

    public void EnterDeathmode() {
        if (deathmode != -1)
            return; //already in deathmode, don't want to reset the stuff

        if (isCirno) {
            CirnoDead = true;
            GameObject createdObject = Instantiate((GameObject) Resources.Load("Prefabs/CirnoIce"));
            Vector2 pos = createdObject.transform.position;
            pos = transform.position;
            createdObject.transform.position = new Vector3(pos.x, pos.y, createdObject.transform.position.z);
        } else {
            ClownDead = true;
        }
        deathmode = deathmodeDuration;
        //screw caching it it doesn't get called often at all
        aura.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void ExitDeathmode() {
        if (deathmode < 0)
            return; //not in deathmode shouldn't do anything
        if (isCirno)
            CirnoDead = false;
        else
            ClownDead = false;

        deathmode = -1;
        iframes = iframeduration;
        Color col = auraRenderer.color;
        col.a = 0.5f;
        auraRenderer.color = col;
        aura.GetComponent<CircleCollider2D>().enabled = false;
        transform.position = otherPlayer.transform.position + Vector3.up;
        movement.ResetMovement();
        body.simulated = true;
    }
}
