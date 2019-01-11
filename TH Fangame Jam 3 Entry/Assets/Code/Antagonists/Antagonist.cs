using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Antagonist : MonoBehaviour
{
    protected abstract int CooldownDuration { get; set; }
    protected abstract int Cooldown { get; set; }

    protected Transform tr;

    private void Start() {
        tr = transform;
    }

    void Update() {
        if (Cooldown == 0) {
            Shoot();
            Cooldown = CooldownDuration;
        }
        if (Cooldown > 0)
            Cooldown--;
    }

    protected abstract void Shoot();
}
