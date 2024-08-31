using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour {

    // Declare and initialize variables
    private GameObject _gameManagerObject;
    private GameManagerScript _gameManager;
    public GameObject pipeObject;
    public float pipeSpawnYRangeMin, pipeSpawnYRangeMax;
    public float spawnTime = 5f; // In seconds
    private float _currentTime = 0;



    void Start() {
        // Find objects
        _gameManagerObject = GameObject.Find("GameManager");

        // Get components
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();
    }


    void Update() {
         // Add to time
         _currentTime += Time.deltaTime;

        // Spawn a pipe in a random (but within set range) location
        if (_gameManager.gameStart && _currentTime >= spawnTime) {
            float pipeYLocation = Random.Range(pipeSpawnYRangeMin, pipeSpawnYRangeMax);
            pipeObject.layer = LayerMask.NameToLayer("Pipes");
            GameObject.Instantiate(pipeObject, new Vector3(transform.position.x, pipeYLocation, transform.position.z), Quaternion.identity);
            _currentTime = 0; // Reset time
        }
    }
}
