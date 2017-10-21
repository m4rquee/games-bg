using UnityEngine;

public class InitGame: MonoBehaviour {

	public static string path = null;
	public GameObject gamesObjs;
	public GameObject btns;

	public void Init() {
		if (!string.IsNullOrEmpty(path)) {
			Controller ctrl = this.gamesObjs.GetComponentInChildren<Controller>();
			ctrl.Init(path);

			this.gamesObjs.SetActive(true);
			this.btns.SetActive(false);
		}
	}
}
