using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicStart : MonoBehaviour
{
    private bool canSkip = false;

    private void Start()
    {
        StartCoroutine(SkipScene());
    }

    private void Update()
    {
        if (canSkip)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator SkipScene()
    {
        yield return new WaitForSeconds(23f);
        canSkip = true;
    }
}
