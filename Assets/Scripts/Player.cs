using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{   //public or private reference
    //data type (int, float, bool, string)
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _fireRate = 0.15f;
    private float _canFire = -1;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private UI_Manager _uiManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    private bool _isShield = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private AudioClip _laserShot;
// Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(new Vector3(1, 0, 0) * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        //if player position on the y is greater than 0, y position equal 0

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.05f, transform.position.z), Quaternion.identity);
        }
        AudioSource.PlayClipAtPoint(_laserShot, transform.position);
    }
    public void Damage()
    {
        if (_isShield == false)
        {
            _lives--;
            _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
            _uiManager.UpdateLives(_lives);
            if (_lives==2)
            {
                _rightEngine.SetActive(true);
            }
            if (_lives==1)
            {
                _leftEngine.SetActive(true);
            }
        }
        else
        {
            _shield.SetActive(false);
            _isShield = false;
        }
        if (_lives<= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.GameOver();
            //communicate with spawn manager
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void IncreaseSpeed()
    {
        _speed = 7f;
        StartCoroutine(HighSpeed());
    }

    IEnumerator HighSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 3.5f;
    }
    public void ActivateShield()
    {
        _isShield = true;
        _shield.SetActive(true);
    }
    public void IncreaseScore()
    {
        _score += 10;
    }
    public int UIScore()
    {
        return _score;
    }
}



