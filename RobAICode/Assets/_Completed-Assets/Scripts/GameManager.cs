using System;
using System.Reflection;
using System.Collections.Generic;

using State = System.Collections.Generic.Dictionary<string, object>;

namespace BitsGalaxy {
	public class GameManager {
		private List<IRobot> players;
		private Dictionary<string, Action<int, List<object>>> actions;

		private List<string> plyrsName;

		public List<string> PlyrsName {
			get {
				return this.plyrsName;
			}
		}

		public GameManager(String[] playersPaths, Dictionary<string, Action<int, List<object>>> actions, int numPlyrs) {
			if (numPlyrs <= 0)
				throw new Exception("Invalid number of players");

			this.players = new List<IRobot>();
			this.plyrsName = new List<string>();
			InitPlayers(playersPaths, numPlyrs);

			if (actions == null)
				throw new Exception("Null action Dictionary");

			foreach (var entry in actions)
				if (entry.Key == null || entry.Value == null)
					throw new Exception("Found null action on Dictionary");

			this.actions = actions;
		}

		private void InitPlayers(String[] playersPaths, int numPlyrs) {
			if (playersPaths == null)
				throw new Exception("Null path array");

			List<Assembly> DLLs = new List<Assembly>();

			foreach (String path in playersPaths)
				if (path == null)
					throw new Exception("Null path in array");
				else
					DLLs.Add(Assembly.LoadFile(@path));

			int index = 1;
			foreach (Assembly dll in DLLs) {
				bool found = false;

				Type[] types = dll.GetExportedTypes();
				foreach (Type type in types)
					if (type.GetInterface("IRobot") != null) {
						IRobot playerObj = (IRobot) Activator.CreateInstance(type);
						this.players.Add(playerObj);
						found = true;

						this.plyrsName.Add(type.Name);
					}

				if (!found)
					throw new Exception("Could not find an IRobot class on the " + index + "th dll");

				index++;
			}

			if (this.players.Count != numPlyrs)
				throw new Exception("Invalid number of players");
		}

		public void SetAction(string actionName, Action<int, List<object>> actionFunc) {
			this.actions[actionName] = actionFunc;
		}

		public void RemoveAction(string actionName) {
			this.actions.Remove(actionName);
		}

		public void Update(State[] states) {
			if (states == null)
				throw new Exception("Null state array");

			if (states.Length != this.players.Count)
				throw new Exception("Invalid number of states");

			ActionBlock playerAction;
			for (int i = 0; i < this.players.Count; i++) {
				State currentState = states[i];

				if (currentState == null)
					throw new Exception("Null state on state array");

				playerAction = players[i].Update(currentState);
				this.actions[playerAction.actionName](i, playerAction.args);
			}
		}
	}
}
