using System;
using BitsGalaxy;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlyrsCtrl: MonoBehaviour {
	private GameManager gameManager;

	private Text[] tanksName;
	private Transform[] tanksTrans;
	private Complete.TankMovement[] tanksMov;
	private Complete.TankShooting[] tanksShoot;

	private Dictionary<string, object>[] states;
	private Dictionary<string, Action<int, object>> tankActions;

	private float min;
	private float max;

	public void Init(Complete.TankManager[] tanks, string[] paths) {
		var actions = new Dictionary<string, Action<int, List<object>>>
			{{ "DoAction", DoAction }};

		this.gameManager = new GameManager(paths, actions, 2);

		GetTanksComponents(tanks);
		SetNames(this.gameManager.PlyrsName);

		this.states = new Dictionary<string, object>[]
			{ new Dictionary<string, object>(), new Dictionary<string, object>() };

		this.tankActions = new Dictionary<string, Action<int, object>>()
			{ { "move", SetMovementInputValue}, { "turn", SetTurnInputValue},
			{ "look_to_enemy", LookToEnemy}, {"fire", Fire } };

		this.min = Complete.TankShooting.m_MinLaunchForce;
		this.max = Complete.TankShooting.m_MaxLaunchForce;
	}

	private void GetTanksComponents(Complete.TankManager[] tanks) {
		this.tanksName = new Text[2];
		this.tanksTrans = new Transform[2];
		this.tanksMov = new Complete.TankMovement[2];
		this.tanksShoot = new Complete.TankShooting[2];

		this.tanksTrans[0] = tanks[0].m_Instance.transform;
		this.tanksTrans[1] = tanks[1].m_Instance.transform;

		this.tanksName[0] = tanks[0].m_NameText;
		this.tanksName[1] = tanks[1].m_NameText;

		this.tanksMov[0] = tanks[0].m_Movement;
		this.tanksMov[1] = tanks[1].m_Movement;

		this.tanksShoot[0] = tanks[0].m_Shooting;
		this.tanksShoot[1] = tanks[1].m_Shooting;
	}

	private void SetNames(List<string> names) {
		if (names[0] == names[1]) {
			names[0] += " 1";
			names[1] += " 2";
		}

		this.tanksName[0].text = names[0];
		this.tanksName[1].text = names[1];
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

	public void DoTurn() {
		double p1X = this.tanksTrans[0].position.x,
		p1Y = this.tanksTrans[0].position.z,
		p1R = this.tanksTrans[0].rotation.eulerAngles.y;

		double p2X = this.tanksTrans[1].position.x,
		p2Y = this.tanksTrans[1].position.z,
		p2R = this.tanksTrans[1].rotation.eulerAngles.y;

		this.states[0]["enemy-pos"] = new ArrayList { p2X, p2Y };
		this.states[0]["player-info"] = new ArrayList { p1X, p1Y, p1R };

		this.states[1]["enemy-pos"] = new ArrayList { p1X, p1Y };
		this.states[1]["player-info"] = new ArrayList { p2X, p2Y, p2R };

		this.gameManager.Update(this.states);
	}
}