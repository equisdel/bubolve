using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public string name;
    public string description;

    public GameObject bubbleGameObject;
    public CharacterController characterController;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float movement_speed;  // escapar más rápido de los enemigos
    public float attack_damage;  // Daño
    public float age;             // correspondencia con el tamaño en view
    public float max_health;          // cuando se termina, reinicia la ronda con una vida menos
    public float health;
    public float birth;

    public float shield = 0;

    public List<Ability> baseAbilities;

    public Enemy enemy;

    public Slider lifeSlider;

    void Start()
    {
        enemy = new Enemy
        {
            age = age,
            description = description,
            health = health,
            max_health = max_health,
            movement_speed = movement_speed,
            attack_damage = attack_damage,
            name = name,
            shield = shield,
            size = size,
            thickness = thickness,
            birth = birth
        };

        foreach (var item in baseAbilities)
        {
            enemy.AddAbility(item);
            item.SetEntity(enemy, gameObject, bubbleGameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.health > 0)
        {
            int abilityIndex = Random.Range(0, enemy.abilities.Count); 

            Vector3 dir = (bubbleGameObject.transform.position - gameObject.transform.position).normalized;

            Ability ability = enemy.abilities[abilityIndex];
            if (ability != null)
            {
                if (ability.ready)
                {
                    ability.DoAction(dir);
                }
            }

            Vector3 mov = new Vector3(0, -1, 0);
            characterController.Move(mov.normalized * Time.deltaTime);
        } else
        {

        }
    }

    void FixedUpdate()
    {
        lifeSlider.value = enemy.health / enemy.max_health;
    }
}
