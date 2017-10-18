using UnityEngine;

public class NameRotation: MonoBehaviour {
	void Update() {
		transform.rotation = Quaternion.Euler(0, transform.parent.rotation.y + 60, 0);
	}
}
