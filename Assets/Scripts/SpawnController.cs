using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Vector3 screenCenter;
    private float spawnEnemyDistance = 30f;

    private int spawnedEnemies = 0;
    private int enemiesAlive = 0;
    private int initialSpawnedEnemies;
    private int numberOfEnemiesToSpawn;

    [SerializeField] private GameEvent _nextWaveGameEvent = default;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject particleExplosion;

    [SerializeField] private DifficultyCurvesScriptableObject difficultyData;

    float minimumY;
    float maximumY;
    float minimumX;
    float maximumX;


    //Awake is always called before any Start functions
    void Awake()
    {
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        Enemy.OnEnemyDestroy += CreateExplosion;
        Enemy.OnEnemyDestroy += CheckEnemiesAlive;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDestroy -= CreateExplosion;
        Enemy.OnEnemyDestroy -= CheckEnemiesAlive;
    }

 

    public void PlayerSpawn()
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(-180, 0, 0));
    }

    public void DestroyPlayer()
    {
        Debug.Log("DestroyPlayer");
        if (GameObject.FindWithTag("Player") != null)
            Destroy(GameObject.FindWithTag("Player"));
    }

    public Vector3 GetNewPosition(Vector3 position)
    {
        return new Vector3(screenCenter.x - position.x, screenCenter.y - position.y, 0);
    }

    public void CreateExplosion(Vector3 position)
    {
        GameObject particlesInstance = Instantiate(particleExplosion, position, Quaternion.identity);
        Destroy(particlesInstance, 2f);
    }

    public void CheckEnemiesAlive(Vector3 position)
    {
        enemiesAlive--;
        if (enemiesAlive == 0)
        {
            DestroyPlayer();
            _nextWaveGameEvent.RaiseEvent();
        }
         
    }

    public void SpawnWave(int wave)
    {
        spawnedEnemies = 0;
        enemiesAlive = 0;
        numberOfEnemiesToSpawn = Mathf.FloorToInt( difficultyData.SpawnCountCurve.Evaluate(wave));
        initialSpawnedEnemies = (wave == 0) ? numberOfEnemiesToSpawn :  Random.Range(4,6);
        StopCoroutine("SpawnEnemiesForWave");
        StartCoroutine(SpawnEnemiesForWave(wave));
      
    }

    private IEnumerator SpawnEnemiesForWave(int wave)
    {
        float delaySecondsSpawn = difficultyData.SpawnRateCurve.Evaluate(wave);
        WaitForSeconds waitSecondsDelay = new WaitForSeconds(difficultyData.SpawnRateCurve.Evaluate(wave));
        while (spawnedEnemies < numberOfEnemiesToSpawn)
        {
            SpawnEnemyWithLevel(wave);
            spawnedEnemies++;
            enemiesAlive++;
            // if (spawnedEnemies <= initialSpawnedEnemies)
                yield return null;
            //else
              //  yield return waitSecondsDelay;
        }
             
    }

    private void SpawnEnemyWithLevel(int level)
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = spawnDirection * spawnEnemyDistance;

        GameObject asteroidInstance = PoolingController.instance.GetWeightedPoolObject();
        asteroidInstance.GetComponent<Enemy>().SetLevelValues(difficultyData, level);
        Collider[] hitColliders = Physics.OverlapSphere(spawnPoint, asteroidInstance.transform.localScale.x/2);
        if(hitColliders.Length>0)
        {
            Debug.Log("respawn");
            SpawnEnemyWithLevel(level);
        }
        else
        {
            asteroidInstance.transform.position = spawnPoint;
            asteroidInstance.SetActive(true);

        }
    }

    public void ClearEnemies() => PoolingController.instance.DisablePoolingObjects();

 

}
