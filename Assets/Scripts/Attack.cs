using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;
    public bool enableCriticalHits = false;  // Flag to enable critical hits
    public float criticalHitChance = 0.2f;   // Chance for a critical hit (20%)
    public bool applyPoison = false;         // Flag to apply poison when attacking

    // Method to attack a target
    public void AttackTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target is null.");
            return;
        }

        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            bool isCriticalHit = enableCriticalHits && Random.value < criticalHitChance;

            // Apply the damage with or without critical hit
            targetHealth.TakeDamage(damage, isCriticalHit);

            // Optionally apply poison
            if (applyPoison)
            {
                targetHealth.ApplyPoisonEffect();
            }

            Debug.Log("Attacked " + target.name + " for " + (isCriticalHit ? damage * 2 : damage) + " damage.");
        }
        else
        {
            Debug.Log("Target does not have a Health component.");
        }
    }
}
