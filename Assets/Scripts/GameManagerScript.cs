using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    // Declare and initialize variables
    public int score;
    public bool gameStart = false;
    private GameObject _onScreenMessage;
    private GameObject _playerObject;
    private PlayerScript _player;

    void Start() {
        // Find objects
        _onScreenMessage = GameObject.Find("Message");
        _playerObject = GameObject.Find("Player");

        // Get components
        _player = _playerObject.GetComponent<PlayerScript>();
    }


    void Update() {
        // If the game started, delete the message on screen (if it exists)
        if (_onScreenMessage != null && gameStart) {
            GameObject.Destroy(_onScreenMessage);
        }

       // When pressing the jump button
       if (Input.GetButtonDown("Jump")) {
            // Start the game
            gameStart = true;

            // Reset the scene
            if (_player.hasHitPipe) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
       }
    }
}
