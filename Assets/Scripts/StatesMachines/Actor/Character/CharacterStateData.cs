using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StaminaComponent))]
[RequireComponent(typeof(HealthComponent))]
public class CharacterStateData : ActorStateData
{
    #region Components
    protected Animator m_anim;
    public Animator Anim
    {
        get { return m_anim; }
    }
    protected Rigidbody m_rb;
    public Rigidbody RB
    {
        get { return m_rb; }
    }
    protected HealthComponent m_hc;
    public HealthComponent Health
    {
        get { return m_hc; }
    }
    protected StaminaComponent m_sc;
    public StaminaComponent Stamina
    {
        get { return m_sc; }
    }
    #endregion

    #region Main
    public override void PauseController()
    {
        m_rb.velocity = Vector3.zero;
        base.PauseController();
    }

    protected override void Awake()
    {
        base.Awake();
        m_rb = GetComponentInChildren<Rigidbody>();
        m_anim = GetComponentInChildren<Animator>();
        m_hc = GetComponentInChildren<HealthComponent>();
        m_sc = GetComponentInChildren<StaminaComponent>();
    }
    #endregion
}
