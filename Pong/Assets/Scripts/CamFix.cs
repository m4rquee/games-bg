using UnityEngine;

public class CamFix: MonoBehaviour {

	public float step = 1f;
	public float timeScale = 1;

	private Camera cam;

	void Start() {
		cam = GetComponent<Camera>();
	}

	void Update() {
		if (Input.GetKey(KeyCode.LeftControl))
			Time.timeScale += step * (Input.GetAxis("Mouse ScrollWheel"));
		else
			cam.fieldOfView += step * (Input.GetAxis("Mouse ScrollWheel"));
	}
}
