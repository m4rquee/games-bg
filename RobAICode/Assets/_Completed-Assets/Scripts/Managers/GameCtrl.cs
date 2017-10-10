using System;
using BitsGalaxy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCtrl: MonoBehaviour {

	private GameManager gameManager;
	private Dictionary<string, object>[] states;
	private Complete.TankMovement[] tanks = new Complete.TankMovement[2];

	// Use this for initialization
	void Start() {
		var tanksObjs = GameObject.FindGameObjectsWithTag("Tank");
		this.tanks[0] = tanksObjs[0].GetComponent<Complete.TankMovement>();
		this.tanks[1] = tanksObjs[1].GetComponent<Complete.TankMovement>();

		try {
			var actions = new Dictionary<string, Action<int, List<object>>>
			{{ "movement", Movement }};

			this.gameManager = new GameManager(new string[] { "", "" }, actions, 2);

			this.states = new Dictionary<string, object>[2];
			this.states[0] = new Dictionary<string, object>();
			this.states[1] = new Dictionary<string, object>();
		} catch (Exception e) {
			Debug.Log(e.StackTrace);
		}
	}

	void Update() {

	}

	public void Movement(int plyrs, List<object> p) { }
}
