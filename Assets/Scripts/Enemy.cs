using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _doubleEnemyLaser;
    private float _fireRate=2.5f;
    private float _lastFire;
    private AudioSource _explosionSound;
    Animator _deathAnimator;
    private float _speed = 2.0f;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _explosionSound = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
        _deathAnimator = GetComponent<Animator>();
        if (_deathAnimator == null)
        {
            Debug.LogError("The Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();

    }
    private void CalculateMovement()
    {
        // move down 4meters per s
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        //if bottom of screen, respawn at top with a new random x position
        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-8, 8);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _deathAnimator.SetTrigger("On_Enemy_Death");
            _explosionSound.Play();
            _speed = 0;
            Destroy(this.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
        if (other.tag == "Laser") 
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.IncreaseScore();
            }
            _deathAnimator.SetTrigger("On_Enemy_Death");
            _explosionSound.Play();
            _speed = 0;
            Destroy(this.gameObject.GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }
    }
    private void FireLaser()
    {
        if (Time.time>_lastFire+_fireRate)
        {
            Instantiate(_doubleEnemyLaser, transform.position, Quaternion.identity);
            _lastFire = Time.time;
            _fireRate = Random.Range(3f, 7f);
        }

    }
}
