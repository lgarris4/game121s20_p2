using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] public string playerPool, enemyPool;
    [SerializeField] private int playerPoolSize, enemyPoolSize;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //create player then enemy shot pool;
        ProjectilePool.instance.CreateShotPool(playerPool, playerPoolSize, shotPrefab);
        ProjectilePool.instance.CreateShotPool(enemyPool, enemyPoolSize, shotPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
}
