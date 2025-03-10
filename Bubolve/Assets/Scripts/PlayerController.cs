using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI lifeText;

    public GameObject gameOverPanel;
    public TextMeshProUGUI roomText;
    public Room room;

    private List<AbilityUI> abilitiesUI = new List<AbilityUI>();

    private int selectedAbilityIndex = 0;
    public Bubble bubble;

    
    void Awake(){

        gameOverPanel.SetActive(false);

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
            abilityUI.SetData(i, Mathf.Min(1, item.currentTime / item.cooldown), item.ended, item.ready, selectedAbilityIndex == i || i == 0);
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
        if(bubble.health <= 0)
        {
            roomText.text = "Highest Room: " + room.room;
            gameOverPanel.SetActive(true);

            return;
        }

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
            selectedAbilityIndex = 1;

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
        if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedAbilityIndex = 2;

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
        /*if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedAbilityIndex = 3;
        }*/

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

                Ability ability = bubble.abilities[0];
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
            abilityUI.SetData(i, Mathf.Min(1, item.currentTime/item.cooldown), item.ended, item.ready, selectedAbilityIndex == i || i == 0);
            abilitiesUI.Add(abilityUI);
        }

        lifeSlider.value = bubble.health / bubble.max_health;
        lifeText.text = Mathf.CeilToInt(bubble.health).ToString();
    }

    internal void Grow(float v)
    {
        age += v;
        bubble.age += v;


    }
}
