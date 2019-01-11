using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
    SpriteRenderer spriterenderer;

    // Start is called before the first frame update
    void Start() {
        tr = transform;
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
        spriterenderer = GetComponent<SpriteRenderer>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Freezable")) {
            if ((tr.position - g.transform.position).sqrMagnitude <= 1.1 && g != gameObject)
                neighbours.Add(g);
        }
    }

    // Update is called once per frame
    void Update() {
        if (freezeTimer == 0)
            SpreadFreeze();
        if (freezeTimer > 0)
            freezeTimer--;
    }

    // Start freezing
    public void Freeze() {
        if (freezeTimer != -1 || !PlayerData.CirnoDead)
            return; //already freezing or can't freeze
        freezeTimer = freezeDuration;
        spriterenderer.sprite = halfFrozenTexture;
    }

    // Final phase of freezing: turn into a block of ice subject to physics
    private void SpreadFreeze() {
        foreach (GameObject neighbour in neighbours) {
            if (neighbour == null)
                continue;
            neighbour.GetComponent<Freezeable>().Freeze();
        }
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().isTrigger = false;
        spriterenderer.sprite = freezeTexture;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == skillCirnoID && PlayerData.CirnoDead) {
            Freeze();
        }
    }
}
