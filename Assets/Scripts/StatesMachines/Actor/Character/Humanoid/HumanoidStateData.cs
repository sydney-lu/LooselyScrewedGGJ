using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IKController))]
public class HumanoidStateData : CharacterStateData
{
    #region Components
    protected IKController m_IK;
    public IKController IK
    {
        get { return m_IK; }
    }
    #endregion

    #region Main
    protected override void Awake()
    {
        base.Awake();
        m_IK = GetComponent<IKController>();
    }
    #endregion
}