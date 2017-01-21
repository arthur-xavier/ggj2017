using SDD.Events;
using UnityEngine;

namespace GameTemplate {

  public class BasicBehaviour<D, S> : MonoBehaviour {

    public Game<D, S> Game {
      get { return Game<D, S>.Instance; }
    }

    public EventManager Events {
      get { return EventManager.Instance; }
    }

    public override string ToString() {
      return string.Format("{0}, InstanceID {1}", GetType().ToString(), GetInstanceID());
    }
  }
}
