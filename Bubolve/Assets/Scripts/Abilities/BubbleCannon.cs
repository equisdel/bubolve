using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCannon : Ability
{
    public GameObject projectilePrefab;

    public override void DoAction(Vector3 dir)
    {
        base.DoAction();
        GameObject projectileGamaObject = Instantiate(projectilePrefab, entityGameObject.transform.position, Quaternion.identity);
        Projectile projectile = projectileGamaObject.GetComponent<Projectile>();
        projectile.direction = dir;

    }
}
