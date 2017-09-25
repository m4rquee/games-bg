using UnityEngine;
using System.Collections.Generic;

public class RacketMov {

	private float maxSpeed;

	private Rigidbody2D[] rb2d;

	public RacketMov(Rigidbody2D rb2dP1, Rigidbody2D rb2dP2, float maxSpeed) {
		this.rb2d = new Rigidbody2D[2];

		this.rb2d[0] = rb2dP1;
		this.rb2d[1] = rb2dP2;

		this.maxSpeed = maxSpeed;
	}

	public void Move(int player, List<object> vel) {
		this.rb2d[player].velocity = new Vector2(0, (float) (double) vel[0] * maxSpeed);
	}
}
