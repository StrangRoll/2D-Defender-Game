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
    private int _spawned;
    private int _killed;

    public event UnityAction AllEnemiesSpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(0);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;
        _timeAfterLastSpawn += Time.deltaTime;  

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
            if (_spawned >= _currentWave.Count)
            {
                if (_waves.Count > _currentWaveIndex + 1)
                    AllEnemiesSpawned?.Invoke();
                _currentWave = null;
            }
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
        _spawned = 0;
        EnemyCountChanged?.Invoke(0, 1);
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
        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
