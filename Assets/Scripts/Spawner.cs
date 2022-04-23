using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private float _timeAfterLastSpawn;
    private int _killed;
    private int _spawned;

    public event UnityAction AllEnemiesKilled;
    public event UnityAction<int, int> DeadEnemyCountChanged;

    private void Start()
    {
        SetWave(0);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;
        _timeAfterLastSpawn += Time.deltaTime;  

        if (_timeAfterLastSpawn >= _currentWave.Delay && _spawned < _currentWave.Count)
        {
            InstantiateEnemy();
            _timeAfterLastSpawn = 0;
            _spawned++;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    { 
        _killed = 0;
        _spawned = 0;
        DeadEnemyCountChanged?.Invoke(0, 1);
        _currentWave = _waves[index];
        _currentWaveIndex = index;
    }

    public void NextWave()
    {
        SetWave(++_currentWaveIndex);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _killed++;
        DeadEnemyCountChanged?.Invoke(_killed, _currentWave.Count);
        _player.AddMoney(enemy.Reward);

        if (_killed >= _currentWave.Count)
        {
            if (_waves.Count > _currentWaveIndex + 1)
                AllEnemiesKilled?.Invoke();
            _currentWave = null;
        }    
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
