using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    // Declare and initialize variables
    private GameObject _gameManagerObject;
    private GameManagerScript _gameManager;
    private GameObject _spriteChild;
    private Rigidbody2D _myRigidbody;
    private Animator _myAnimation;
    private AudioSource _myFlapAudioSource;
    private AudioSource _mySmackAudioSource;
    private AudioSource _myScoreAudioSource;
    private AudioSource _myFallAudioSource;
    public bool hasHitPipe = false;
    public float flapPower = 10;
    private float _spriteRotation = 0;
    
    void Start() {
        // Find objects
        _gameManagerObject = GameObject.Find("GameManager");
        _spriteChild = transform.Find("Sprite").gameObject;

        // Get components
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimation = GetComponent<Animator>();
        _myFlapAudioSource = GetComponents<AudioSource>()[0];
        _mySmackAudioSource = GetComponents<AudioSource>()[1];
        _myScoreAudioSource = GetComponents<AudioSource>()[2];
        _myFallAudioSource = GetComponents<AudioSource>()[3];

        // Turn collision on for the pipes layer
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipes"), false);
    }

    void Update() {

        // Don't move until game start
        if (!_gameManager.gameStart && !hasHitPipe) {
            _myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else {
             // Start flapping
            _myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        // Flapping
        if (Input.GetButtonDown("Jump") && !hasHitPipe) {
            _myRigidbody.velocity = new Vector2(0, flapPower);

            // Play flap sound
            _myFlapAudioSource.Play(0);
        }

        // Rotate up if the player is going up
        float _rotationSpeed = 0.05f;
        if (!hasHitPipe && _myRigidbody.velocity.y >= (flapPower / 4)) {
            _spriteRotation = Mathf.Lerp(_spriteRotation, 40f, _rotationSpeed);
        }
        // If the player has hit the pipe, rotate down
        else if (hasHitPipe) {
            _spriteRotation = Mathf.Lerp(_spriteRotation, -90f, _rotationSpeed);
        }
        // If it's neither, go back to normal
        else {
            _spriteRotation = Mathf.Lerp(_spriteRotation, 0f, _rotationSpeed / 2);
        }

        // Update sprite rotation
        _spriteChild.transform.rotation = Quaternion.Euler(0, 0, _spriteRotation);
    }

    void OnCollisionEnter2D(Collision2D other) {
        // Get the tag
        string colliderTag = other.gameObject.tag;

        // Player hitting the pipe or the floor
        if ((colliderTag == "Pipe" || colliderTag == "Floor") && _gameManager.gameStart == true) {
            // Make sure the game is over
            hasHitPipe = true;
            _gameManager.gameStart = false;
            // Play smack sound
            _mySmackAudioSource.Play(0);
            _myFallAudioSource.Play(0);
            // Stop animation
            _myAnimation.enabled = false;

            // If the player hit the pipe, make sure to ignore it's collision
            if (colliderTag == "Pipe") {
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipes"), true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Get the tag
        string colliderTag = other.gameObject.tag;

        // Player scoring a point
        if (colliderTag == "PointGiver") {
            Debug.Log("Scored!");
            _gameManager.score++; // Add score by 1
            GameObject.Destroy(other.gameObject); // Then destroy itself

            // Play score sound
            _myScoreAudioSource.Play(0);

        }
    }
}
