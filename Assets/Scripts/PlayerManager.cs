using GameTemplate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sabotage {

  public class PlayerManager : SingletonBehaviour<Sabotage, Settings, PlayerManager> {

    [SerializeField]
    private GameObject m_PlayerCorpsePrefab;

    [SerializeField]
    private int m_MaxCorpseCount = 5;

    Queue<Transform> m_Corpses = new Queue<Transform>();
    Transform m_Player {
      get { return Game.Data.Player; }
    }

    public void SpawnCorpse() {
      var rotation = Quaternion.LookRotation(Vector3.up);
      var eulerAngles = rotation.eulerAngles;
      eulerAngles.y = m_Player.transform.rotation.eulerAngles.y - 30.0f;
      rotation.eulerAngles = eulerAngles;

      m_Corpses.Enqueue(
        Instantiate(
          m_PlayerCorpsePrefab,
          m_Player.transform.position,
          rotation
          )
          .transform
        );
      if (m_Corpses.Count > m_MaxCorpseCount)
        Destroy(m_Corpses.Dequeue().gameObject);
    }
  }
}
