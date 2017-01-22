using System.Collections;
using UnityEngine;

namespace Sabotage {

  [RequireComponent(typeof(CharacterController))]
  public class PlayerControl : SabotageBehaviour {

    [SerializeField]
    private float m_Speed = 1.0f;

    [SerializeField]
    private float m_TurnSpeed = 5.0f;

    private InputManager InputManager {
      get { return InputManager.Instance; }
    }

    private bool m_IsAlive = true;
    public bool IsAlive {
      private get { return m_IsAlive; }
      set {
        m_IsAlive = value;
        m_Animator.SetBool("IsAlive", m_IsAlive);
      }
    }

    Animator m_Animator;
    Quaternion m_LookAt;
    CharacterController m_Controller;

    void OnEnable() {
      m_Animator = GetComponent<Animator>();
      m_Animator.Rebind();
      m_Animator.SetFloat("Velocity", 0.0f);

      m_Controller = GetComponent<CharacterController>();
      m_Controller.Move(Vector3.zero);

      IsAlive = true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
      if (hit.gameObject.CompareTag("Finish") && Game.State is PlayingState) {
        Game.State = new WinState();
      }
    }

    void Update() {
      transform.rotation = Quaternion.Slerp(transform.rotation, m_LookAt, m_TurnSpeed * m_Controller.velocity.magnitude * Time.deltaTime);
      m_Animator.SetFloat("Velocity", m_Controller.velocity.magnitude);

      if (!enabled)
        return;

      var targetPosition = Game.Data.Bomb.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = Game.Data.Camera.rotation * Vector3.up;
      var side = Game.Data.Camera.rotation * Vector3.right;

      m_Controller.Move((forward * InputManager.Axis.y + side * InputManager.Axis.x) * m_Speed * Time.deltaTime);

      if (m_Controller.velocity.magnitude > 0.1f) {
        m_LookAt = Quaternion.LookRotation(-m_Controller.velocity.normalized);
      }
    }
  }
}
