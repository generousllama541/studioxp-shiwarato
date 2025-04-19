using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f;

    [Header("Advanced Options Toggle")]
    public bool enableAdvancedOptions = false;

    [Header("Advanced Settings")]
    public bool enableCriticalHits = false;
    public float criticalHitChance = 0.2f;
    public bool applyPoison = false;

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
            bool isCritical = enableCriticalHits && Random.value < criticalHitChance;

            targetHealth.TakeDamage(damage, isCritical);

            if (applyPoison)
            {
                targetHealth.ApplyPoisonEffect();
            }

            Debug.Log($"Attacked {target.name} for {(isCritical ? damage * 2 : damage)} damage.");
        }
        else
        {
            Debug.Log("Target does not have a Health component.");
        }
    }
}
