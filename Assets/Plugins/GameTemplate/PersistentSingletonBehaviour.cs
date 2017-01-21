using System;
using UnityEngine;

namespace GameTemplate {

  public abstract class PersistentSingletonBehaviour<T>
    : SingletonBehaviour<T>
    where T : MonoBehaviour
  {

    protected override void Start() {
      base.Start();
      DontDestroyOnLoad(gameObject);
    }
  }
}
