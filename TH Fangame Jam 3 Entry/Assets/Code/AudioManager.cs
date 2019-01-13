using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Music { None, Title, Shrine, Forest, Mountain }

    public enum SFX { Death, Ressurect, Freeze, Vaporise, Select, Click, Shoot1, Shoot2, MarisaCharge, Laser };

    static Music playing = Music.None;

    public AudioSource TitleMusic;
    public AudioSource ShrineMusic;
    public AudioSource ForestMusic;
    public AudioSource MountainMusic;
    public AudioSource[] soundEffects; // should align with the SFX enum
    private static AudioManager manager = null;

    private static List<AudioSource> sfx = new List<AudioSource>(20);

    public static float MusicVolume { get => musicVolume; set => SetBGMVolume(value); }
    static float musicVolume = 1f;

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
        else if (i <= 10)
            StartMusic(Music.Mountain); // The final screen should also have the mountain theme
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

    // first a basic version that doesn't allow multiple of the same
    public static void StartSFX(SFX sfx) {
        manager.soundEffects[(int) sfx].Play();
    }

    public static void SetBGMVolume(float percentage) {
        percentage = Mathf.Clamp01(percentage);
        manager.TitleMusic.volume = percentage;
        manager.ShrineMusic.volume = percentage;
        manager.ForestMusic.volume = percentage;
        manager.MountainMusic.volume = percentage;
        musicVolume = percentage;
    }
}
