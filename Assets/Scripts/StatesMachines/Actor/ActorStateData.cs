using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(AudioSource))]
public class ActorStateData : Actor
{
    #region Components
    protected StateManager m_stateM;
    public StateManager StateM
    {
        get { return m_stateM; }
    }
    #endregion

    #region Pause Info
    private bool m_paused;

    public bool isPaused
    {
        get { return m_paused; }
    }
    public virtual void PauseController()
    {
        StateM.IsPaused = true;
        m_paused = true;
    }
    public virtual void UnPauseController()
    {
        StateM.IsPaused = false;
        m_paused = false;
    }
    #endregion

    #region Main
    protected override void Awake()
    {
        m_stateM = GetComponentInChildren<StateManager>();
    }
    #endregion
}
