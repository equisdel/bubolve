using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour {
    
    public List<EnemyController> enemies;
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public GameObject bubbleGameObject;
    public PlayerController playerController;
    public bool exit;
    public int enemyAmmount = 5;
    public AnimationCurve curve;

    private float[] criteria = new float[3] { 0.3F, 0.5F, 0.2F };   // deber?a sumar siempre 1
    private float max_time_lived = 0;
    private float max_damage_produced = 0;
    private float max_overall_score = 0;

    private class Mutation {

        public enum Mutations { SIZE, THICKNESS, MOV_SPEED, MAX_HEALTH, P_SHIELD, P_BLAST_WAVE, P_BUBBLE_CANNON, P_CHASE };
        float p_mutation;
        AnimationCurve values;

        public Mutation(float p, AnimationCurve values) {
            p_mutation = p;
            this.values = values;
        }

        public float RollDice() {   // retorno entre -1 y 1, probabilidad no uniforme.
            float alteration = 0.0F;
            float random = Random.Range(0, 1);
            if (random <= p_mutation)
            {     // define la probabilidad de que se efect?e o no un cambio
                alteration = (values.Evaluate(Random.Range(0, 1)) - 0.5F) * 2;   // define el tipo de mutaci?n: [-1,+1]
            }
            return alteration;
        }

        public void Mutate(Enemy padre, Enemy hijo) {
            
            foreach (string mutable in System.Enum.GetValues(typeof(Mutations))) {
                float alteration = RollDice();  // valor de -1 a 1, m?s probablemente vale 0.
                {
                    switch (System.Enum.Parse(typeof(Mutations), mutable)) { 
                        case Mutations.SIZE:
                            hijo.size = Mathf.Min(Mathf.Max(padre.size + alteration * padre.size, 0), 1);
                            break;
                        case Mutations.THICKNESS:
                            hijo.thickness = Mathf.Min(Mathf.Max(padre.thickness + alteration * padre.thickness, 0), 1);
                            break;
                        case Mutations.MOV_SPEED:
                            hijo.movement_speed = Mathf.Min(Mathf.Max(padre.movement_speed + alteration * padre.movement_speed, 0), 1);
                            break;
                        case Mutations.MAX_HEALTH:
                            hijo.max_health = Mathf.Min(Mathf.Max(padre.max_health + alteration * padre.max_health, 0), 1);
                            break;
                        case Mutations.P_SHIELD:
                            hijo.stats[0] = Mathf.Min(Mathf.Max(padre.stats[0] + alteration * padre.stats[0], 0), 1);
                            break;
                        case Mutations.P_BLAST_WAVE:
                            hijo.stats[1] = Mathf.Min(Mathf.Max(padre.stats[1] + alteration * padre.stats[1], 0), 1);
                            break;
                        case Mutations.P_BUBBLE_CANNON:
                            hijo.stats[2] = Mathf.Min(Mathf.Max(padre.stats[2] + alteration * padre.stats[2], 0), 1);
                            break;
                        case Mutations.P_CHASE:
                            hijo.stats[3] = Mathf.Min(Mathf.Max(padre.stats[3] + alteration * padre.stats[3], 0), 1);
                            break;
                        default:
                            
                            break;
                    }
                    
                
                }
            }

        }



        // se itera sobre todas las cualidades, todas con igual probabilidad de mutar

    }
   

    public bool complete { get
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].enemy.health > 0) return false;
            }
            return true;
        }
    }

    public void Start()
    {
        float xd = Random.Range(0f,1f);
        curve.Evaluate(xd);
        Mutation mutation = new Mutation(0.1F, curve);
        for (int i = 0; i < enemyAmmount; i++)
        {
            GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity, transform);
            EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
            enemyController.bubbleGameObject = bubbleGameObject;
            enemies.Add(enemyController);
        }

        //GetFittestEnemy();
        Debug.Log("Done");
    }



    // Al finalizar una ronda

    public void CloseRoom(float grow) {  // se ejecuta cuando el jugador mata a todos los enemigos y elige una puerta
        // usa index para saber cu?l ser? la siguiente habitaci?n
        EnemyController fittest_enemy = GetFittestEnemy();
        Mutation mutation = new Mutation(0.5F,curve);
        List<EnemyController> tempEnemies = new List<EnemyController>(enemies);
        enemies.Clear();
        float birth_time = Time.time;
        for (int i = 0; i < enemyAmmount; i++) {   // cantidad fija de enemigos en este caso, mejoras?

            GameObject enemyGameObject = Instantiate(fittest_enemy.gameObject, spawnPoints[i].position, Quaternion.identity, transform);
            EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
            enemyController.bubbleGameObject = bubbleGameObject;
            enemyController.birth = birth_time;
            enemyController.Init();
            mutation.Mutate(fittest_enemy.enemy,enemyController.enemy);

            // seteo las mutaciones basadas en fittest_enemy
            //enemyController.enemy
            //fittest_enemy.enemy

            enemies.Add(enemyController);
        }

        for (int i = tempEnemies.Count - 1; i >= 0; i--)
        {
            Destroy(tempEnemies[i].gameObject);
        }

        playerController.Grow(grow);
    }

    private void UpdateMaxValues() {
        foreach (EnemyController enemyController in enemies) {
            if (enemyController.enemy.time_lived > max_time_lived) 
                max_time_lived = enemyController.enemy.time_lived;
            if (enemyController.enemy.damage_produced > max_damage_produced)
                max_damage_produced = enemyController.enemy.damage_produced;
            if (enemyController.enemy.GetScore() > max_overall_score)
                max_overall_score = enemyController.enemy.GetScore();
        }
    }

    private float GetFitness(EnemyController enemyController)
    {
        return criteria[0] * enemyController.enemy.time_lived / max_time_lived + criteria[1] * enemyController.enemy.damage_produced / max_damage_produced + criteria[2] * enemyController.enemy.GetScore() / max_overall_score;
    }

    // m?todo para seleccionar al mejor enemigo
    public EnemyController GetFittestEnemy() {
        UpdateMaxValues();
        EnemyController fittest = null;
        float fittest_fitness = float.MinValue;
        foreach (EnemyController enemy in enemies) {
            float actual_fitness = GetFitness(enemy);
            if (actual_fitness > fittest_fitness) { 
                fittest = enemy;
                fittest_fitness = actual_fitness;
            }
        }
        //Debug.Log("Meilleur ennemie: " + fittest.name + " with " + actual_fitness);
        return fittest;
    }
}
