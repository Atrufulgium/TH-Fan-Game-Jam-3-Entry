using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWorkaround : MonoBehaviour
{
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = PlayerData.CirnoTr.position + Vector3.forward * 0.001f;
    }
}
