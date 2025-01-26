using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Room {
    
    public List<Room> paths;
    public List<Enemy> enemies;
    public bool exit;

    private float[] criteria = new float[3] { 0.3F, 0.5F, 0.2F };   // debería sumar siempre 1
    private float max_time_lived = 0;
    private float max_damage_produced = 0;
    private float max_overall_score = 0;

    public Room() { }


    // Al finalizar una ronda

    public void CloseRoom(int index) {  // se ejecuta cuando el jugador mata a todos los enemigos y elige una puerta
        // usa index para saber cuál será la siguiente habitación
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

    // método para seleccionar al mejor enemigo
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
            Console.WriteLine("Meilleur ennemie: "+fittest.name);
        }
        return fittest;
    }

    public static void Main(String[] args) {

        // cómo pruebo solo este main?

        Room room = new Room();

        Enemy enemy1 = new Enemy(); 
        Enemy enemy2 = new Enemy(); 
        Enemy enemy3 = new Enemy();

        room.enemies.Add(enemy1);
        room.enemies.Add(enemy2);
        room.enemies.Add(enemy3);

        room.GetFittestEnemy();
        Console.WriteLine("Done");
    }
}
