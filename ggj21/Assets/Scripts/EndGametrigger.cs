using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGametrigger : MonoBehaviour
{
    public GameObject EndGamePanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            EndGamePanel.SetActive(true);
        }
            
    }
}
