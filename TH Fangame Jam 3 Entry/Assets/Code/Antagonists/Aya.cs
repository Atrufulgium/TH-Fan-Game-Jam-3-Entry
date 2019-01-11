using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aya : Antagonist
{
    protected override int CooldownDuration { get; set; } = 120;
    protected override int Cooldown { get; set; } = 180;
    protected override float FlySpeed { get; set; } = 15;
    protected override float FlyHeight { get; set; } = 6;

    int phase = 0; // maybe three phases? dunno

    // todo: "feather"/arrow/whatever bullets. as long as they're not these round ones.
    protected override void Shoot() {
        if (phase == 0) {
            for (int i = 2; i < 11; i++) {
                Bullet.CreateBullet(tr.position, (i * 30f - 90) * Mathf.Deg2Rad, 5, Color.white, Color.red);
                Bullet.CreateBullet(tr.position, (i * 30f - 90) * Mathf.Deg2Rad, 8, Color.white, Color.red);
            }
            for (int i = 2; i <= 4; i++) {
                Bullet.CreateBullet(tr.position, 270 * Mathf.Deg2Rad, 2 * i, Color.white, Color.green);
                Bullet.CreateBullet(tr.position, 265 * Mathf.Deg2Rad, 1.5f * i, Color.white, Color.green);
                Bullet.CreateBullet(tr.position, 275 * Mathf.Deg2Rad, 1.5f * i, Color.white, Color.green);
                Bullet.CreateBullet(tr.position, 255 * Mathf.Deg2Rad, 1.2f * i, Color.white, Color.green);
                Bullet.CreateBullet(tr.position, 285 * Mathf.Deg2Rad, 1.2f * i, Color.white, Color.green);
            }

            phase = 1;
            CooldownDuration = 30;
        } else {
            for (int i = 4; i <= 8; i++) {
                Bullet.CreateBullet(tr.position, 270 * Mathf.Deg2Rad, 3f * i, Color.white, Color.blue);
                Bullet.CreateBullet(tr.position, 260 * Mathf.Deg2Rad, 2f * i, Color.white, Color.blue);
                Bullet.CreateBullet(tr.position, 280 * Mathf.Deg2Rad, 2f * i, Color.white, Color.blue);
                Bullet.CreateBullet(tr.position, 245 * Mathf.Deg2Rad, 1.5f * i, Color.white, Color.blue);
                Bullet.CreateBullet(tr.position, 295 * Mathf.Deg2Rad, 1.5f * i, Color.white, Color.blue);
            }
            phase = 0;
            CooldownDuration = 210;
        }
    }
}
