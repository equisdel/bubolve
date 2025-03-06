using System;
using UnityEngine;

public class ShieldAbility : Ability
{
    public float shield = 5;
    public float thiccnessMultiplier = 1;
    public GameObject shieldGameObject;

    public override void DoAction()
    {
        base.DoAction();
        shieldGameObject.SetActive(true);
        parentEntity.ModifyQuality(Entity.Qualities.SHIELD, shield);
    }

    public override void EndAction()
    {
        base.EndAction();
        shieldGameObject.SetActive(false);
        parentEntity.ModifyQuality(Entity.Qualities.SHIELD, -shield);
    }

}

//Shield
//BlastWave
//BubbleCannon
//Chase