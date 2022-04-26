using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _mouseScrollWheelInput;

    // Update is called once per frame
    void Update()
    {
        _mouseScrollWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (_mouseScrollWheelInput > 0)
            _player.NextWeapon();

        if (_mouseScrollWheelInput < 0)
            _player.PreviousWeapon();
    }
}
