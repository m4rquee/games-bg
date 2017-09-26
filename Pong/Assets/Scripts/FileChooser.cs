using UnityEngine;
using UnityEditor;

public class FileChooser: MonoBehaviour {

	public int pathNum;
	public InitGame initGame;

	public void OpenFile() {
		this.initGame.paths[this.pathNum] = EditorUtility.OpenFilePanel("Selecione uma dll", "C:\\Users\u15182\\Desktop", "dll");
	}
}