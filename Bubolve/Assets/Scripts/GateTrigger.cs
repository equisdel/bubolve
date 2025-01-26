using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public GameObject bubbleGameObject;
    public Room room;
    public float grow = 0.1f;


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
}
