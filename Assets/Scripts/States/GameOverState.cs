using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class GameOverState : SabotageState {

    public override IEnumerator OnStateEnter() {
      ResetPlayer();
      yield return new WaitForSecondsRealtime(1.0f);
      Game.State = new PlayingState();
    }

    private void ResetPlayer() {
      Game.Data.Player.GetComponent<PlayerControl>().enabled = false;
      Game.Data.Player.GetComponent<Renderer>().material.color = Color.red;
    }
  }
}
