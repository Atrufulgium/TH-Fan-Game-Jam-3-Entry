using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Movement movement;

    private void Update() {
        transform.localPosition = new Vector3(0, -0.96f, 0);
    }

    private void OnTriggerStay2D(Collider2D other) {
        movement.grounded = true;
    }
}
