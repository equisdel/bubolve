using System.Collections;
using System.Collections.Generic;

public class ParentEntity: Entity {
    
    public Entity child;
    public List<Ability> abilities = new List<Ability>();
    
    
    // tiene habilidades

    // hace crecer al niño 
    public void GrowChild() {
        child.age++;
    }

    public void ExecuteAbility(int index) {
        abilities[index].DoAction(); // debería retornar la cantidad de daño que produce sobre el otro
    }

    public void EvolveAbility(int index) {
        //abilities[index].Evolve();  // GeneMarket?
    }

    public void AddAbility(Ability ability)
    {
        if(abilities.IndexOf(ability) == -1)
        {
            abilities.Add(ability);
        }
    }

 


}