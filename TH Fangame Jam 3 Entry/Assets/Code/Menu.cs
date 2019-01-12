using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Menu : MonoBehaviour
{
    public enum Function { Play, Manual, Quit }

    public Function function;

    public GameObject next;
    public GameObject previous;

    public bool selected = false;
    public bool goselect = false; //set selected to true next tick

    public Sprite activeImage;
    public Sprite inactiveImage;

    private void Start() {
        SetImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                previous.GetComponent<Menu>().Select();
                Deselect();
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                next.GetComponent<Menu>().Select();
                Deselect();
            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.Z)) {
                switch (function) {
                    case Function.Play:
                        Scenes.LoadLevel("Level 1");
                        break;
                    case Function.Quit:
                        Application.Quit();
                        break;
                }
            }
        }
        if (goselect) {
            selected = true;
            goselect = false;
            SetImage();
        }
    }

    public void Select() {
        goselect = true;
    }

    public void Deselect() {
        selected = false;
        SetImage();
    }

    private void SetImage() {
        GetComponent<Image>().sprite = selected ? activeImage : inactiveImage;
    }
}
