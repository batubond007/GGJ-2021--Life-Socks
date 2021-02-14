using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem instance;

    private void Awake()
    {
        instance = this;
        respawnPoint = checkPointData.CheckPoint;
        foreach (var x in checkPointData.LastItemIds)
            Items.Add(x);
    }
    public CheckPointData checkPointData;
    public List<int> Items;

    public Vector3 respawnPoint;

    private void Update()
    {
        checkPointData.CheckPoint = respawnPoint;
        if(checkPointData.LastItemIds.Count != Items.Count)
        {
            checkPointData.LastItemIds.Clear();
            foreach (var x in Items)
            {
                checkPointData.LastItemIds.Add(x);
            }
        }
        
    }

}
