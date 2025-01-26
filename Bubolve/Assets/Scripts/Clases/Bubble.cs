
using Unity.VisualScripting;

public class Bubble: ParentEntity {

    public int lives;   // default: 5

    public override void Die() {
        base.Die(); // llama al método de Entity
        lives--;    // y después decrementa las vidas
        if (lives == 0) {
            // CASO 1: El jugador perdió porque se rompió la burbuja (nave)
        }
    }

}