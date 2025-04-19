using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
   [SerializeField] public GameObject healthBarPrefab;
    private HealthBar healthBarInstance;


    public float CurrentHealth => currentHealth;

    [Header("Advanced Options Toggle")]
    public bool enableAdvancedOptions = false;

    [Header("Advanced Settings")]
    public bool enableStatusEffects = false;
    public bool enableCriticalHits = false;
    public float criticalHitMultiplier = 2f;
    public bool isPoisoned = false;
    public float poisonDamagePerSecond = 5f;

    private Coroutine poisonCoroutine;

    public delegate void HealthChanged(float currentHealth);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        {
            GameObject bar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
            healthBarInstance = bar.GetComponent<HealthBar>();
            healthBarInstance.targetHealth = this;
        }
    }

    public void TakeDamage(float amount, bool isCriticalHit = false)
    {
        if (enableCriticalHits && isCriticalHit)
        {
            amount *= criticalHitMultiplier;
            Debug.Log("Critical Hit!");
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }

        if (enableStatusEffects && isPoisoned && poisonCoroutine == null)
        {
            poisonCoroutine = StartCoroutine(ApplyPoison());
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }

    private IEnumerator ApplyPoison()
    {
        while (isPoisoned && currentHealth > 0)
        {
            TakeDamage(poisonDamagePerSecond * Time.deltaTime);
            yield return null;
        }
        poisonCoroutine = null;
    }

    public void ApplyPoisonEffect()
    {
        if (enableStatusEffects)
        {
            isPoisoned = true;
            Debug.Log($"{gameObject.name} is poisoned!");
        }
    }

    public void RemovePoison()
    {
        isPoisoned = false;
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
            poisonCoroutine = null;
        }
        Debug.Log($"{gameObject.name} is no longer poisoned.");
    }

    // Expose for editor
    public bool EnableStatusEffects => enableStatusEffects;
    public bool EnableCriticalHits => enableCriticalHits;
    public bool IsPoisoned => isPoisoned;
    public float PoisonDamagePerSecond => poisonDamagePerSecond;
    public float CriticalHitMultiplier => criticalHitMultiplier;
}
