using System.Collections;
using UnityEngine;

namespace Sabotage {

  [RequireComponent(typeof(Rigidbody))]
  public class PlayerControl : SabotageBehaviour {

    [SerializeField]
    private float m_Speed = 1.0f;

    [SerializeField]
    private float m_TurnSpeed = 5.0f;

    Animator m_Animator;
    Quaternion m_LookAt;
    Rigidbody m_Rigidbody;

    void Start() {
      m_Rigidbody = GetComponent<Rigidbody>();
      m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

      m_Animator = GetComponent<Animator>();

      Events.AddListener<AxisInputEvent>(MovePlayer);
    }

    void OnCollisionEnter(Collision c) {
      if (c.gameObject.CompareTag("Finish") && Game.State is PlayingState) {
        Game.State = new WinState();
      }
    }

    void FixedUpdate() {
      transform.rotation = Quaternion.Slerp(transform.rotation, m_LookAt, m_TurnSpeed * m_Rigidbody.velocity.magnitude * Time.deltaTime);
      m_Animator.SetFloat("Velocity", m_Rigidbody.velocity.magnitude);
    }

    void MovePlayer(AxisInputEvent e) {
      if (!enabled)
        return;

      var targetPosition = Game.Data.Bomb.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = Game.Data.Camera.rotation * Vector3.up;
      var side = Game.Data.Camera.rotation * Vector3.right;

      m_Rigidbody.AddForce((forward * e.Axis.y + side * e.Axis.x) * m_Speed * Time.deltaTime);

      if (m_Rigidbody.velocity.magnitude > 0.1f) {
        m_LookAt = Quaternion.LookRotation(-m_Rigidbody.velocity.normalized);
      }
    }
  }
}
