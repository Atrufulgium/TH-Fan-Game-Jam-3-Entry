using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimation))]
public class Burnable : MonoBehaviour
{
    // Clownpiece for burning, maybe cirno for putting out the flames again?
    // then again, both being death at once should not be a mechanic
    int skillClownID, skillCirnoID;

    // How far the burning's coming along. It should only propagate when clownpiece's dead.
    int burntimer = -1;
    static int burnDuration = 30;

    public Sprite burnTexture;

    List<GameObject> neighbours = new List<GameObject>(8);
    Transform tr;

    SpriteAnimation anim;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        anim = GetComponent<SpriteAnimation>();
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
        // I really hope FindGameObjectsWithTag caches its stuff, it'd be a mess otherwise
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Burnable")) {
            if ((tr.position - g.transform.position).sqrMagnitude <= 1.1 && g != gameObject)
                neighbours.Add(g);
        }
        anim.currentFrame = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (burntimer == 0)
            SpreadBurn();
        if (burntimer > 0)
            burntimer--;
    }

    // Start burning
    public void Burn() {
        if (burntimer != -1 || !PlayerData.ClownDead)
            return; //already burning or not dead
        burntimer = burnDuration;
        anim.currentAnimation = 1;
    }

    // Final phase of burning
    private void SpreadBurn() {
        foreach (GameObject neighbour in neighbours) {
            if (neighbour == null)
                continue;
            neighbour.GetComponent<Burnable>().Burn();
        }
        AudioManager.StartSFX(AudioManager.SFX.Vaporise);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        //Debug.Log($"other: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (other.gameObject.layer == skillClownID && PlayerData.ClownDead) {
            Burn();
        }
    }
}
