using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public Movement movement;
    // Note that the player gets scaled by -1 when moving to the other side
    private void Update() {
        transform.localPosition = new Vector3(0.33f, 0, 0);
    }

    private void OnTriggerStay2D(Collider2D other) {
        movement.wallFront = true;
    }
}
