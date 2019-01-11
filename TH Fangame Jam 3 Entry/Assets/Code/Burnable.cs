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
    static int burnDuration;

    public GameObject[] neighbours;

    // Start is called before the first frame update
    void Start()
    {
        skillClownID = LayerMask.NameToLayer("SkillClownpiece");
        skillCirnoID = LayerMask.NameToLayer("SkillCirno");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burn() {

    }

    private void SpreadBurn() {
        foreach (GameObject neighbour in neighbours) {
            neighbour.GetComponent<Burnable>().Burn();
        }
    }
}
