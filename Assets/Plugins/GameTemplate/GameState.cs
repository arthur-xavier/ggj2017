using SDD.Events;
using System;
using System.Collections;
using UnityEngine;

namespace GameTemplate {

  [Serializable]
  public abstract class GameState<D, S> {

    public Game<D, S> Game {
      get { return Game<D, S>.Instance; }
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
