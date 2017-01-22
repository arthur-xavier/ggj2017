using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class AudioManager : SingletonBehaviour<Sabotage, Settings, AudioManager> {

    [SerializeField]
    private AudioSource m_PlayerAudioSource;

    [SerializeField]
    private AudioClip m_StepsAudioClip;

    [SerializeField]
    private AudioClip m_ScreamAudioClip;

    [SerializeField]
    private AudioClip m_WetExplosionAudioClip;

    [SerializeField]
    private PlayerControl m_PlayerControl;

    [SerializeField]
    private CharacterController m_Controller;

    [SerializeField]
    private AudioSource m_EmitterAudioSource;

    [SerializeField]
    private AudioClip m_RadioactivityClip;

    [SerializeField]
    private AudioClip m_SmokeClip;

    void Update() {
      if (!m_PlayerControl.enabled)
        return;

      if (m_Controller.velocity.magnitude > 0.1f) {
        if (!m_PlayerAudioSource.isPlaying)
          PlaySteps();
      }
      else if (m_PlayerAudioSource.isPlaying) {
        StopPlayer();
      }
    }

    public void PlaySteps() {
      m_PlayerAudioSource.clip = m_StepsAudioClip;
      m_PlayerAudioSource.loop = true;
      m_PlayerAudioSource.volume = 1.0f;
      m_PlayerAudioSource.Play();
    }

    public void PlayScream() {
      m_PlayerAudioSource.clip = m_ScreamAudioClip;
      m_PlayerAudioSource.loop = false;
      m_PlayerAudioSource.volume = 0.35f;
      m_PlayerAudioSource.Play();
    }

    public void PlayWetExplosion() {
      m_PlayerAudioSource.clip = m_WetExplosionAudioClip;
      m_PlayerAudioSource.loop = false;
      m_PlayerAudioSource.volume = 0.9f;
      m_PlayerAudioSource.Play();
    }

    public void PlayRadioactivity() {
      m_EmitterAudioSource.clip = m_RadioactivityClip;
      m_EmitterAudioSource.loop = true;
      m_EmitterAudioSource.volume = 1.0f;
      m_EmitterAudioSource.Play();
    }

    public void PlaySmoke() {
      m_EmitterAudioSource.clip = m_SmokeClip;
      m_EmitterAudioSource.loop = false;
      m_EmitterAudioSource.volume = 0.8f;
      m_EmitterAudioSource.Play();
    }

    public void StopEmitter() {
      m_EmitterAudioSource.Stop();
    }

    public void StopPlayer() {
      m_PlayerAudioSource.Stop();
    }
  }
}
