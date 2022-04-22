using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _moneyText.text = _player.Money.ToString();
    }

}
