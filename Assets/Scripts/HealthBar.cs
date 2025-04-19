using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health targetHealth;
    public Image fillImage;
    public Vector3 offset = new Vector3(0, 1f, 0);

    void Update()
    {
        if (targetHealth != null)
        {
            float fillAmount = targetHealth.CurrentHealth / targetHealth.maxHealth;
            fillImage.fillAmount = fillAmount;

            // Make the health bar follow the target
            transform.position = Camera.main.WorldToScreenPoint(targetHealth.transform.position + offset);
        }
    }
}
