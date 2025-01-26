using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public float radius;
    public float lifeTime;

    private float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if(currentTime >= lifeTime)
        {
            Destroy(gameObject);
        } else
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }
}
