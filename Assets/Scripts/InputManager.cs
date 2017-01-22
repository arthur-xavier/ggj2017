using GameTemplate;
using UnityEngine;

namespace Sabotage {

  public class AxisInputEvent : SDD.Events.Event {

    public Vector2 Axis;

    public AxisInputEvent(Vector2 axis) {
      Axis = axis;
    }
  }

  public class PauseEvent : SDD.Events.Event {}
  public class ResumeEvent : SDD.Events.Event {}

  public class InputManager : SingletonBehaviour<Sabotage, Settings, InputManager> {

    void Update() {
      if (Input.GetAxisRaw("Horizontal") != 0
        || Input.GetAxisRaw("Vertical") != 0)
      {
        Events.Raise(
          new AxisInputEvent(
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            )
          );
      }

      if (Input.GetButtonDown("Submit")) {
        if (!Game.Data.IsPaused) {
          Events.Raise(new PauseEvent());
        }
        else {
          Events.Raise(new ResumeEvent());
        }
      }
    }
  }
}
