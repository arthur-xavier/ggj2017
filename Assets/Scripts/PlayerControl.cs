using GameTemplate;
using UnityEngine;

namespace Sabotage {

  public class PlayerControl : BasicBehaviour<Settings> {

    [SerializeField]
    private Transform m_Target;

    [SerializeField]
    private Transform m_Camera;

    [SerializeField]
    private float m_Speed = 1.0f;

    [SerializeField]
    private bool m_Radial = true;

    void Update() {
      var targetPosition = m_Target.position;
      var direction = (targetPosition - transform.position).normalized;

      var forward = m_Radial
        // ? new Vector3(direction.x, 0, direction.z)
        ? m_Camera.rotation * Vector3.up
        : Vector3.forward;
      var side = m_Radial
        // ? Vector3.Cross(forward, Vector3.up)
        ? m_Camera.rotation * Vector3.right
        : Vector3.right;

      if (Input.GetAxisRaw("Horizontal") > 0) {
        transform.position += side * m_Speed * Time.deltaTime;
      }
      else if (Input.GetAxisRaw("Horizontal") < 0) {
        transform.position -= side * m_Speed * Time.deltaTime;
      }

      if (Input.GetAxisRaw("Vertical") > 0) {
        transform.position += forward * m_Speed * Time.deltaTime;
      }
      else if (Input.GetAxisRaw("Vertical") < 0) {
        transform.position -= forward * m_Speed * Time.deltaTime;
      }
    }
  }
}
