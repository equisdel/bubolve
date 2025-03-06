using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : Ability
{
    public int shoots = 60;
    public float damage = 8;
    public float damageMultiplier = 1;
    public GameObject projectilePrefab;

    public override void DoAction(Vector3 dir)
    {
        base.DoAction();
        for (int i = 0; i < shoots; i++)
        {
            GameObject projectileGamaObject = Instantiate(projectilePrefab, entityGameObject.transform.position, Quaternion.identity);
            Projectile projectile = projectileGamaObject.GetComponent<Projectile>();
            projectile.damage = damage + parentEntity.attack_damage * damageMultiplier;
            projectile.source = entityGameObject;
            projectile.attacker = parentEntity;

            float angle = (360f / (float)shoots) * (float)i;
            Vector3 dir2 = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            projectile.direction = dir2;
        }

    }
}
