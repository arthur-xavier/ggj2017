using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class WaveHitEvent : SDD.Events.Event {}

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

    private Queue<float> m_Waves = new Queue<float>();
    private IEnumerator m_EmissionCoroutine;
    private WaitForSeconds m_WavePeriodCoroutine;
    private bool m_PlayerIsHit = false;

    void Start() {
      Events.AddListener<StartEmissionEvent>(StartEmission);
      Events.AddListener<StopEmissionEvent>(StopEmission);
    }

    void Update() {
      bool playerHit = false;

      foreach (float t in m_Waves) {
        Vector3 playerDistance = Game.Data.Player.position - transform.position;
        Vector3 playerDirection = playerDistance.normalized;
        Vector3 waveDistance = playerDirection * (Time.time - t) * m_WaveSpeed;

        RaycastHit hitInfo;
        if (Mathf.Abs(waveDistance.magnitude - playerDistance.magnitude) < m_WaveSize
          && Physics.Raycast(Game.Data.Player.position - playerDirection * m_WaveDispersionLength, playerDirection, out hitInfo, m_WaveDispersionLength)
          && hitInfo.collider.CompareTag("Player"))
        {
          playerHit = true;
        }
      }

      if (!m_PlayerIsHit && playerHit) {
        Events.Raise(new WaveHitEvent());
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
