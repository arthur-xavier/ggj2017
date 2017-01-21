using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class SabotageGame : Game<Settings> {

    protected override void LoadSettings() {
      Debug.Log("Loading settings");
      Settings = new Settings();
    }

    protected override void SaveSettings() {
      Debug.Log("Saving settings");
    }
  }
}
