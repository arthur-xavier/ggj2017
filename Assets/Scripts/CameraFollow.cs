using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class CameraFollow : SabotageBehaviour {

    [SerializeField]
    private float m_DistanceTreshold = 1.0f;

    [SerializeField]
    private float m_DistanceFromBorder = 2.0f;

    [SerializeField]
    private float m_MaxSpeed = 1.0f;

    [SerializeField]
    private float m_RotationSpeed = 1.0f;

    private Transform m_Player;
    private Transform m_Bomb;
    private Camera m_Camera;

    void Start() {
      m_Camera = GetComponent<Camera>();
      m_Player = Game.Data.Player;
      m_Bomb = Game.Data.Bomb;
    }

    void Update() {
      float distance = (transform.position - m_Player.position).y;
      float frustumHeight = 2.0f * distance * Mathf.Tan(m_Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
      float playerDistanceToTarget = (m_Player.position - m_Bomb.position).magnitude;

      FollowTarget(
        playerDistanceToTarget < frustumHeight * 0.5f- m_DistanceFromBorder
        ? m_Bomb
        : m_Player
        );
      LookAtTarget(m_Bomb);
    }

    private void FollowTarget(Transform target) {
      var position = new Vector3(transform.position.x, 0, transform.position.z);
      var targetPosition = new Vector3(target.position.x, 0, target.position.z);
      var distanceToTarget = (targetPosition - position).magnitude;
      var directionToTarget = (targetPosition - position).normalized;
      var forward = new Vector3(directionToTarget.x, 0, directionToTarget.z);
      if (distanceToTarget > m_DistanceTreshold) {
        transform.position += forward * Mathf.SmoothStep(0, m_MaxSpeed, distanceToTarget) * Time.deltaTime;
      }
    }

    private void LookAtTarget(Transform target) {
      var position = new Vector3(transform.position.x, 0, transform.position.z);
      var targetPosition = new Vector3(target.position.x, 0, target.position.z);
      var directionToTarget = (targetPosition - position).normalized;
      var newRotation = Quaternion.Euler(90, Mathf.Rad2Deg * Mathf.Atan2(directionToTarget.x, directionToTarget.z), 0);
      transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * m_RotationSpeed);
    }
  }
}
