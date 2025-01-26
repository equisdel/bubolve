using System;

[Serializable]
public class Entity {
    
    public string name;
    public string description;

    public float size;          // radio
    public float thickness;     // afecta que tanto impactan los ataques en la pérdida de health
    public float attack_damage;  // escapar más rápido de los enemigos
    public float movement_speed;  // escapar más rápido de los enemigos
    public float age;             // correspondencia con el tamaño en view
    public float max_health;  
    public float health;

    public float shield = 0;

    public enum Qualities { AGE, HEALTH, MOVEMENT_SPEED, ATTACK_DAMAGE, SIZE, THICKNESS, SHIELD }; 

    public void ModifyQuality(Qualities qualities, float value)
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
            case Qualities.ATTACK_DAMAGE:
                attack_damage += value;
                break;
            case Qualities.SIZE:
                size += value;
                break;
            case Qualities.THICKNESS:
                thickness += value;
                break;
            case Qualities.SHIELD:
                shield += value;
                break;
            default:    
                Console.Write("Caso default: no hay tal cualidad.");
                break;
                  
        }

    }

    public virtual float MakeDamage(float value, Entity victim) { 
        return victim.TakeDamage(value);
    }

    // recibir daño
    public float TakeDamage(float value) {
        // tener en cuenta el escudo y el grosor de la burbuja
        float value_after_shield = (value - (value / 100 * shield));
        float value_after_thickness = (value_after_shield - (value_after_shield / 100 * thickness));
        health -= value_after_thickness;
        if (health <= 0) { 
            Die(); 
        }
        return value_after_thickness;
    }

    public virtual void Die() { 
        // ni idea qué pasa acá, muy filosófico todo
    }

}

