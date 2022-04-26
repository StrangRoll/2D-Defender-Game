using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon
{
    [SerializeField] private float _timeBetweenShooting;
    [SerializeField] private int _bulletsCount;

    private bool _isShooting;
    private Transform _shootPoint;


    public override void Shoot(Transform shootPoint)
    {
        if (_isShooting != false)
        {
            _isShooting = true;
            _shootPoint = shootPoint;
            StartCoroutine(Shooting());
        }
    }

    private IEnumerator Shooting()
    {
        var waitForNewShoting = new WaitForSeconds(_timeBetweenShooting);
        for (int i = 0; i < _bulletsCount; i++)
        {
            Instantiate(Bullet, _shootPoint.position, Quaternion.identity);
            yield return waitForNewShoting;
        }
        _isShooting = false;
    }
}
