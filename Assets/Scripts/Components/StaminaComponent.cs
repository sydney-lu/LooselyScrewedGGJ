using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaminaComponent : MonoBehaviour
{
    [SerializeField]
    protected float m_maxStamina = 100;
    [SerializeField]
    protected float m_staminaRegen = 1;
    [SerializeField]
    protected float m_regenDelayOnUse = 1f;
    [SerializeField]
    protected float m_regenDelayOnEmpty = 3f;

    protected bool m_isExhausted = false;
    protected float m_currentStamina;
    protected float m_exhaustTimer;

    public float Stamina { get { return m_currentStamina; } }

    public virtual void UseStamina(float amount)
    {
        if (!m_isExhausted)
        {
            m_exhaustTimer = m_regenDelayOnUse;
            m_currentStamina -= amount;

            if (m_currentStamina <= 0)
            {
                m_exhaustTimer = m_regenDelayOnEmpty;
                m_isExhausted = true;
            }
        }
    }

    private void Awake()
    {
        m_currentStamina = m_maxStamina;
    }

    public virtual void Update()
    {
        if (m_exhaustTimer > 0)
            m_exhaustTimer -= Time.deltaTime;
        else m_currentStamina += m_staminaRegen * Time.deltaTime;
    }
}
