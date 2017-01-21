using System.Collections;
using UnityEngine;

namespace Sabotage {

  [RequireComponent(typeof(Rigidbody))]
  public class PlayerControl : SabotageBehaviour {

    [SerializeField]
    private float m_Speed = 1.0f;

    Rigidbody m_Rigidbody;

    void Start() {
      m_Rigidbody = GetComponent<Rigidbody>();
      m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

      Events.AddListener<AxisInputEvent>(MovePlayer);
    }

    void OnCollisionEnter(Collision c) {
      if (c.gameObject.CompareTag("Finish")) {
        Game.State = new WinState();
      }
    }

    void MovePlayer(AxisInputEvent e) {
      if (!enabled)
        return;

      var targetPosition = Game.Data.Bomb.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = Game.Data.Camera.rotation * Vector3.up;
      var side = Game.Data.Camera.rotation * Vector3.right;

      m_Rigidbody.AddForce((forward * e.Axis.y + side * e.Axis.x) * m_Speed);
    }
  }
}
