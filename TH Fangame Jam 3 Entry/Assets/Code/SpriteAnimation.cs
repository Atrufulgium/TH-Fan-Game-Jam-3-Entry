using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public int frameCount; // size of a row
    public int animationCount; // number of rows
    public int frameDuration;

    public int currentAnimation = 0;
    public int currentFrame = 0;

    public static Dictionary<string, Sprite[]> animations = new Dictionary<string, Sprite[]>(30);

    SpriteRenderer spriteRenderer;
    Sprite[] anim;
    public int cooldown = 0;

    private void Start() {
        cooldown = frameDuration;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Makes use of the fact that assigning atlassed sprites gets a single one with suffix _[index].
        string texname = spriteRenderer.sprite.name;
        texname = texname.Remove(texname.LastIndexOf('_'));

        if (animations.ContainsKey(texname)) {
            anim = animations[texname];
        } else {
            anim = Resources.LoadAll<Sprite>($"Animations/{texname}");
            animations.Add(texname, anim);
        }
    }

    private void Update() {
        cooldown--;
        if (cooldown <= 0) {
            if (currentFrame >= frameCount - 1) {
                currentFrame = 0;
            } else {
                currentFrame++;
            }
            ApplyAnimation();

            cooldown = frameDuration;
        }
    }

    public void ApplyAnimation() {
        spriteRenderer.sprite = anim[currentFrame + frameCount * currentAnimation];
    }
}
