using System.Collections;
using System.Collections.Generic;

// unidad de evolución

public class Enemy: ParentEntity {

    public static List<float> evolutionary_state; // probabilidades de habilidades
    public float detection_radius;  // escala?


    // constructor: relativamente bajo, pensar bien en los valores iniciales para que tenga chances de evolucionar
    // evolucionar: dar luz a la siguiente generación de enemigos
    // atacar: la probabilidad de atacar debe relacionarse directamente con el puntaje, es decir, los primeros enemigos son más mansos
    // los enemigos saben donde está el jugador, pero una habilidad podría ser persecución (?) -> Los datos de posición están en Unity?

    

} 