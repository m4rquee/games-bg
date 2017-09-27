using UnityEngine;

public class Human: MonoBehaviour {

	private Rigidbody2D rb2d;

	void Start() {
		this.rb2d = GetComponent<Rigidbody2D>();
	}

	void Update() {
		float input = Input.GetAxis("Vertical");

		if (input > 0)
			this.rb2d.velocity = new Vector2(0, 500f * Time.deltaTime);
		else if (input < 0)
			this.rb2d.velocity = new Vector2(0, -500f * Time.deltaTime);
		else
			this.rb2d.velocity = new Vector2(0, 0);
	}
}