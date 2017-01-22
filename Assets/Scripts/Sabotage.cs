using GameTemplate;
using System;
using UnityEngine;

namespace Sabotage {

  [Serializable]
  public class Sabotage {

    public Transform Player;

    public Transform PlayerRespawn;

    public Transform Bomb;

    public Transform Camera;

    [HideInInspector]
    public bool IsPaused;
  }
}
