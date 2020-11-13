using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private AudioClip _powerupSound;
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        if (transform.position.y <= -6f)
        {
            Destroy(this.gameObject);
        }
        //when we leave the screen destroy this object
    }
    //ontriggercollision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {   
                switch(_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.IncreaseSpeed();
                        break;
                    case 2:
                        player.ActivateShield();
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_powerupSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}
