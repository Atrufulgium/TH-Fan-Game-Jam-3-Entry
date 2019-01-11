using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnWorkaround : MonoBehaviour
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
        tr.position = PlayerData.ClownTr.position + Vector3.forward * 1.001f;
    }
}
