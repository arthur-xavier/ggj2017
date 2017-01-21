using SDD.Events;
using UnityEngine;

namespace GameTemplate {

  public class BasicBehaviour<S> : MonoBehaviour {

    public Game<S> Game {
      get { return Game<S>.Instance; }
    }

    public EventManager Events {
      get { return EventManager.Instance; }
    }

    public override string ToString() {
      return string.Format("{0}, InstanceID {1}", GetType().ToString(), GetInstanceID());
    }
  }
}
