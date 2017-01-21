using System;
using UnityEngine;

namespace GameTemplate {

  public abstract class PersistentSingletonBehaviour<D, S, T>
    : SingletonBehaviour<D, S, T>
    where T : MonoBehaviour
  {

    protected override void Start() {
      base.Start();
      DontDestroyOnLoad(gameObject);
    }
  }
}
