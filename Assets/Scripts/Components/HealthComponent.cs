using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    protected Transform respawnPoint;
    protected Vector3 m_startLocation;

    [SerializeField]
    protected float m_maxHealth = 100;
    [SerializeField]
    protected float m_healthRegen = 1;
    [SerializeField]
    protected float m_damageImmuneDuration = 0.5f;
    [SerializeField]
    protected float m_regenDelay = 1f;
    [SerializeField]
    protected float m_regenTimer = 0;

    protected bool m_isDead = false;
    public bool isDead { get { return m_isDead; } }
    protected float m_currentHealth;
    protected float m_immuneTimer;

    public float Health { get { return m_currentHealth; } }
    
    private void Awake()
    {
        m_currentHealth = m_maxHealth;
        m_startLocation = transform.position;
    }

    protected virtual void SetUndamageable(float duration = Mathf.Infinity)
    {
        m_immuneTimer = duration;
    }

    protected virtual void SetDamageable()
    {
        m_immuneTimer = -1;
    }

    protected virtual void Die()
    {
        m_isDead = true;
    }

    public virtual void TakeDamage(float damage)
    {
        if (m_immuneTimer <= 0)
        {
            m_regenTimer = m_regenDelay;
            m_immuneTimer = m_damageImmuneDuration;
            m_currentHealth -= damage;


            if (m_currentHealth <= 0) Die();
        }
    }

    public virtual IEnumerator TakeTickDamage(float tickAmount, float interval, float Damage)
    {
        for (int i = 0; i < tickAmount; i++)
        {
            yield return new WaitForSeconds(interval);
            TakeDamage(Damage);
        }
    }

    public virtual void ApplyStatusEffect(params StatusEffect[] effects)
    {

    }

    public virtual void Heal(float amount)
    {
        if (!m_isDead)
        {
            m_currentHealth += amount;
            m_currentHealth = Mathf.Min(m_currentHealth, m_maxHealth);
        }
    }

    public virtual void AddHealth(float amount)
    {
        m_currentHealth += amount;
        m_isDead = false;
    }

    public virtual void Update()
    {
        if (m_immuneTimer > 0)
            m_immuneTimer -= Time.deltaTime;

        if (m_regenTimer > 0)
            m_regenTimer -= Time.deltaTime;
        else m_currentHealth += m_healthRegen * Time.deltaTime;
    }

    public virtual void RespawnCharacter()
    {
        if (respawnPoint) transform.position = respawnPoint.position;
        else transform.position = m_startLocation;
    }
}
