using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Transform hitDetector;
    public float attackRange = 1.5f;    // Range of attack

    public void Attack(int damage)
    {
        // Get all enemies in attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitDetector.position, attackRange, enemyLayers);

        // Deal damage to all enemies in the hit radius
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<ICharacterController>().Damage(damage);
        }
    }

    // Draw a sphere representing hit radius in the scene window
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hitDetector.position, attackRange);
    }
}
