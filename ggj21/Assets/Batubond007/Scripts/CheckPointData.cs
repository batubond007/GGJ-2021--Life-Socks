using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CheckPointData", menuName = ("Data/CheckPointData"))]
public class CheckPointData : ScriptableObject
{
    public Vector3 CheckPoint;
    public List<GameObject> AllItems;
    public List<int> LastItemIds;
}
