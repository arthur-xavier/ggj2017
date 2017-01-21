using System.Collections;
using UnityEngine;

namespace Sabotage {

  [RequireComponent(typeof(Rigidbody))]
  public class PlayerControl : SabotageBehaviour {

    [SerializeField]
    private float m_Speed = 1.0f;

    Transform m_Target;
    Transform m_Camera;
    Rigidbody m_Rigidbody;

    void Start() {
      m_Rigidbody = GetComponent<Rigidbody>();
      m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

      m_Target = Game.Data.Bomb;
      m_Camera = Game.Data.Camera;

      Events.AddListener<WaveHitEvent>(PlayerHit);
    }

    void FixedUpdate() {
      var targetPosition = m_Target.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = m_Camera.rotation * Vector3.up;
      var side = m_Camera.rotation * Vector3.right;

      if (Input.GetAxisRaw("Horizontal") > 0) {
        m_Rigidbody.AddForce(side * m_Speed);
      }
      else if (Input.GetAxisRaw("Horizontal") < 0) {
        m_Rigidbody.AddForce(-side * m_Speed);
      }

      if (Input.GetAxisRaw("Vertical") > 0) {
        m_Rigidbody.AddForce(forward * m_Speed);
      }
      else if (Input.GetAxisRaw("Vertical") < 0) {
        m_Rigidbody.AddForce(-forward * m_Speed);
      }
    }

    void PlayerHit(WaveHitEvent e) {
      GetComponent<Renderer>().material.color = Color.red;
      StartCoroutine(HealPlayer());
    }

    IEnumerator HealPlayer() {
      yield return new WaitForSeconds(0.5f);
      GetComponent<Renderer>().material.color = Color.yellow;
    }
  }
}
