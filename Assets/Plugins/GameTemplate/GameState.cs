using SDD.Events;
using System;
using System.Collections;
using UnityEngine;

namespace GameTemplate {

  [Serializable]
  public abstract class GameState<S> {

    public Game<S> Game {
      get { return Game<S>.Instance; }
    }

    public EventManager Events {
      get { return EventManager.Instance; }
    }

    public virtual IEnumerator OnStateEnter() {
      yield return null;
    }

    public virtual IEnumerator OnStateExit() {
      yield return null;
    }
  }
}
