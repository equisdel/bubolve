using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject lifeSliderFill;
    public TextMeshProUGUI lifeText;

    private bool initialized = false;

    public void Init()
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

        initialized = true;
    }

    void Start()
    {
        if (!initialized) Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.health > 0)
        {

            int abilityIndex = -1;
            /*List<float> p_acumulada = new List<float> {};
            float anterior = 0.0F;
            for (int i = 0; i < enemy.stats.Count; i++) {
                p_acumulada.Add(anterior + enemy.stats[i]);
                anterior = p_acumulada[i];
            }

            float random_score = Random.Range(0.0F, enemy.GetScore());
            for (int i = 0; i < p_acumulada.Count; i++) {
                if (random_score <= p_acumulada[i]) {
                    // se ejecuta esa acción
                    abilityIndex = i;
                    break;
                }
            }*/
            for (int i = 0; i < enemy.stats.Count; i++)
            {
                float random_score = Random.Range(0.0F, enemy.GetScore());
                if (enemy.stats[i] > random_score &&  enemy.abilities[i].ready)
                {
                    abilityIndex = i;
                    Debug.Log(enemy.abilities[abilityIndex].name);
                    break;
                }
            }

            Vector3 dir = (bubbleGameObject.transform.position - gameObject.transform.position).normalized;

            if (abilityIndex != -1)
            {
                Ability ability = enemy.abilities[abilityIndex];
                if (ability != null)
                {
                    if (ability.ready)
                    {
                        ability.DoAction(dir);
                    }
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
        lifeText.text = Mathf.CeilToInt(enemy.health).ToString();
        if (enemy.health <= 0) lifeSliderFill.SetActive(false);
    }
}
