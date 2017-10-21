using UnityEngine;
using UnityEngine.UI;

public class RandSpeed: MonoBehaviour {

	public float speed;
	public float randomRange;

	public Text scoreText;

	private Rigidbody2D rb2d;

	private TrailRenderer trail;

	private int[] score = new int[] { 0, 0 };

	public static System.Func<Vector2> direction =
		() => new Vector2(Random.value > 0.5 ? 1 : -1, 0);

	void Start() {
		this.rb2d = GetComponent<Rigidbody2D>();
		this.rb2d.velocity = direction() * this.speed;

		this.trail = this.GetComponent<TrailRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		switch (collision.gameObject.tag) {
			case "racket":
				float dirY = (transform.position.y - collision.transform.position.y) /
					collision.collider.bounds.size.y, dirX = 0;

				if (collision.gameObject.name == "racketl")
					dirX = 1;
				else if (collision.gameObject.name == "racketr")
					dirX = -1;

				Vector2 dir = new Vector2(dirX, dirY +
					Random.Range(-this.randomRange, this.randomRange)).normalized;

				this.rb2d.velocity = dir * this.speed;
				break;

			case "wall":
				if (collision.gameObject.name == "walll")
					this.score[0]++;
				else if (collision.gameObject.name == "wallr")
					this.score[1]++;

				this.transform.position = Vector3.zero;
				this.rb2d.velocity = direction() * this.speed;

				this.scoreText.text = System.String.Format("{0:000} - {1:000}", this.score[0], this.score[1]);

				this.trail.Clear();
				break;
		}
	}
}
