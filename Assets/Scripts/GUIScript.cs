using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

    // Declare and initialize variables
    private GameObject _gameManagerObject;
    private GameManagerScript _gameManager;
    private GameObject _scoreCounterObject;
    private Text _scoreCounterText;

    void Start() {
        // Declare variables
        _gameManagerObject = GameObject.Find("GameManager");
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();
        _scoreCounterObject = transform.Find("ScoreCounter").gameObject;

        // Get components
        _gameManager = _gameManagerObject.GetComponent<GameManagerScript>();
        _scoreCounterText = _scoreCounterObject.GetComponent<Text>();
    }


    void Update() {
        // Update score
        _scoreCounterText.text = _gameManager.score.ToString();
    }
}
