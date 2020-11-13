using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _asteroidExplosionPrefab;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 2, Space.Self);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Laser")
        {
            Destroy(other.gameObject);
            GameObject asteroidExplosion = Instantiate(_asteroidExplosionPrefab,transform.position ,Quaternion.identity);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
