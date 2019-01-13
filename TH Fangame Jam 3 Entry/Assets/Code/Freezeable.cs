using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteAnimation))]
public class Freezeable : MonoBehaviour
{
    // Cirno for freezing it, Clownpiece for thawing it?
    int skillClownID, skillCirnoID;

    // How far the freezing's coming along. It should only propagate when cirno's dead.
    int freezeTimer = -1;
    static int freezeDuration = 30;

    public Sprite halfFrozenTexture;
    public Sprite freezeTexture;

    List<GameObject> neighbours = new List<GameObject>(8);
    Transform tr;

    SpriteAnimation anim;

    // Start is called before the first frame update
    void Start() {
        tr = transform;
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
        anim = GetComponent<SpriteAnimation>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Freezable")) {
            if ((tr.position - g.transform.position).sqrMagnitude <= 1.1 && g != gameObject)
                neighbours.Add(g);
        }
        anim.currentFrame = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update() {
        if (freezeTimer == 0) {
            SpreadFreeze();
            freezeTimer = -2;
        }
        if (freezeTimer > 0)
            freezeTimer--;
    }

    // Start freezing
    public void Freeze() {
        if (freezeTimer != -1 || !PlayerData.CirnoDead)
            return; //already freezing or can't freeze
        freezeTimer = freezeDuration;
        anim.currentAnimation = 1;
        anim.cooldown = 1;
        anim.frameDuration = 8;
        anim.ApplyAnimation();
    }

    // Final phase of freezing: turn into a block of ice subject to physics
    private void SpreadFreeze() {
        foreach (GameObject neighbour in neighbours) {
            if (neighbour == null)
                continue;
            neighbour.GetComponent<Freezeable>().Freeze();
        }
        AudioManager.StartSFX(AudioManager.SFX.Freeze);
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().isTrigger = false;
        anim.frameDuration = 30;
        anim.currentFrame = 0;
        anim.currentAnimation = 2;
        anim.ApplyAnimation();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == skillCirnoID && PlayerData.CirnoDead) {
            Freeze();
        }
    }
}
