using UnityEngine;

[ CreateAssetMenu(menuName = "Card") ]
public class Card : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;
    
    public int cost;
    public int damage;
    public bool canChooseTarget;
    public GameObject useEffect;

    public void Use(Entity caster, Entity target = null)
    {
        caster.energy -= cost;

        if (target != null)
        {
            target.Damage(damage);
            if(useEffect)Instantiate( useEffect, target.transform.position, Quaternion.identity);
        }
    }
}
