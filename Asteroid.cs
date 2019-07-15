using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;

    [SerializeField]
    private GameObject _explosion;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rotate object on z axis
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2.75f);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject);
        }
    }
}
