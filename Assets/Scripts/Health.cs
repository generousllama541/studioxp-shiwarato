using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float CurrentHealth => currentHealth;

    // Flags to control additional effects
    public bool enableStatusEffects = false;  // Toggle to enable status effects
    public bool enableCriticalHits = false;   // Toggle to enable critical hits
    public float criticalHitMultiplier = 2f;  // Critical hit damage multiplier
    public bool isPoisoned = false;           // Example status effect: Poison
    public float poisonDamagePerSecond = 5f;  // Poison damage rate
    private Coroutine poisonCoroutine;

    // Event to notify when health changes
    public delegate void HealthChanged(float currentHealth);
    public event HealthChanged OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Method to take damage, can include critical hit or status effects
    public void TakeDamage(float amount, bool isCriticalHit = false)
    {
        // If critical hits are enabled, apply the critical hit multiplier
        if (enableCriticalHits && isCriticalHit)
        {
            amount *= criticalHitMultiplier;
            Debug.Log("Critical Hit!");
        }

        // Apply the damage
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        // Notify about the health change
        OnHealthChanged?.Invoke(currentHealth);

        // Check if the character died
        if (currentHealth == 0)
        {
            Die();
        }

        // If status effects are enabled, handle them
        if (enableStatusEffects && isPoisoned)
        {
            if (poisonCoroutine == null)
            {
                poisonCoroutine = StartCoroutine(ApplyPoison());
            }
        }
    }

    // Method to heal
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        // Notify about the health change
        OnHealthChanged?.Invoke(currentHealth);
    }

    // Method called when the object dies
    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        // Implement death logic here (e.g., disable game object, play death animation, etc.)
        Destroy(gameObject);  // Example: Destroy the object
    }

    // Poison effect: applies damage over time
    private IEnumerator ApplyPoison()
    {
        while (isPoisoned && currentHealth > 0)
        {
            TakeDamage(poisonDamagePerSecond * Time.deltaTime);  // Poison applies over time
            yield return null;
        }
        poisonCoroutine = null;
    }

    // Method to apply poison status effect
    public void ApplyPoisonEffect()
    {
        if (enableStatusEffects)
        {
            isPoisoned = true;
            Debug.Log(gameObject.name + " is poisoned!");
        }
    }

    // Method to remove poison status effect
    public void RemovePoison()
    {
        isPoisoned = false;
        if (poisonCoroutine != null)
        {
            StopCoroutine(poisonCoroutine);
            poisonCoroutine = null;
        }
        Debug.Log(gameObject.name + " is no longer poisoned.");
    }
}
