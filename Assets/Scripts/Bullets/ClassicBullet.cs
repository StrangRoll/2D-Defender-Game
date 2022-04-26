using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBullet : Bullet
{
    protected override void Hit(Collider2D collision, int damage)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
