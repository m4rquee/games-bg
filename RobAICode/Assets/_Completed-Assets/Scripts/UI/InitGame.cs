using Complete;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InitGame: MonoBehaviour {
	public void Init() {
		if (!string.IsNullOrEmpty(GameManager.paths[0])
			&& !string.IsNullOrEmpty(GameManager.paths[1]))
			SceneManager.LoadScene(1);
	}
}