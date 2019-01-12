using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Menu : MonoBehaviour
{
    public enum Function { Play, Manual, BGMVol, Quit }

    public Function function;

    public GameObject next;
    public GameObject previous;

    public GameObject affectedObject;

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
            // don't you just love those switch statements that just scream as obvious as possible "use oop"?
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
                    case Function.Manual:
                        affectedObject.SetActive(!affectedObject.activeSelf);
                        break;
                    case Function.Quit:
                        Application.Quit();
                        break;
                }
            } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                switch (function) {
                    case Function.BGMVol:
                        AudioManager.MusicVolume += 0.02f;
                        break;
                }
            } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                switch (function) {
                    case Function.BGMVol:
                        AudioManager.MusicVolume -= 0.02f;
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
