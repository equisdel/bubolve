using System;
using UnityEngine;

public class ShieldAbility : Ability
{
    public float shield = 5;
    public float thiccnessMultiplier = 1;

    public override void DoAction()
    {
        base.DoAction();
        parentEntity.ModifyQuality(Entity.Qualities.SHIELD, shield);
    }

    public override void EndAction()
    {
        base.EndAction();
        parentEntity.ModifyQuality(Entity.Qualities.SHIELD, -shield);
    }

}

//Shield
//BlastWave
//BubbleCannon
//Chase