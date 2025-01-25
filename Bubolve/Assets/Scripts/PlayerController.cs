using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    void Awake(){

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            mov += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            mov += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            mov += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mov += new Vector3(0, 0, -1);
        }

        transform.position += mov.normalized * speed * Time.deltaTime;
    }

    void FixedUpdate(){

    }
}
