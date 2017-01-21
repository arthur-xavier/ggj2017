using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class PlayingState : SabotageState {

    private Transform m_Player {
      get { return Game.Data.Player; }
    }

    private Transform m_PlayerRespawn {
      get { return Game.Data.PlayerRespawn; }
    }

    public override IEnumerator OnStateEnter() {
      ResetPlayer();
      ResetBomb();
      InputManager.Instance.enabled = true;
      Time.timeScale = 1;
      Events.Raise(new StartEmissionEvent());
      yield return null;
    }

    public override IEnumerator OnStateExit() {
      Events.Raise(new StopEmissionEvent());
      yield return null;
    }

    private void ResetPlayer() {
      m_Player.position = m_PlayerRespawn.position;
      m_Player.GetComponent<Renderer>().material.color = Color.white;

      var rigidbody = m_Player.GetComponent<Rigidbody>();
      rigidbody.isKinematic = true;
      rigidbody.isKinematic = false;

      Game.Data.Player.GetComponent<PlayerControl>().enabled = true;
    }

    private void ResetBomb() {
      Game.Data.Bomb.GetComponent<RadioactivityEmitter>().enabled = true;
    }
  }
}
