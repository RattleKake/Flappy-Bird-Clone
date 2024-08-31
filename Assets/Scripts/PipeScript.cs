using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour {

    // Declare and initialize variables
    private GameObject _gameManagerObject;
    private GameManagerScript _gameManager;
    private Rigidbody2D _myRigidbody;
    public float pipeSpeed = 1f;
    public float despawnTime = 5f; // In seconds
    private float _currentTime = 0;

    void Start() {
        // Find objects
        _gameManagerObject = GameObject.Find("GameManager");
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();

        // Get components
        _myRigidbody = GetComponent<Rigidbody2D>();
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();
    }


    void Update() {

        // Destroy this object if time is up
        _currentTime += Time.deltaTime; // Count up

        if (_gameManager.gameStart && _currentTime >= despawnTime) {
            GameObject.Destroy(gameObject);
        }


        // Scroll the pipe to the left
        _myRigidbody.velocity = new Vector2(-pipeSpeed, 0);


        // If the game is over, stop scrolling
        if (!_gameManager.gameStart) {
            _myRigidbody.velocity = new Vector2(0, 0);
        }

    }
}
