
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour {
    
    public List<Room> paths;
    public List<EnemyController> enemies;
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public GameObject bubbleGameObject;
    public bool exit;
    public int enemyAmmount = 5;
    public AnimationCurve curve;

    private float[] criteria = new float[3] { 0.3F, 0.5F, 0.2F };   // deber?a sumar siempre 1
    private float max_time_lived = 0;
    private float max_damage_produced = 0;
    private float max_overall_score = 0;

    public void Start()
    {
        float xd = Random.Range(0f,1f);
        curve.Evaluate(xd);

        for (int i = 0; i < enemyAmmount; i++)
        {
            GameObject enemyGameObject = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity, transform);
            EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
            enemyController.bubbleGameObject = bubbleGameObject;
            enemies.Add(enemyController);
        }

        GetFittestEnemy();
        Debug.Log("Done");
    }



    // Al finalizar una ronda

    public void CloseRoom(int index) {  // se ejecuta cuando el jugador mata a todos los enemigos y elige una puerta
        // usa index para saber cu?l ser? la siguiente habitaci?n
        EnemyController fittest_enemy = GetFittestEnemy();
        float birth_time = Time.time;
        for (int i = 0; i < enemies.Count; i++) {   // cantidad fija de enemigos en este caso, mejoras?

            GameObject enemyGameObject = Instantiate(fittest_enemy.gameObject, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity, transform);
            EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
            enemyController.bubbleGameObject = bubbleGameObject;
            enemyController.enemy.birth = birth_time;
            paths[index].enemies.Add(enemyController);
        }
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
        float fittest_fitness = -1;
        foreach (EnemyController enemy in enemies) {
            float actual_fitness = GetFitness(enemy);
            if (actual_fitness > fittest_fitness) { 
                fittest = enemy;
                fittest_fitness = actual_fitness;
            }
            Debug.Log("Meilleur ennemie: "+fittest.name);
        }
        return fittest;
    }
}
