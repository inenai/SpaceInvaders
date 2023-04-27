﻿using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{

    public class EnemiesController : MonoBehaviour
    {
        [Header("Configiration")]
        [SerializeField] private float minFireTime = 0f;
        [SerializeField] private float maxFireTime = 3f;
        [SerializeField] private int enemyRows = 4;
        [SerializeField] private int enemyColumns = 10;
        [SerializeField] private Vector2 offset = new Vector2(0.35f, 0.3f);
        [SerializeField] private Vector2 originViewport = new Vector2(0.2f, 0.9f);
        [SerializeField] private float resetDelay = 0.6f;

        [Header("References")]
        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private EnemyRepository enemyRepo;

        private Enemy[][] enemies;
        private HashSet<Enemy> aliveEnemies;
        private List<Enemy> firingEnemies;
        private Vector3 vToWorldPoint;
        private Vector2 initialPosition;
        private float currentFireDelay;
        private float fireTimer;
        private float resetTimer;
        private bool resetEnemies;

        public EnemyData GetRandomEnemyData()
        {
            return enemyRepo.repository[Random.Range(0, enemyRepo.repository.Length)];
        }

        private void Awake()
        {
            EventDispatcher.OnEnemyKilled += EnemyKilled;
            vToWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(originViewport.x, originViewport.y, 0f));
        }

        private void Start()
        {
            ResetEnemies();
        }

        private void ResetEnemies()
        {
            EventDispatcher.EnemyReset();
            if (firingEnemies != null)
            {
                firingEnemies.Clear();
            }
            else
            {
                firingEnemies = new List<Enemy>();
            }
            if (aliveEnemies != null)
            {
                aliveEnemies.Clear();
            }
            else
            {
                aliveEnemies = new HashSet<Enemy>();
            }
            enemies = new Enemy[enemyRows][];
            for (int i = 0; i < enemyRows; i++)
            {
                enemies[i] = new Enemy[enemyColumns];
            }
            initialPosition = new Vector2(vToWorldPoint.x, vToWorldPoint.y);
            currentFireDelay = Random.Range(minFireTime, maxFireTime);

            for (int i = 0; i < enemies.Length; i++)
            {
                for (int j = 0; j < enemies[i].Length; j++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(initialPosition.x + (offset.x * j), initialPosition.y - (offset.y * i), 0f), Quaternion.identity, transform);
                    //TODO Improvable: 
                    //Enemies areinstantiated to the right by adding offset. 
                    //Could be instantiated in accordance to viewport so it will look centered in any aspect ratio.
                    enemies[i][j] = enemy.GetComponent<Enemy>();
                    enemies[i][j].Setup(GetRandomEnemyData(), i, j); //Give random enemy type.
                    aliveEnemies.Add(enemies[i][j]); //Set with alive enemies. Enemies will be reset when this set is empty.
                }
            }
            foreach (Enemy enemy in enemies[enemies.Length - 1])
            {
                firingEnemies.Add(enemy); //At first, all the lower row of enemies will be firing.
            }
        }

        private void EnemyKilled(int rowIndex, int columnIndex, bool autoKill)
        {
            aliveEnemies.Remove(enemies[rowIndex][columnIndex]);

            if (aliveEnemies.Count == 0)
            {
                resetEnemies = true; //No more enemies are alive or not marked as killed.
                return;
            }

            if (!autoKill)
            {
                KillSameTypeNeighbors(rowIndex, columnIndex);
            }

            RecalculateFiringEnemies(rowIndex, columnIndex);
        }

        private void RecalculateFiringEnemies(int rowIndex, int columnIndex) {
            if (firingEnemies.Contains(enemies[rowIndex][columnIndex])) //If this was a firing enemy, the one avobe will take its place.
            {
                firingEnemies.Remove(enemies[rowIndex][columnIndex]);
                int newRow = rowIndex - 1;
                //Browse enemies upwards in search for a candidate to start firing instead of this one:
                while (newRow >= 0 && (enemies[newRow][columnIndex] == null || enemies[newRow][columnIndex].IsDead()))
                {
                    newRow--;
                }
                if (newRow >= 0 && enemies[newRow][columnIndex] != null)
                {
                    firingEnemies.Add(enemies[newRow][columnIndex]); //If a candidate is found, it will be added to the list of firing enemies.
                }
            }
        }

        private void KillSameTypeNeighbors(int rowIndex, int columnIndex) {
            List<Vector2Int> enemiesToKill = new List<Vector2Int>(); //Adjacent enemies of the same type will be put here to undergo this same process. 
            Queue<Vector2Int> neighbors = new Queue<Vector2Int>();

            //Check if any adjacent alive enemies share types with the one being killed:
            int enemyType = enemies[rowIndex][columnIndex].GetKind();

            neighbors.Enqueue(new Vector2Int(rowIndex, columnIndex)); //Start with killed by player enemy

            while (neighbors.Count > 0)
            {
                Vector2Int currentNeighbor = neighbors.Dequeue();

                //Check up
                if (rowIndex > 0)
                {
                   CheckNeighbor(new Vector2Int(currentNeighbor.x - 1, currentNeighbor.y), enemyType, neighbors);
                }
                //Check down
                if (rowIndex < enemies.Length - 1)
                {
                    CheckNeighbor(new Vector2Int(currentNeighbor.x + 1, currentNeighbor.y), enemyType, neighbors);
                }
                //Check left
                if (columnIndex > 0)
                {
                    CheckNeighbor(new Vector2Int(currentNeighbor.x, currentNeighbor.y - 1), enemyType, neighbors);
                }
                //Check right
                if (columnIndex < enemies[currentNeighbor.x].Length - 1)
                {
                    CheckNeighbor(new Vector2Int(currentNeighbor.x, currentNeighbor.y + 1), enemyType, neighbors);
                }
               
                if (currentNeighbor.x != rowIndex || currentNeighbor.y != columnIndex)  //Do not kill already killed by player enemy
                {
                    enemies[currentNeighbor.x][currentNeighbor.y].MarkForKill();
                    enemiesToKill.Add(currentNeighbor);
                }                                
            }

            foreach (Vector2Int enemyCoords in enemiesToKill)
            {
                //Finally, restart this process with any enemies of same type that should die too.
                enemies[enemyCoords.x][enemyCoords.y].Kill(true);
            }
            
        }

        private void CheckNeighbor(Vector2Int coords, int currentEnemyId, Queue<Vector2Int> neighbors)
        {
            if (coords.x < 0 || coords.y < 0) return;
            if (coords.x >= enemies.Length || coords.y >= enemies[coords.x].Length) return;

            Enemy neighbor = enemies[coords.x][coords.y];

            if ((neighbor.GetKind() == currentEnemyId) //Should be same type as current enemy
            && !(neighbor.MarkedForKill())) //And should not be marked for kill already.
            {
                neighbors.Enqueue(coords);
            }
        }

        private void Update()
        {
            if (resetEnemies)
            {
                if (resetTimer < resetDelay)
                {  //waiting so last explosion will be shown.
                    resetTimer += Time.deltaTime;
                }
                else
                {
                    resetEnemies = false;
                    ResetEnemies();
                    resetTimer = 0f;
                    return;
                }
            }

            //If the random shooting timer is reached, choose a random firing enemy and make it shoot.
            if ((fireTimer >= currentFireDelay) && (firingEnemies.Count > 0))
            {
                firingEnemies[Random.Range(0, firingEnemies.Count)].Shoot();
                currentFireDelay = Random.Range(minFireTime, maxFireTime);
                fireTimer = 0f;
            }
            fireTimer += Time.deltaTime;
        }

        private void OnDestroy()
        {
            EventDispatcher.OnEnemyKilled -= EnemyKilled;
        }
    }
}