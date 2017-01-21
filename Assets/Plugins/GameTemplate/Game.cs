using System.Collections;
using UnityEngine;

namespace GameTemplate {

  public abstract class Game<D, S> : PersistentSingletonBehaviour<D, S, Game<D, S>> {

    [SerializeField]
    [HideInInspector]
    private GameState<D, S> m_State;
    public GameState<D, S> State {
      get { return m_State; }
      set {
        StartCoroutine(ChangeState(value));
      }
    }

    public S Settings { get; protected set; }

    [SerializeField]
    private D m_Data;
    public D Data {
      get { return m_Data; }
    }

    protected override void Start() {
      base.Start();
      LoadSettings();
    }

    protected abstract void LoadSettings();
    protected abstract void SaveSettings();

    private IEnumerator ChangeState(GameState<D, S> state) {
      if (m_State != null) {
        var oldState = m_State;
        m_State = null;
        yield return StartCoroutine(oldState.OnStateExit());
      }
      m_State = state;
      yield return StartCoroutine(state.OnStateEnter());
    }
  }
}
