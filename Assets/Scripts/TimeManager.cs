using GameTemplate;
using System.Collections;
using UnityEngine;

namespace Sabotage {

  public class TimeManager : SingletonBehaviour<Sabotage, Settings, TimeManager> {

    [SerializeField]
    private float m_MaxTime;

    IEnumerator m_CountCoroutine;
    float m_Time;

    public void Reset() {
      if (m_CountCoroutine != null)
        StopCoroutine(m_CountCoroutine);

      m_Time = m_MaxTime;
      m_CountCoroutine = Count();
      StartCoroutine(m_CountCoroutine);
    }

    public void Stop() {
      StopCoroutine(m_CountCoroutine);
    }

    private IEnumerator Count() {
      while (m_Time > 0) {
        yield return new WaitForSeconds(1.0f);
        m_Time -= 1.0f;
      }

      Game.State = new GameOverState();
    }
  }
}
