#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class FileChooser: MonoBehaviour {

	public InitGame initGame;

	public void OpenFile() {
#if UNITY_EDITOR
		InitGame.path = EditorUtility.OpenFilePanel("Selecione uma dll", "C:\\Users", "dll");
#endif
	}
}