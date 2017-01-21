using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class WinState : SabotageState {

    public override IEnumerator OnStateEnter() {
      Game.Data.Bomb.GetComponent<RadioactivityEmitter>().enabled = false;
      yield return new WaitForSecondsRealtime(2.0f);
      Game.State = new PlayingState();
    }
  }
}
