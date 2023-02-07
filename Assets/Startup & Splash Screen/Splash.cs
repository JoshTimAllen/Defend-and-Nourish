using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour

{
    [SerializeField] float videoLength;
    private void Start()
    {
        StartCoroutine(Load());
    }
    public string loadLevel;
    IEnumerator Load()
    {
        yield return new WaitForSeconds(videoLength);//change to the time your video ends in secs
        SceneManager.LoadSceneAsync(loadLevel);
    }
}
