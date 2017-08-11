using UnityEngine;

public class RandSpeed: MonoBehaviour {

	public float speed;

    private Rigidbody2D rb2d;

    void Start () {
        this.rb2d = GetComponent<Rigidbody2D> ();

        this.rb2d.velocity = (Random.value > 0.5 ? Vector2.right : Vector2.left) * speed;
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "racket") {
            float dirY = (transform.position.y - collision.transform.position.y)
                / collision.collider.bounds.size.y, dirX = 0;

            if (collision.gameObject.name == "racketl")
                dirX = 1;
            else if (collision.gameObject.name == "racketr")
                dirX = -1;

            Vector2 dir = new Vector2 (dirX, dirY).normalized;

            this.rb2d.velocity = dir * this.speed;
        }
    }
}
