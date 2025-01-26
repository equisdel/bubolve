
using Unity.VisualScripting;

public class Bubble: ParentEntity {

    public int lives;   // default: 5

    public override void Die() {
        base.Die(); // llama al m�todo de Entity
        lives--;    // y despu�s decrementa las vidas
        if (lives == 0) {
            // CASO 1: El jugador perdi� porque se rompi� la burbuja (nave)
        }
    }

}