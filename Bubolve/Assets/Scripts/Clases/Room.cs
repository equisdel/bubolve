
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour {
    
    public List<Room> paths;
    public List<Enemy> enemies;
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
            enemies.Add(enemyController.enemy);
        }

        GetFittestEnemy();
        Debug.Log("Done");
    }

    // Al finalizar una ronda

    public void CloseRoom(int index) {  // se ejecuta cuando el jugador mata a todos los enemigos y elige una puerta
        // usa index para saber cu?l ser? la siguiente habitaci?n
        Enemy fittest_enemy = GetFittestEnemy();
        float birth_time = Time.time;
        for (int i = 0; i < enemies.Count; i++) {   // cantidad fija de enemigos en este caso, mejoras?
            paths[index].enemies.Add(new Enemy(fittest_enemy,birth_time));
        }
    }

    private void UpdateMaxValues() {
        foreach (Enemy enemy in enemies) {
            if (enemy.time_lived > max_time_lived) 
                max_time_lived = enemy.time_lived;
            if (enemy.damage_produced > max_damage_produced)
                max_damage_produced = enemy.damage_produced;
            if (enemy.GetScore() > max_overall_score)
                max_overall_score = enemy.GetScore();
        }
    }

    private float GetFitness(Enemy enemy)
    {
        return criteria[0] * enemy.time_lived / max_time_lived + criteria[1] * enemy.damage_produced / max_damage_produced + criteria[2] * enemy.GetScore() / max_overall_score;
    }

    // m?todo para seleccionar al mejor enemigo
    public Enemy GetFittestEnemy() {
        UpdateMaxValues();
        Enemy fittest = null;
        float fittest_fitness = -1;
        foreach (Enemy enemy in enemies) {
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
