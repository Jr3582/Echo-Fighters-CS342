using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    public GameObject bulletSprite;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public bool isFacingRight = true;
    public void ShootBullet() {
        GameObject bullet = Instantiate(bulletSprite, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = isFacingRight ? 1f : -1f;
            rb.velocity = new Vector2(direction * bulletSpeed, 0);
        }

        SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
        if (sr != null && !isFacingRight)
        {
            sr.flipX = true;
            bullet.transform.localScale = new Vector3(-1 * bullet.transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
        }
    }

}
