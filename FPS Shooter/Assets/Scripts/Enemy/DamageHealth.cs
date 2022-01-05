using UnityEngine;

public class DamageHealth : MonoBehaviour
{
    public float healthCount;

    public void ApplyDamage(float damage)
    {
        healthCount -= damage;
        if (healthCount <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject, 2);
    }
}
