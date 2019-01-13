using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to main camera
public class Parallax : MonoBehaviour
{
    public Transform target;
    public float amount;

    Transform tr;
    Vector3 initialPos;

    private void Start() {
        tr = transform;
        initialPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.x = tr.position.x * amount + initialPos.x;
        pos.y = initialPos.y;
        target.position = pos;
    }
}
