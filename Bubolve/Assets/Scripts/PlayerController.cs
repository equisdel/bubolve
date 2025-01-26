using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string name;
    public string description;

    public CharacterController characterController;
    public Animator animator;
    public float maxGrowFrame;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float movement_speed;  // escapar más rápido de los enemigos
    public float attack_damage;  // escapar más rápido de los enemigos
    public float age;             // correspondencia con el tamaño en view
    public float max_health;          // cuando se termina, reinicia la ronda con una vida menos
    public float health;

    public float shield = 0;

    public List<Ability> baseAbilities;

    //UI
    public GameObject abilityUIPrefab;
    public Transform abilityUIParent;

    public Slider lifeSlider;

    private List<AbilityUI> abilitiesUI = new List<AbilityUI>();

    private int selectedAbilityIndex = 0;
    public Bubble bubble;

    
    void Awake(){
        bubble = new Bubble
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
            thickness = thickness
        };

        for (int i = 0; i < baseAbilities.Count; i++)
        {
            Ability item = baseAbilities[i];

            bubble.AddAbility(item);
            item.SetEntity(bubble, gameObject);

            GameObject abilityUIGameObject = Instantiate(abilityUIPrefab, abilityUIParent);
            AbilityUI abilityUI = abilityUIGameObject.GetComponent<AbilityUI>();
            abilityUI.SetData(i + 1, Mathf.Min(1, item.currentTime / item.cooldown), item.ended, item.ready, selectedAbilityIndex == i);
            abilitiesUI.Add(abilityUI);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = new Vector3(0, -1, 0);
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

        characterController.Move(mov.normalized * bubble.movement_speed * Time.deltaTime);

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

            var groundPlane = new Plane(Vector3.up, 0.5f);
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance;

            if (groundPlane.Raycast(mouseRay, out hitDistance))
            {
                Vector3 pos = mouseRay.GetPoint(hitDistance);
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
        }
    }

    void FixedUpdate(){
        //Update UI
        

        for (int i = 0; i < baseAbilities.Count; i++)
        {
            Ability item = baseAbilities[i];
            AbilityUI abilityUI = abilitiesUI[i];
            abilityUI.SetData(i + 1, Mathf.Min(1, item.currentTime/item.cooldown), item.ended, item.ready, selectedAbilityIndex == i);
            abilitiesUI.Add(abilityUI);
        }

        lifeSlider.value = bubble.health / bubble.max_health;
    }

    internal void Grow(float v)
    {
        age += v;
        bubble.age += v;


    }
}
