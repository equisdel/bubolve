using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    // do_action
    public float cooldown;
    public float activeTime;
    public bool instant;
    public bool ended = true;
    public bool ready = true;

    internal float currentTime = 0;
    internal ParentEntity parentEntity;
    internal GameObject entityGameObject;
    internal GameObject targetEntity;

    public virtual void SetEntity(ParentEntity parentEntity, GameObject entityGameObject)
    {
        this.parentEntity = parentEntity;
        this.entityGameObject = entityGameObject;
    }

    public virtual void SetEntity(ParentEntity parentEntity, GameObject entityGameObject, GameObject targetEntity)
    {
        this.parentEntity = parentEntity;
        this.entityGameObject = entityGameObject;
        this.targetEntity = targetEntity;
    }

    public virtual void DoAction()
    {
        ended = false;
        ready = false;
        currentTime = 0;
    }

    public virtual void DoAction(Vector3 dir)
    {
        DoAction();
    }

    public virtual void EndAction()
    {
        ended = true;
    }

    public virtual void FixedUpdate()
    {
        if (!ready)
        {
            currentTime += Time.fixedDeltaTime;
            if (instant)
            {
                EndAction();

            } else
            {
                if (currentTime >= activeTime)
                {
                    EndAction();
                }
            }

            if (currentTime >= cooldown)
            {
                ready = true;
            }

        }
    }

}