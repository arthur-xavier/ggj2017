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

      Events.AddListener<WaveHitEvent>(PlayerHit);
    }

    void FixedUpdate() {
      var targetPosition = Game.Data.Bomb.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = Game.Data.Camera.rotation * Vector3.up;
      var side = Game.Data.Camera.rotation * Vector3.right;

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
