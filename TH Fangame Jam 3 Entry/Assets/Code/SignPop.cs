using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPop : MonoBehaviour
{
    public static int pops = 0;

    bool popped = false;
    int poptime = 0;
    int maxpoptime = 30;
    Transform child;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        pops = 0;
        child = transform.GetChild(0);
        startPos = child.position;
    }

    private void Update() {
        poptime = popped ? poptime+1 : poptime-1;
        if (poptime < 0) poptime = 0;
        if (poptime > maxpoptime) poptime = maxpoptime;
        child.position = startPos + Vector3.up * (4.5f * poptime / maxpoptime);
        child.localScale = Vector3.one * (poptime / (float) maxpoptime);
    }

    // Pop the actual text out of it
    public void PopSign() {
        if (!popped) {
            popped = true;
            pops++;
        }
    }

    // Put it back
    public void PutSign() {
        if (popped) {
            popped = false;
            pops--;
        }
    }
}
