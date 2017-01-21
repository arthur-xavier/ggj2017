using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class LightFollow : SabotageBehaviour {

    [SerializeField]
    private float m_DistanceTreshold = 1.0f;

    [SerializeField]
    private float m_MaxSpeed = 1.0f;

    void FixedUpdate() {
      FollowTarget(Game.Data.Player);
    }

    private void FollowTarget(Transform target) {
      var position = new Vector3(transform.position.x, 0, transform.position.z);
      var targetPosition = new Vector3(target.position.x, 0, target.position.z);
      var distanceToTarget = (targetPosition - position).magnitude;
      var directionToTarget = (targetPosition - position).normalized;
      var forward = new Vector3(directionToTarget.x, 0, directionToTarget.z);

      transform.position +=
        forward
        * Mathf.Max(0, distanceToTarget - m_DistanceTreshold)
        * m_MaxSpeed
        * Time.deltaTime;
    }
  }
}
