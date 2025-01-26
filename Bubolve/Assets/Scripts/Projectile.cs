using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public float radius;
    public float lifeTime;
    public float damage;

    public GameObject source;
    public Entity attacker;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != source)
        {

            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                attacker.MakeDamage(damage, playerController.bubble);
                //playerController.bubble.TakeDamage(damage);
                Destroy(gameObject);
            }

            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                attacker.MakeDamage(damage, enemyController.enemy);
                //enemyController.enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
