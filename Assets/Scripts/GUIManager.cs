using GameTemplate;
using UnityEngine;

namespace Sabotage {

  public class GUIManager : PersistentSingletonBehaviour<Sabotage, Settings, GUIManager> {

    void OnGUI() {
      if (Game.State is GameOverState) {
        DisplayGameOverGUI();
      }
      else if (Game.State is WinState) {
        DisplayWinGUI();
      }
    }

    private void DisplayGameOverGUI() {
      GUI.Label(
        new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2),
        "Game over"
        );
    }

    private void DisplayWinGUI() {
      GUI.Label(
        new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2),
        "You win!!!!!!!!!!!"
        );
    }
  }
}
