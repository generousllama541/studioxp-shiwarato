using UnityEngine;

public class ClickDamageTester : MonoBehaviour
{
    public float testDamage = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Damage"))
            {
                GameObject target = hit.collider.gameObject;

                // Find the player (attacker)
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    Attack attack = player.GetComponent<Attack>();
                    if (attack != null)
                    {
                        // Override damage for testing
                        float originalDamage = attack.damage;
                        attack.damage = testDamage;

                        attack.AttackTarget(target);

                        // Restore damage value
                        attack.damage = originalDamage;
                    }
                }
            }
        }
    }
}

