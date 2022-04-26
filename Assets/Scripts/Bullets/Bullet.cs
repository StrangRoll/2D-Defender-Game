using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LevelBound>(out LevelBound levelBound))
            Destroy(gameObject);
        Hit(collision, _damage);
    }

    protected abstract void Hit(Collider2D collision, int damage);
  
}
