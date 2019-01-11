using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marisa : Antagonist
{
    protected override int CooldownDuration { get; set; } = 120;
    protected override int Cooldown { get; set; } = 60;
    protected override float FlySpeed { get; set; } = 8;
    protected override float FlyHeight { get; set; } = 10;

    int bulletSpamCounter = 0;
    int maxBulletSpam = 80;

    bool target = false; // false = cirno, true = clownpiece

    Vector2 schietschijf;

    protected override void Shoot() {
        if (bulletSpamCounter == maxBulletSpam) { // Start: aim + warning
            target = Random.Range(0f,1f) < 0.5f;
            if (PlayerData.ClownDead)
                target = false;
            if (PlayerData.CirnoDead)
                target = true;

            if (target)
                schietschijf = PlayerData.ClownTr.position;
            else
                schietschijf = PlayerData.CirnoTr.position;

            //todo stars
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf), 6, Color.white, Color.blue);

            bulletSpamCounter--;
            CooldownDuration = 120;
        } else if (bulletSpamCounter == maxBulletSpam - 1) { // begin
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf), 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) + 30, 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) - 30, 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) + 45, 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) - 45, 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) + 50, 4, Color.white, Color.blue);
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) - 50, 4, Color.white, Color.blue);
            CooldownDuration = 2;
            bulletSpamCounter--;
        } else if (bulletSpamCounter == 0) { // End: cooldown
            CooldownDuration = 180;
            bulletSpamCounter = maxBulletSpam;
        } else { //Middle: spam
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(schietschijf) + Random.Range(-0.175f, 0.175f), 8, Color.white, Color.yellow);
            bulletSpamCounter--;
        }
    }
}
