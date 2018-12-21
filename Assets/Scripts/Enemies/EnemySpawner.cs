
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	[SerializeField]	
	private float timeBetweenSpawn = 1f;
	[SerializeField]
	private Enemy[] enemyTypes;
	[SerializeField]
	private Transform[] spawnLocations;

	internal static List<Enemy> enemies;

	private void Awake()
	{
		enemies = new List<Enemy>();
		StartCoroutine(SpawningEnemies());
	}

	IEnumerator SpawningEnemies()
	{
		while(true)
		{
			yield return new WaitForSeconds(timeBetweenSpawn);
			int enemyIndex = Random.Range(0, enemyTypes.Length);
			int locationIndex = Random.Range(0, spawnLocations.Length);
			enemies.Add(Instantiate(enemyTypes[enemyIndex], spawnLocations[locationIndex].position, spawnLocations[locationIndex].rotation));
		}
	}

	public void Reset()
	{
		enemies.Clear();
	}
}
