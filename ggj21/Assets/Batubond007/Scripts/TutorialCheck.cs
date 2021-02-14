using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCheck : MonoBehaviour
{
    public GameObject panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(ShowPanel3Sec());
    }
    IEnumerator ShowPanel3Sec()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Time.timeScale = 0;
        panel.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
