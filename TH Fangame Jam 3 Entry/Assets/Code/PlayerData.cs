using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerData : MonoBehaviour
{
    int harmLayerID;
    int cirnoLayerID;
    int clownLayerID;
    // Invulnerability, -1 = no invulnerability, any more is the amount of gameticks left
    int iframes = -1;
    // Whether in death mode, -1 = not in death mode, any more is the amount of ticks left
    int deathmode = -1;

    private static int deathmodeDuration = 300;
    private static int iframeduration = 120;

    public GameObject aura;
    SpriteRenderer auraRenderer;

    public GameObject otherPlayer;

    Movement movement;

    public static bool CirnoDead = false;
    public static bool ClownDead = false;

    // Start is called before the first frame update
    void Start()
    {
        harmLayerID = LayerMask.NameToLayer("Harmful");
        cirnoLayerID = LayerMask.NameToLayer("Cirno");
        clownLayerID = LayerMask.NameToLayer("Clownpiece");
        auraRenderer = aura.GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathmode == 0) {
            ExitDeathmode();
        } else if (deathmode > 0) {
            Color col = auraRenderer.color;
            col.a = 0.5f - (deathmode / (2f * deathmodeDuration));
            auraRenderer.color = col;
            deathmode--;
        }

        if (iframes >= 0)
            iframes--;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == harmLayerID && iframes < 0 && deathmode < 0) {
            EnterDeathmode();
        }
    }

    public void EnterDeathmode() {
        Debug.Log($"{gameObject.name} entered deathmode");
        if (gameObject.layer == cirnoLayerID)
            CirnoDead = true;
        else
            ClownDead = true;

        deathmode = deathmodeDuration;
        //screw caching it it doesn't get called often at all
        aura.GetComponent<CircleCollider2D>().enabled = true;
    }

    public void ExitDeathmode() {
        if (gameObject.layer == cirnoLayerID)
            CirnoDead = false;
        else
            ClownDead = false;

        deathmode = -1;
        iframes = iframeduration;
        Debug.Log($"{gameObject.name} exited deathmode");
        Color col = auraRenderer.color;
        col.a = 0.5f;
        auraRenderer.color = col;
        aura.GetComponent<CircleCollider2D>().enabled = false;
        transform.position = otherPlayer.transform.position + Vector3.up;
        movement.ResetMovement();
    }
}
