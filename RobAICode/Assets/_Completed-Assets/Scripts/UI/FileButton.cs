using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FileButton: MonoBehaviour {

	public int pathNum;
	public GameObject menu;
	public InitGame initGame;

	private static bool windowOpen;

	public void OpenFile() {
		if (!windowOpen) {
			this.menu.SetActive(false);
			FileSelector.GetFile(GotFile, ".dll");
			windowOpen = true;
		}
	}

	void GotFile(FileSelector.Status status, string path) {
		GameManager.paths[this.pathNum] = path;
		windowOpen = false;

		this.menu.SetActive(true);
	}
}
