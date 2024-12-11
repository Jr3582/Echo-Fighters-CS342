using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1"))
        {
            Player1 player1 = collision.GetComponentInParent<Player1>();
            if (player1 != null)
            {
                player1.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Player2"))
        {
            Player2 player2 = collision.GetComponentInParent<Player2>();
            if (player2 != null)
            {
                player2.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
