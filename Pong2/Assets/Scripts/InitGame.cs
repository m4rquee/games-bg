using UnityEngine;

public class InitGame: MonoBehaviour {

	public string path = null;
	public GameObject gamesObjs;
	public GameObject btns;

	public void Init() {
		if (!string.IsNullOrEmpty(this.path)) {
			Controller ctrl = this.gamesObjs.GetComponentInChildren<Controller>();
			ctrl.Init(this.path);

			this.gamesObjs.SetActive(true);
			this.btns.SetActive(false);
		}
	}
}
