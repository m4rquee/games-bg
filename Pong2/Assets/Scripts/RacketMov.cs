using UnityEngine;
using System.Collections.Generic;

public class RacketMov {

	private float maxSpeed;

	private Rigidbody2D rb2d;

	public RacketMov(Rigidbody2D rb2dP1, float maxSpeed) {
		this.rb2d = rb2dP1;

		this.maxSpeed = maxSpeed;
	}

	public void Move(int player, List<object> vel) {
		this.rb2d.velocity = new Vector2(0, (float) (double) vel[0] * maxSpeed);
	}
}
