using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class GameOverState : SabotageState {

    public override IEnumerator OnStateEnter() {
      ResetPlayer();
      AudioManager.Instance.PlayScream();
      yield return new WaitForSecondsRealtime(1.0f);
      Game.State = new PlayingState();
    }

    public override IEnumerator OnStateExit() {
      PlayerManager.Instance.SpawnCorpse();
      yield return null;
    }

    private void ResetPlayer() {
      var playerControl = Game.Data.Player.GetComponent<PlayerControl>();
      playerControl.enabled = false;
      playerControl.IsAlive = false;
    }
  }
}
