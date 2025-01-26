using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAbility : Ability
{
    public CharacterController characterController;
    public override void DoAction(Vector3 dir)
    {
        base.DoAction();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!ended)
        {
            Vector3 dir = (targetEntity.transform.position - gameObject.transform.position).normalized;
            characterController.Move(dir.normalized * parentEntity.movement_speed * Time.deltaTime);
        }
    }
}
