using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    private static KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

    void Update()
    {
        for (int i = 0; i < 9; i++) {
            if (Input.GetKeyDown(keys[i])) {
                AudioManager.StartSFX(AudioManager.SFX.Click);
                Scenes.LoadLevel($"Level {i + 1}");
            }
        }
    }
}
