using UnityEngine;

public class InitGame: MonoBehaviour {

	public GameObject menu;
	public GameObject gamesObjs;

	public static string[] paths = new string[] { null, null };

	public void Init() {
		if (!string.IsNullOrEmpty(paths[0]) && !string.IsNullOrEmpty(paths[1])) {
			Controller ctrl = this.gamesObjs.GetComponentInChildren<Controller>();
			ctrl.Init(paths[0], paths[1]);

			this.gamesObjs.SetActive(true);
			this.menu.SetActive(false);
		}
	}
}
