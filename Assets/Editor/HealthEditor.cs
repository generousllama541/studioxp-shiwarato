using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Health health = (Health)target;

        health.maxHealth = EditorGUILayout.FloatField("Max Health", health.maxHealth);

        health.enableAdvancedOptions = EditorGUILayout.Toggle("Enable Advanced Options", health.enableAdvancedOptions);

        if (health.enableAdvancedOptions)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Advanced Settings", EditorStyles.boldLabel);

            health.enableCriticalHits = EditorGUILayout.Toggle("Enable Critical Hits", health.enableCriticalHits);
            health.criticalHitMultiplier = EditorGUILayout.FloatField("Critical Hit Multiplier", health.criticalHitMultiplier);
            health.enableStatusEffects = EditorGUILayout.Toggle("Enable Status Effects", health.enableStatusEffects);
            health.isPoisoned = EditorGUILayout.Toggle("Is Poisoned", health.isPoisoned);
            health.poisonDamagePerSecond = EditorGUILayout.FloatField("Poison Damage Per Second", health.poisonDamagePerSecond);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(health);
        }
    }
}
