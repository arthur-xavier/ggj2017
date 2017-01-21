using GameTemplate;
using UnityEngine;

namespace Sabotage {

  public class GUIManager : SingletonBehaviour<Sabotage, Settings, GUIManager> {

    void OnGUI() {
      if (Game.State is GameOverState) {
        DisplayGameOverGUI();
      }
    }

    private void DisplayGameOverGUI() {
      GUI.Label(
        new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2),
        "Game over"
        );
    }
  }
}
