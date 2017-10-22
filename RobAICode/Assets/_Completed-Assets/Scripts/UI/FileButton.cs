using Complete;
using UnityEngine;

public class FileButton: MonoBehaviour {

	public int pathNum;
	public GameObject menu;

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
