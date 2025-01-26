using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Room {
    
    public List<Room> paths;
    public List<Enemy> enemies;
    public bool exit;

    int max_time_lived = 0;
    int max_damage_produced = 0;
    int max_overall_power = 0;


    private float[] criteria = new float[3] { 0.3F, 0.5F, 0.2F };   // debería sumar siempre 1
    private float GetFitness(Enemy enemy)
    {
        return criteria[0] * enemy.time_lived / max_time_lived + criteria[1] * enemy.damage_produced / max_damage_produced + criteria[2] * enemy.overall_power / max_overall_power;
    }
    // método para seleccionar al mejor
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


}
