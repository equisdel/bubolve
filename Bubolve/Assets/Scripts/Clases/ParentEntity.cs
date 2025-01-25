using System.Collections;
using System.Collections.Generic;

public class ParentEntity: Entity {
    
    public Entity child;
    public List<Ability> abilities;
    
    
    // tiene habilidades

    // hace crecer al niño 
    public void GrowChild() {
        child.age++;
    }

    public void ExecuteAbility(int index) {
        abilities[index].DoAction();
    }

    public void EvolveAbility(int index) {
        abilities[index].Evolve();  // GeneMarket?
    }

 


}