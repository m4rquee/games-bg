using UnityEngine;

public class TimeCtrl: MonoBehaviour {
	public float timeStep = 0.1f;

	// Update is called once per frame
	void Update() {
		var input = Input.GetAxis("Vertical2");

		var mult = input == 0 ? 0 : Mathf.Sign(input);
		var newValue = Time.timeScale + this.timeStep * mult;

		if (newValue > 0 && newValue <= 100)
			Time.timeScale = newValue;

		Debug.Log(Time.timeScale);
	}
}
