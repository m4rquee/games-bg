using UnityEngine;

public class InitGame: MonoBehaviour {

	public static string[] paths = new string[] { null, null };
	public GameObject gamesObjs;
	public GameObject btns;

	public void Init() {
		if (!string.IsNullOrEmpty(paths[0]) && !string.IsNullOrEmpty(paths[1])) {
			Controller ctrl = this.gamesObjs.GetComponentInChildren<Controller>();
			ctrl.Init(paths[0], paths[1]);

			this.gamesObjs.SetActive(true);
			this.btns.SetActive(false);
		}
	}
}
