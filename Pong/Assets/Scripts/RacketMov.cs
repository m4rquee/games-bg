using UnityEngine;

public class RacketMov {

	private float maxSpeed;

    private Rigidbody2D rb2dP1;
    private Rigidbody2D rb2dP2;

    public RacketMov (Rigidbody2D rb2dP1, Rigidbody2D rb2dP2, float maxSpeed) {
        this.rb2dP1 = rb2dP1;
        this.rb2dP2 = rb2dP2;

        this.maxSpeed = maxSpeed;
    }

    public void move (int player, object [] vel) {
        if (player == 1)
            this.rb2dP1.velocity = new Vector2 (0, Mathf.Min (this.maxSpeed, (float) vel [0]));
        else
            this.rb2dP2.velocity = new Vector2 (0, Mathf.Min (this.maxSpeed, (float) vel [0]));
    }
}
