using UnityEngine;

public class CamFix: MonoBehaviour {

	public float edge = 0.5f;
	public float minSize = 1;
	public float timeStep = 1f;
	public Transform[] targets;

	private Camera cam;

	void Start() {
		cam = GetComponent<Camera>();

		Zoom();
	}

	void Update() {
		Time.timeScale += timeStep * (Input.GetAxis("Mouse ScrollWheel"));
	}

	private void Zoom() {
		cam.orthographicSize = FindRequiredSize();
	}

	private float FindRequiredSize() {
		float size = 0f;

		foreach (var target in this.targets) {
			Vector3 targetLocalPos = transform.InverseTransformPoint(target.position);

			size = Mathf.Max(size, Mathf.Abs(targetLocalPos.y));

			size = Mathf.Max(size, Mathf.Abs(targetLocalPos.x) / cam.aspect);
		}

		return Mathf.Max(size + edge, this.minSize);
	}
}
