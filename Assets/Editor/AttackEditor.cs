using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attack))]
public class AttackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Attack attack = (Attack)target;

        attack.damage = EditorGUILayout.FloatField("Damage", attack.damage);

        attack.enableAdvancedOptions = EditorGUILayout.Toggle("Enable Advanced Options", attack.enableAdvancedOptions);

        if (attack.enableAdvancedOptions)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Advanced Settings", EditorStyles.boldLabel);

            attack.enableCriticalHits = EditorGUILayout.Toggle("Enable Critical Hits", attack.enableCriticalHits);
            attack.criticalHitChance = EditorGUILayout.Slider("Critical Hit Chance", attack.criticalHitChance, 0f, 1f);
            attack.applyPoison = EditorGUILayout.Toggle("Apply Poison", attack.applyPoison);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(attack);
        }
    }
}
