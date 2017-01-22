using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class SabotageGame : Game<Sabotage, Settings> {

    protected override void Start() {
      base.Start();
      State = new PlayingState();

      Events.AddListener<PauseEvent>(Pause);
      Events.AddListener<ResumeEvent>(Resume);
    }

    protected override void LoadSettings() {
      Debug.Log("Loading settings");
      Settings = new Settings();
    }

    protected override void SaveSettings() {
      Debug.Log("Saving settings");
    }

    private void Pause(PauseEvent e) {
      Data.IsPaused = true;
      Time.timeScale = 0;
    }

    private void Resume(ResumeEvent e) {
      Data.IsPaused = false;
      Time.timeScale = 1;
    }
  }
}
