using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class PlayingGameState : SabotageGameState {

    public override IEnumerator OnStateEnter() {
      Events.Raise(new StartEmissionEvent());
      yield return null;
    }

    public override IEnumerator OnStateExit() {
      Events.Raise(new StopEmissionEvent());
      yield return null;
    }
  }
}
