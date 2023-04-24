using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRepository : MonoBehaviour
{

    [SerializeField] private EnemyData[] repository;

    [System.Serializable]
    public struct EnemyData
    {
        public Color color;
        public int life;
        public int score;
        [HideInInspector] public int id;
    }

    private void Awake()
    {
        for (int i = 0; i < repository.Length; i++)
        {
            repository[i].id = i; //Setting ids based on array index.
        }
    }

    public EnemyData GetRandomEnemyData()
    {
        return repository[Random.Range(0, repository.Length)];
    }

}
