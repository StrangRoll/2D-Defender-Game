using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapon;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChaged;

    private void Start()
    {
        _currentWeapon = _weapon[0];
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChaged?.Invoke(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
    }
}
