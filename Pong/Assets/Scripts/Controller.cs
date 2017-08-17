using System;
using BitsGalaxy;
using UnityEngine;
using System.Collections.Generic;

public class Controller: MonoBehaviour {
    public GameObject ball;

    public GameObject player1;
    public GameObject player2;

    public float maxSpeed;

    private RacketMov racketMov;
    private GameManager gameManager;

    private Dictionary<string, object>[] states;

    void Start() {
        Time.timeScale = 3;
        this.racketMov = new RacketMov(player1.GetComponent<Rigidbody2D>(),
            player2.GetComponent<Rigidbody2D>(), this.maxSpeed);

        var actions = new Dictionary<string, Action<int, List<object>>>
        {{ "move", this.racketMov.move }};

        this.gameManager = new GameManager(new string[]
            { "F:\\PD\\TCC\\MyRobot.dll", "F:\\PD\\TCC\\MyRobot.dll" }, actions);

        this.states = new Dictionary<string, object>[2];
    }

    void Update() {
        this.states[0] = new Dictionary<string, object>();
        this.states[1] = new Dictionary<string, object>();

        float ballX = this.ball.transform.position.x,
        ballY = this.ball.transform.position.y;

        float plyrX = this.player1.transform.position.x,
        plyrY = this.player1.transform.position.y;

        this.states[0].Add("ball-pos", new float[] { ballX, ballY });
        this.states[0].Add("player-pos", new float[] { plyrX, plyrY });

        plyrX = this.player2.transform.position.x;
        plyrY = this.player2.transform.position.y;

        this.states[1].Add("ball-pos", new float[] { ballX, ballY });
        this.states[1].Add("player-pos", new float[] { plyrX, plyrY });

        gameManager.update(this.states);
    }
}