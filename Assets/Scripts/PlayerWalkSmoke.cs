using System.Collections;
using UnityEngine;

namespace Sabotage {

  [RequireComponent(typeof(PlayerControl))]
  [RequireComponent(typeof(CharacterController))]
  public class PlayerWalkSmoke : SabotageBehaviour {

    [SerializeField]
    private ParticleSystem m_Particles;

    PlayerControl m_PlayerControl;
    CharacterController m_Controller;

    void Start() {
      m_Controller = GetComponent<CharacterController>();
      m_PlayerControl = GetComponent<PlayerControl>();
    }

    void Update() {
      if (m_Controller.velocity.magnitude > 0.1f && !m_Particles.isPlaying) {
        m_Particles.Play();
      }
      else if (m_Controller.velocity.magnitude <= 0.1f && m_Particles.isPlaying) {
        m_Particles.Stop();
      }

      if (!m_PlayerControl.enabled && m_Particles.isPlaying) {
        m_Particles.Stop();
      }
    }
  }
}
