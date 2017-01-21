using SDD.Events;
using System;
using UnityEngine;

namespace GameTemplate {

  public abstract class SingletonBehaviour<D, S, T>
    : MonoBehaviour
    where T : MonoBehaviour
  {

    public Game<D, S> Game {
      get { return Game<D, S>.Instance; }
    }

    public EventManager Events {
      get { return EventManager.Instance; }
    }

    private static T s_Instance;
    public static T Instance {
      get { return !s_ApplicationIsQuitting ? s_Instance : null; }
    }

    private static bool s_ApplicationIsQuitting = false;

    protected virtual void Start() {
      if (s_Instance == null) {
        s_Instance = this as T;
      }
      else {
        Destroy(gameObject);
      }
    }

    public void OnDestroy () {
      s_ApplicationIsQuitting = true;
    }

    public void OnApplicationQuit() {
      s_ApplicationIsQuitting = true;
    }

    public override string ToString() {
      return string.Format("{0} (Singleton),  InstanceID={1}", GetType().ToString(), GetInstanceID());
    }
  }
}
