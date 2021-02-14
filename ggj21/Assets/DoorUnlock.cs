using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public List<int> doorInfo = new List<int>();
    public bool isCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && CheckInput())
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            if (isCheckpoint)
            {
                RespawnSystem.instance.respawnPoint = transform.position + Vector3.right;
                RespawnSystem.instance.Items.Clear();
                foreach(var x in Inventory.instance.ItemIDs)
                    RespawnSystem.instance.Items.Add(x);
            }
        }
    }

    public bool CheckInput()
    {
        int count = 0;
        int wanted = doorInfo.Count;
        List<int> tmp = new List<int>(Inventory.instance.ItemIDs);
        List<int> deletionIndexes = new List<int>();
        for (int i = 0; i< tmp.Count;i++)
        {
            int id = tmp[i];
            foreach (var want in doorInfo)
            {
                if (id == want)
                {
                    count++;
                    tmp.RemoveAt(i);
                    deletionIndexes.Add(i);
                    i--;
                    break;
                }
            }
        }
        if(count == wanted)
        {
            GetComponent<Animator>().SetTrigger("opendoor");
            foreach(var x in deletionIndexes)
            {
                Inventory.instance.ItemIDs.RemoveAt(x);
            }
        }
        return count == wanted;
    }
}
