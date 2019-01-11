using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    // Clownpiece for burning, maybe cirno for putting out the flames again?
    // then again, both being death at once should not be a mechanic
    int skillClownID, skillCirnoID;

    // How far the burning's coming along. It should only propagate when clownpiece's dead.
    int burntimer = -1;
    static int burnDuration = 30;

    public Sprite burnTexture;
    public GameObject[] neighbours;

    SpriteRenderer spriterenderer;

    // Start is called before the first frame update
    void Start()
    {
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
        spriterenderer = GetComponent<SpriteRenderer>();
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
        if (burntimer != -1)
            return; //already burning
        burntimer = burnDuration;
        spriterenderer.sprite = burnTexture;
    }

    // Final phase of burning
    private void SpreadBurn() {
        foreach (GameObject neighbour in neighbours) {
            if (neighbour == null)
                continue;
            neighbour.GetComponent<Burnable>().Burn();
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == skillClownID) {
            Burn();
        }
    }
}
