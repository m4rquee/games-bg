using System;
using BitsGalaxy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller: MonoBehaviour {
	public GameObject ball;

	public GameObject player1;

	public float maxSpeed;

	private RacketMov racketMov;
	private GameManager gameManager;

	private Dictionary<string, object>[] states;

	private bool active = false;

	public void Init(string path1) {
		try {
			this.racketMov = new RacketMov(player1.GetComponent<Rigidbody2D>(), this.maxSpeed);

			var actions = new Dictionary<string, Action<int, List<object>>> { { "move", this.racketMov.Move } };

			this.gameManager = new GameManager(new string[] { path1 }, actions, 1);

			this.states = new Dictionary<string, object>[1];
			this.states[0] = new Dictionary<string, object>();

			this.active = true;
		} catch (Exception e) {
			Debug.Log(e.StackTrace);
		}
	}

	void Update() {
		if (this.active) {
			double ballX = this.ball.transform.position.x,
			ballY = this.ball.transform.position.y;

			double plyrX = this.player1.transform.position.x,
			plyrY = this.player1.transform.position.y;

			this.states[0]["ball-pos"] = new ArrayList { ballX, ballY };
			this.states[0]["player-pos"] = new ArrayList { plyrX, plyrY };

			gameManager.Update(this.states);
		}
	}
}