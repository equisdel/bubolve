using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string name;
    public string description;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float movement_speed;  // escapar más rápido de los enemigos
    public float age;             // correspondencia con el tamaño en view
    public float max_health;          // cuando se termina, reinicia la ronda con una vida menos
    public float health;

    public float shield = 0;

    public List<Ability> baseAbilities;

    private int selectedAbilityIndex = 0;
    private Bubble bubble;

    
    void Awake(){
        bubble = new Bubble
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
            bubble.AddAbility(item);
            item.SetEntity(bubble, gameObject);
        }
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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedAbilityIndex = 0;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedAbilityIndex = 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 p = Input.mousePosition;
            p.z = 20;
            Vector3 pos = Camera.main.ScreenToWorldPoint(p);
            Vector3 dir = (pos - transform.position);
            dir.Scale(new Vector3(1, 0, 1));
            dir.Normalize();

            Ability ability = bubble.abilities[selectedAbilityIndex];
            if (ability != null)
            {
                if (ability.ready)
                {
                    ability.DoAction(dir);
                }
            }
        }

        transform.position += mov.normalized * bubble.movement_speed * Time.deltaTime;
    }

    void FixedUpdate(){

    }
}
