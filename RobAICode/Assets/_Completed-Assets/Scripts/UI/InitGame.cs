using Complete;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame: MonoBehaviour {
	public void Init() {
		if (!string.IsNullOrEmpty(GameManager.paths[0]))
			SceneManager.LoadScene(1);
	}
}