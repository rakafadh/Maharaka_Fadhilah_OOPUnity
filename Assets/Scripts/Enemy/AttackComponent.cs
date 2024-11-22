using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = GetComponent<HitboxComponent>();
        if (collision.gameObject.tag == gameObject.tag)
        {
            return;
        }
        if (collision.CompareTag("Bullet"))
        {

            if (hitbox != null)
            {
                hitbox.Damage(collision.GetComponent<Bullet>()); 
            }
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            hitbox = GetComponent<HitboxComponent>();
            if (hitbox != null)
            {
                hitbox.Damage(damage);
            }
        }

    }
}