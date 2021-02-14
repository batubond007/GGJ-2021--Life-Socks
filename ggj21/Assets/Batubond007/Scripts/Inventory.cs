using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }
    #endregion
    private void Start()
    {
        foreach (var x in RespawnSystem.instance.Items)
            ItemIDs.Add(x);
    }
    public List<int> ItemIDs = new List<int>();
    public Image FillBlock1, FillBlock2;
    public Image Block1, Block2;
    public Sprite FreeImage;

    void Update()
    {

        if (ItemIDs.Count == 0)
        {
            Block1.sprite = FreeImage;
            Block2.sprite = FreeImage;
        }
        else if (ItemIDs.Count == 1)
        {
            Block1.sprite = RespawnSystem.instance.checkPointData.AllItems[ItemIDs[0]].GetComponent<ItemIDCard>().Sprite;
            Block2.sprite = FreeImage;

        }
        else if (ItemIDs.Count == 2)
        {
            Block1.sprite = RespawnSystem.instance.checkPointData.AllItems[ItemIDs[0]].GetComponent<ItemIDCard>().Sprite;
            Block2.sprite = RespawnSystem.instance.checkPointData.AllItems[ItemIDs[1]].GetComponent<ItemIDCard>().Sprite;
        }
    }

}
