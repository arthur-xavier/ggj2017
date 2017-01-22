using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class WinState : SabotageState {

    public override IEnumerator OnStateEnter() {
      ResetPlayer();
      ResetEmitter();
      yield return new WaitForSecondsRealtime(2.0f);
      Game.State = new PlayingState();
    }

    private void ResetPlayer() {
      var playerControl = Game.Data.Player.GetComponent<PlayerControl>();
      playerControl.enabled = false;
      playerControl.IsAlive = true;
      Game.Data.Player.GetComponent<Animator>().SetFloat("Velocity", 0.0f);
    }

    private void ResetEmitter() {
      Game.Data.Bomb.GetComponent<RadioactivityEmitter>().enabled = false;
    }
  }
}
