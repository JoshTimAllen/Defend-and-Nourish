using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public List<GameObject> enemyList;
    public GameObject enemy;
    public List<GameObject> enemySpawnPoints = new List<GameObject>();

    [SerializeField]
    int score;
    bool escapeButton;

    IEnumerator coroutine;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Update() {
        Wave();
        Pause();
    }

    void Start() {
        enemyList = new List<GameObject>();
        coroutine = Awardpoints(1f);
        StartCoroutine(coroutine);
        escapeButton = false;
    }

    IEnumerator Awardpoints(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            score += 1;
        }
        yield return null;
    }
    int maxEnemies = 11;
    int maxMaxMaxMaxMaxEnemies = 60;
    //Spawns a random number of enemies at the spawn enemy location. Doesn't create anymore until first set is killed off.
    void Wave() {
        if (enemyList.Count == 0) {
            maxEnemies += 3;
            maxEnemies = Mathf.Clamp(maxEnemies, 0, maxMaxMaxMaxMaxEnemies);
            for (int i = 0; i < maxEnemies; i++) {
                GameObject enemySpawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count - 1)];
                enemyList.Add(Instantiate(enemy, new Vector3(Random.Range(enemySpawnPoint.transform.position.x - 3, enemySpawnPoint.transform.position.x + 3), 1, Random.Range(enemySpawnPoint.transform.position.z - 3, enemySpawnPoint.transform.position.z + 3)), Quaternion.identity));
            }
        }
    }

    void Pause() {
        if (Input.GetButtonDown("Cancel") && escapeButton == false) {
            Time.timeScale = 0;
            escapeButton = true;
        }
        else if (Input.GetButtonDown("Cancel") && escapeButton == true) {
            Time.timeScale = 1;
            escapeButton = false;
        }
    }

    //Uses the Onclick function in unity editor
    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    //Loads the game scene again and reset everything using the Onclick function
    public void ResetGame() {
        SceneManager.LoadScene(1);
    }

}
