using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Antagonist : MonoBehaviour
{
    protected abstract int CooldownDuration { get; set; }
    protected abstract int Cooldown { get; set; }
    protected abstract float FlySpeed { get; set; }
    protected virtual float FlyHeight { get; set; } = 7;

    protected Transform tr;

    private void Start() {
        tr = transform;
    }

    void Update() {
        if (SignPop.pops == 0) {
            if (Cooldown == 0) {
                Shoot();
                Cooldown = CooldownDuration;
            }
            if (Cooldown > 0)
                Cooldown--;
        }

        // Move to "above" the player
        Vector3 goal = FlyHeight * Vector3.up;
        if (PlayerData.CirnoDead) {
            goal += PlayerData.ClownTr.position;
        } else if (PlayerData.ClownDead) {
            goal += PlayerData.CirnoTr.position;
        } else {
            goal += (PlayerData.CirnoTr.position + PlayerData.ClownTr.position) * 0.5f;
        }
        Vector3 pos = tr.position;
        // Only move if it's a considerable distance
        if ((goal - pos).sqrMagnitude > 1) {
            Vector2 result = (goal - pos).normalized * FlySpeed * Time.deltaTime;
            tr.position += new Vector3(result.x, result.y, 0);
        }
        // Change the sprite to reflect the move direction
        Vector3 scale = transform.localScale;
        if (goal.x < pos.x) {
            scale.x = -1;
        } else {
            scale.x = 1;
        }
        transform.localScale = scale;
    }

    protected abstract void Shoot();
}
