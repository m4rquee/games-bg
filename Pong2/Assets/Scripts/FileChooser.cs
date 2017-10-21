using UnityEngine;

public class FileChooser: MonoBehaviour {

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
		InitGame.path = path;
		windowOpen = false;

		this.menu.SetActive(true);
	}
}