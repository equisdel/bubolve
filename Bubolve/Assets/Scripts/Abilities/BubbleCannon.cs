using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCannon : Ability
{
    public float damage = 5;
    public float damageMultiplier = 1;
    public GameObject projectilePrefab;

    public override void DoAction(Vector3 dir)
    {
        base.DoAction();
        GameObject projectileGamaObject = Instantiate(projectilePrefab, entityGameObject.transform.position, Quaternion.identity);
        Projectile projectile = projectileGamaObject.GetComponent<Projectile>();
        projectile.damage = damage + parentEntity.attack_damage * damageMultiplier;
        projectile.source = entityGameObject;
        projectile.attacker = parentEntity;
        projectile.direction = dir;

    }
}
