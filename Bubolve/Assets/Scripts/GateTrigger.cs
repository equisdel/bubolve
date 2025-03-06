using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public GameObject bubbleGameObject;
    public GameObject portalGameObject;
    public float rotateSpeed;
    public Room room;
    public float grow = 0.1f;

    private void Start()
    {
        SetPortal(portalGameObject);
    }

    private void Update()
    {
        portalGameObject.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == bubbleGameObject)
        {
            if (room.complete)
            {
                room.CloseRoom(grow);
            }
        }
    }

    public void SetPortal(bool state)
    {
        portalGameObject.SetActive(state);
    }
}
