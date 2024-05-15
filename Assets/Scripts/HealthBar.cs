using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Entity entity;
    public Transform ProgressTransform;


    private void Update()
    {
        if (entity == null)
        {
            Destroy(gameObject);
            return;
        }

        var t = entity.health / (float)entity.maxHealth;

        ProgressTransform.localScale = new Vector3(t, 1, 1);
    }
}
