using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _leftDamage;
    [SerializeField]
    private GameObject _rightDamage;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotEnabled = false;
    [SerializeField]
    private bool _isSpeedBoostEnabled = false;
    [SerializeField]
    private bool _isShieldsEnabled = false;

    [SerializeField]
    private GameObject _shields;

    [SerializeField]
    private int _score;

    private UI_Manager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        // if spacekey and 0.5 secs have passed, spawn gameobject
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
    }

    public void Damage()
    {
        if (_isShieldsEnabled == true)
        {
            _isShieldsEnabled = false;
            _shields.SetActive(false);
            return;
        }
        _lives -= 1;

        if (_lives == 2)
        {
            _leftDamage.gameObject.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightDamage.gameObject.SetActive(true);
        }


        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotEnabled()
    {
        _isTripleShotEnabled = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotEnabled = false;
    }

    public void SpeedBoostEnabled()
    {
        _isSpeedBoostEnabled = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostEnabled = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsEnabled()
    {
        _isShieldsEnabled = true;
        _shields.SetActive(true);
    }

    private void ShootLaser()
    {        
        _canFire = Time.time + _fireRate;

        if (_isTripleShotEnabled == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }
    }

    private void CalculateMovement()
    {
        // Calculate direction
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(hInput, vInput, 0);        
        transform.Translate(direction * _speed * Time.deltaTime);
        
        // Determine Player bounds
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }  
    
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
