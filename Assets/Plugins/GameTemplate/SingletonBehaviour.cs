using System;
using UnityEngine;

namespace GameTemplate {

  public abstract class SingletonBehaviour<T>
    : MonoBehaviour
    where T : MonoBehaviour
  {

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
