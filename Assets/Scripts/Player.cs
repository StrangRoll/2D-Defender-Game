using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChaged;
    public event UnityAction<int> MoneyChaged;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponIndex]);
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
        MoneyChaged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChaged?.Invoke(Money);
    }

    public void NextWeapon()
    {
        if (_currentWeaponIndex >= _weapons.Count - 1)
            _currentWeaponIndex = 0;
        else
            _currentWeaponIndex++;
        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponIndex <= 0)
            _currentWeaponIndex = _weapons.Count - 1;
        else
            _currentWeaponIndex--;
        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
