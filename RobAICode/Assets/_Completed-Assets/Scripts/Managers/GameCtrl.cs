using System;
using BitsGalaxy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCtrl: MonoBehaviour {

	private GameManager gameManager;
	private GameObject[] tanksObjs;
	private Dictionary<string, object>[] states;
	private Dictionary<string, Action<int, float>> tankActions;

	void Start() {
		this.tankActions = new Dictionary<string, Action<int, float>>()
		{{ "move", SetMovementInputValue}, { "turn", SetTurnInputValue}};

		this.tanksObjs = GameObject.FindGameObjectsWithTag("Tank");

		var actions = new Dictionary<string, Action<int, List<object>>>
			{{ "DoAction", DoAction }};

		this.states = new Dictionary<string, object>[2];
		this.states[0] = new Dictionary<string, object>();
		this.states[1] = new Dictionary<string, object>();

		try {
			this.gameManager = new GameManager(new string[] { "", "" }, actions, 2);
		} catch (Exception e) {
			Debug.Log(e.StackTrace);
		}
	}

	void Update() {
		double p1X = this.tanksObjs[0].transform.position.x,
			p1Y = this.tanksObjs[0].transform.position.y;

		double p2X = this.tanksObjs[1].transform.position.x,
		p2Y = this.tanksObjs[1].transform.position.y;

		this.states[0]["enemy-pos"] = new ArrayList { p2X, p2Y };
		this.states[0]["player-pos"] = new ArrayList { p1X, p1Y };

		this.states[1]["enemy-pos"] = new ArrayList { p1X, p1Y };
		this.states[1]["player-pos"] = new ArrayList { p2X, p2Y };

		gameManager.Update(this.states);
	}

	//p[0] = "action1-action2-action3- ... -actionn", p[1] = {pA1, pA2, pA3 ... pAn}
	public void DoAction(int plyr, List<object> p) {
		string[] actions = ((string) p[0]).Split('-');

		for (int i = 0; i < actions.Length; i++)
			this.tankActions[actions[i]](plyr, (float) (double) p[i]);
	}

	private void SetMovementInputValue(int plyr, float v) {
		v /= 100;
		if (v > 1)
			v = 1;
		else if (v < -1)
			v = -1;

		this.tanksObjs[plyr].GetComponent<Complete.TankMovement>().M_MovementInputValue = v;
	}

	private void SetTurnInputValue(int plyr, float v) {
		v /= 100;
		if (v > 1)
			v = 1;
		else if (v < -1)
			v = -1;

		this.tanksObjs[plyr].GetComponent<Complete.TankMovement>().M_TurnInputValue = v;
	}
}
