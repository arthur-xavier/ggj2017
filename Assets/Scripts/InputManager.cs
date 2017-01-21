using GameTemplate;
using UnityEngine;

namespace Sabotage {

  public class AxisInputEvent : SDD.Events.Event {

    public Vector2 Axis;

    public AxisInputEvent(Vector2 axis) {
      Axis = axis;
    }
  }

  public class SkipEvent : SDD.Events.Event {}

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

      if (Input.GetButton("Jump") || Input.GetButton("Cancel")) {
        Events.Raise(new SkipEvent());
      }
    }
  }
}
