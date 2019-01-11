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
    public GameObject[] neighbours;

    SpriteRenderer spriterenderer;

    // Start is called before the first frame update
    void Start() {
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
        spriterenderer = GetComponent<SpriteRenderer>();
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
        if (freezeTimer != -1)
            return; //already burning
        freezeTimer = freezeDuration;
        spriterenderer.sprite = halfFrozenTexture;
    }

    // Final phase of freezing: turn into a block of ice subject to physics
    private void SpreadFreeze() {
        foreach (GameObject neighbour in neighbours) {
            if (neighbour == null)
                continue;
            neighbour.GetComponent<Burnable>().Burn();
        }
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Collider2D>().isTrigger = false;
        spriterenderer.sprite = freezeTexture;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == skillCirnoID) {
            Freeze();
        }
    }
}
