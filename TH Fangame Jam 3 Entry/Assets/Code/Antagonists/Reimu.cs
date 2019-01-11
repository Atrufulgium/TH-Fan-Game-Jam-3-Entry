﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reimu : Antagonist
{
    protected override int CooldownDuration { get; set; } = 90;
    protected override int Cooldown { get; set; } = 120;

    protected override void Shoot() {
        if (!PlayerData.CirnoDead)
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(PlayerData.CirnoTr.position), 3, Color.white, Color.red);
        if (!PlayerData.ClownDead)
            Bullet.CreateBullet(tr.position, tr.position.AngleTo(PlayerData.ClownTr.position), 3, Color.white, Color.red);
    }
}