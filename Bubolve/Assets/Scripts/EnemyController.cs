using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string name;
    public string description;

    public GameObject bubbleGameObject;
    public CharacterController characterController;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float movement_speed;  // escapar más rápido de los enemigos
    public float age;             // correspondencia con el tamaño en view
    public float max_health;          // cuando se termina, reinicia la ronda con una vida menos
    public float health;

    public float shield = 0;

    public List<Ability> baseAbilities;

    private Enemy enemy;


    void Awake()
    {
        enemy = new Enemy
        {
            age = age,
            description = description,
            health = health,
            max_health = max_health,
            movement_speed = movement_speed,
            name = name,
            shield = shield,
            size = size,
            thickness = thickness
        };

        foreach (var item in baseAbilities)
        {
            enemy.AddAbility(item);
            item.SetEntity(enemy, gameObject, bubbleGameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int hability = Random.Range(0, enemy.abilities.Count); 

        Vector3 dir = (bubbleGameObject.transform.position - gameObject.transform.position).normalized;

        Ability ability = enemy.abilities[hability];
        if (ability != null)
        {
            if (ability.ready)
            {
                ability.DoAction(dir);
            }
        }
    }

    void FixedUpdate()
    {

    }
}
