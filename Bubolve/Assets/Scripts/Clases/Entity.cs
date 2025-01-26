using System;

public class Entity {
    
    public string name;
    public string description;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float movement_speed;  // escapar más rápido de los enemigos
    public float age;             // correspondencia con el tamaño en view
    public float max_health;          // cuando se termina, reinicia la ronda con una vida menos
    public float health;

    public enum Qualities { AGE, HEALTH, MOVEMENT_SPEED, SIZE, THICKNESS }; 

    public void ModifyQuality(Qualities qualities, int value)
    {

        // verificar que el valor sea coherente desde el mercado
        // mercado: escala el valor, puede ser negativo o positivo

        switch (qualities) {
            case Qualities.AGE: age += value; 
                break;
            case Qualities.HEALTH:
                health += value;
                max_health += value;   
                break;
            case Qualities.MOVEMENT_SPEED:
                movement_speed += value;
                break;
            case Qualities.SIZE:
                size += value;
                break;
            case Qualities.THICKNESS:
                thickness += value;
                break;
            default:    
                Console.Write("Caso default: no hay tal cualidad.");
                break;
                  
        }

    }

    // recibir daño
    public bool GetDamage(float value) {
        health -= value;
        if (health <= 0)
        {
            Die();
            return false;
        }
        return true;
    }

    public virtual void Die() { 
        // ni idea qué pasa acá, muy filosófico todo
    }

}

