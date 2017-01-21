using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class GameOverState : SabotageState {

    public override IEnumerator OnStateEnter() {
      Time.timeScale = 0;
      Game.Data.Player.GetComponent<Renderer>().material.color = Color.red;
      yield return new WaitForSecondsRealtime(1.0f);
      Game.State = new PlayingState();
    }

    public override IEnumerator OnStateExit() {
      yield return null;
    }
  }
}
