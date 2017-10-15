using System;
using BitsGalaxy;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameCtrl: MonoBehaviour {

	private GameManager gameManager;

	private Text[] tanksName;
	private Complete.TankMovement[] tanksMov;
	private Complete.TankShooting[] tanksShoot;

	private GameObject[] tanksObjs;
	private Dictionary<string, object>[] states;
	private Dictionary<string, Action<int, object>> tankActions;

	private float min;
	private float max;

	void Start() {
		this.tankActions = new Dictionary<string, Action<int, object>>()
		{{ "move", SetMovementInputValue}, { "turn", SetTurnInputValue},
			{ "look_to_enemy", LookToEnemy}, {"fire", Fire } };

		this.tanksObjs = null;

		var actions = new Dictionary<string, Action<int, List<object>>>
			{{ "DoAction", DoAction }};

		this.states = new Dictionary<string, object>[2];
		this.states[0] = new Dictionary<string, object>();
		this.states[1] = new Dictionary<string, object>();

		this.tanksName = new Text[2];
		this.tanksMov = new Complete.TankMovement[2];
		this.tanksShoot = new Complete.TankShooting[2];

		try {
			this.gameManager = new GameManager(new string[] { "D:\\PD\\TCC\\Tanks.dll" }, actions, 2);
		} catch (Exception e) {
			Debug.Log(e.StackTrace);
		}
	}

	void Update() {
		if (this.tanksObjs == null) {
			this.tanksObjs = GameObject.FindGameObjectsWithTag("Tank");

			this.tanksName[0] = this.tanksObjs[0].transform.GetChild(0).GetChild(0).GetComponent<Text>();
			this.tanksName[1] = this.tanksObjs[1].transform.GetChild(0).GetChild(0).GetComponent<Text>();

			var names = this.gameManager.PlyrsName;
			this.tanksName[0].text = names[0];
			this.tanksName[1].text = names[1];

			this.tanksMov[0] = this.tanksObjs[0].GetComponent<Complete.TankMovement>();
			this.tanksMov[1] = this.tanksObjs[1].GetComponent<Complete.TankMovement>();

			this.tanksShoot[0] = this.tanksObjs[0].GetComponent<Complete.TankShooting>();
			this.tanksShoot[1] = this.tanksObjs[1].GetComponent<Complete.TankShooting>();

			this.min = Complete.TankShooting.m_MinLaunchForce;
			this.max = Complete.TankShooting.m_MaxLaunchForce;
		}

		double p1X = this.tanksObjs[0].transform.position.x,
			p1Y = this.tanksObjs[0].transform.position.z,
			p1R = this.tanksObjs[0].transform.rotation.eulerAngles.y;

		double p2X = this.tanksObjs[1].transform.position.x,
			p2Y = this.tanksObjs[1].transform.position.z,
			p2R = this.tanksObjs[1].transform.rotation.eulerAngles.y;

		this.states[0]["enemy-pos"] = new ArrayList { p2X, p2Y };
		this.states[0]["player-info"] = new ArrayList { p1X, p1Y, p1R };

		this.states[1]["enemy-pos"] = new ArrayList { p1X, p1Y };
		this.states[1]["player-info"] = new ArrayList { p2X, p2Y, p2R };

		this.gameManager.Update(this.states);
	}

	//p[0] = "action1-action2-action3- ... -actionn", p[1] = {pA1, pA2, pA3 ... pAn}
	public void DoAction(int plyr, List<object> p) {
		string[] actions = ((string) p[0]).Split('-');

		for (int i = 0; i < actions.Length; i++)
			this.tankActions[actions[i]](plyr, ((List<object>) p[1])[i]);
	}

	private void SetMovementInputValue(int plyr, object o) {
		var v = (float) (double) o;
		v /= 100;
		if (v > 1)
			v = 1;
		else if (v < -1)
			v = -1;

		this.tanksMov[plyr].M_MovementInputValue = v;
	}

	private void SetTurnInputValue(int plyr, object o) {
		var v = (float) (double) o;
		v /= 100;
		if (v > 1)
			v = 1;
		else if (v < -1)
			v = -1;

		this.tanksMov[plyr].M_TurnInputValue = v;
	}

	private void LookToEnemy(int plyr, object o) {
		int enemy = plyr == 0 ? 1 : 0;
		this.tanksMov[plyr].transform.LookAt(this.tanksMov[enemy].transform);
	}

	private void Fire(int plyr, object o) {
		var f = (float) (double) o;
		f /= 1000;
		if (f > 0.1)
			f = 0.1f;

		f *= (this.max - this.min);
		f += this.min;

		if (!this.tanksShoot[plyr].shooting)
			StartCoroutine(this.tanksShoot[plyr].Fire(f));
	}
}
