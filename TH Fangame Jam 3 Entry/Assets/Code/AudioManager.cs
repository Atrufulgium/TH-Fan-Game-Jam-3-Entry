using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Music { None, Title, Shrine, Forest, Mountain }

    static Music playing = Music.None;

    public AudioSource TitleMusic;
    public AudioSource ShrineMusic;
    public AudioSource ForestMusic;
    public AudioSource MountainMusic;
    private static AudioManager manager = null;

    void Start()
    {
        Scenes.CheckLevel();
        // Want precisely one audio manager.
        if (manager == null) {
            manager = this;
            StartBGM();
            DontDestroyOnLoad(gameObject);
        } else {
            StartBGM();
            Destroy(gameObject);
        }
    }

    public static void StartBGM() {
        int i = Scenes.currentLevel;
        if (i == 0)
            StartMusic(Music.Title);
        else if (i <= 3)
            StartMusic(Music.Shrine);
        else if (i <= 6)
            StartMusic(Music.Forest);
        else if (i <= 9)
            StartMusic(Music.Mountain);
        else
            StartMusic(Music.None);
    }

    private static void StartMusic(Music music) {
        if (music == playing)
            return;
        switch (playing) {
            case Music.Title: manager.TitleMusic.Stop(); break;
            case Music.Shrine: manager.ShrineMusic.Stop(); break;
            case Music.Forest: manager.ForestMusic.Stop(); break;
            case Music.Mountain: manager.MountainMusic.Stop(); break;
        }
        switch (music) {
            case Music.Title: manager.TitleMusic.Play(); break;
            case Music.Shrine: manager.ShrineMusic.Play(); break;
            case Music.Forest: manager.ForestMusic.Play(); break;
            case Music.Mountain: manager.MountainMusic.Play(); break;
        }
        playing = music;
    }
}
