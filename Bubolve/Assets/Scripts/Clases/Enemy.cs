using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// unidad de evolución

public class Enemy: ParentEntity {

    public float birth;
    public float time_lived;
    public float damage_produced;

    public List<float> stats;

    public Enemy()
    {
        stats = new List<float>();
        birth = 0;
    }

    public Enemy(float birth) {
        this.birth = birth;
        // 
    }

    public override float MakeDamage(float value, Entity victim)
    {
        damage_produced += base.MakeDamage(value, victim);
        return damage_produced;
    }

    public Enemy(Enemy antecesor, float birth) {
        // mutación random controlada: determinar eso
        // ruleta: del 1 al x son cualidades, el resto son probabilidades de 
        this.birth = birth;
    }

    public float GetScore() {
        float overall_score = 0;
        foreach (float stat in this.stats) { overall_score += stat; }
        return overall_score;
    }

    public override void Die()
    {
        time_lived = Time.time - birth;
        base.Die(); // llama al método de Entity
    }

    public override void AddAbility(Ability ability)
    {
        base.AddAbility(ability);
        stats.Add(Random.Range(0.0F, 0.3F));
    }

    // Al atacar: debe registrar el daño hecho al jugador burbuja

    public static List<float> evolutionary_state; // probabilidades de habilidades
    public float detection_radius;  // escala?


    // constructor: relativamente bajo, pensar bien en los valores iniciales para que tenga chances de evolucionar
    // evolucionar: dar luz a la siguiente generación de enemigos
    // atacar: la probabilidad de atacar debe relacionarse directamente con el puntaje, es decir, los primeros enemigos son más mansos
    // los enemigos saben donde está el jugador, pero una habilidad podría ser persecución (?) -> Los datos de posición están en Unity?

    

} 