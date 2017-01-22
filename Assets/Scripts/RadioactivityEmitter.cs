using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class RadioactivityEmitter : SabotageBehaviour {

    [SerializeField]
    private float m_WavePeriod = 2.0f;

    [SerializeField]
    private float m_WaveSpeed = 2.0f;

    [SerializeField]
    private float m_WaveLifetime = 4.0f;

    [SerializeField]
    private float m_WaveSize = 1.0f;

    [SerializeField]
    private float m_WaveDispersionLength = 5.0f;

    [SerializeField]
    private Transform m_WavesPlane;

    [SerializeField]
    private ParticleSystem m_ShockParticles;

    private Queue<float> m_Waves = new Queue<float>();
    private IEnumerator m_EmissionCoroutine;
    private WaitForSeconds m_WavePeriodCoroutine;
    private bool m_PlayerIsHit = false;
    private Renderer m_WavesRenderer;

    void Start() {
      Events.AddListener<StartEmissionEvent>(StartEmission);
      Events.AddListener<StopEmissionEvent>(StopEmission);

      m_WavesRenderer = m_WavesPlane.GetComponent<Renderer>();
      m_WavesRenderer.material.SetFloat("_WaveSpeed", m_WaveSpeed);
    }

    void OnEnable() {
      m_ShockParticles.Play();
    }

    void OnDisable() {
      m_ShockParticles.Stop();
    }

    void FixedUpdate() {
      bool playerHit = false;
      int wave = 1;

      Vector3 playerPosition = Game.Data.Player.position;
      Vector3 emitterPosition = transform.position;
      emitterPosition.y = playerPosition.y;

      Vector3 playerDistance = playerPosition - emitterPosition;
      Vector3 playerDirection = playerDistance.normalized;

      foreach (float t in m_Waves) {
        Vector3 waveDistance = playerDirection * (Time.time - t) * m_WaveSpeed;
        Vector3 rayOrigin = playerPosition - playerDirection * m_WaveDispersionLength + Vector3.up;

        RaycastHit hitInfo;
        if (Mathf.Abs(waveDistance.magnitude - playerDistance.magnitude) <= m_WaveSize
          && Physics.Raycast(rayOrigin, playerDirection, out hitInfo, m_WaveDispersionLength)
          && hitInfo.collider.CompareTag("Player"))
        {
          playerHit = true;
        }

        if (wave <= 3) {
          m_WavesRenderer.material.SetFloat("_WaveDistance" + wave, (Time.time - t) * m_WaveSpeed);
          wave++;
        }
      }

      if (!m_PlayerIsHit && playerHit) {
        Game.State = new GameOverState();
      }

      m_PlayerIsHit = playerHit;
    }

    private void StartEmission(StartEmissionEvent e) {
      m_EmissionCoroutine = Emission();
      StartCoroutine(m_EmissionCoroutine);
    }

    private IEnumerator Emission() {
      while (true) {
        if (m_Waves.Count > 0 && Time.time >= m_Waves.Peek() + m_WaveLifetime) {
          m_Waves.Dequeue();
        }

        if (m_WavePeriodCoroutine == null) {
          m_WavePeriodCoroutine = new WaitForSeconds(m_WavePeriod);
        }

        yield return m_WavePeriodCoroutine;

        m_Waves.Enqueue(Time.time);
      }
    }

    private void StopEmission(StopEmissionEvent e) {
      StopCoroutine(m_EmissionCoroutine);
    }
  }
}
