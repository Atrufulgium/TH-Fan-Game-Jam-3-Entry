using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cirnoTr, clownTr;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move camera to the center of the two players
        Vector2 result = (cirnoTr.position + clownTr.position) * 0.5f;
        Vector3 pos = tr.position;
        pos.x = result.x;
        pos.y = result.y;
        tr.position = pos;
        // Resize the camera so things fit nicely;
        // height = 2*size, width = height * aspect ratio => width = 2 * size * aspect ratio
        float dx = Mathf.Abs(cirnoTr.position.x - clownTr.position.x);
        float dy = Mathf.Abs(cirnoTr.position.y - clownTr.position.y);
        float aspectRatio = Camera.main.aspect;
        float sizewidth = (dx + 10) / (2 * aspectRatio); // really I can assume the screen is not degenerate
        float sizeheight = (dy + 10) / 2;
        float size = Mathf.Max(Mathf.Max(sizewidth, sizeheight), 10); // don't want to params array
        Camera.main.orthographicSize = size;
    }
}
