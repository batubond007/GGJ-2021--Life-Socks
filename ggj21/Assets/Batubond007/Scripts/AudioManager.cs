using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public AudioSource BackGround,
        Jump,
        DoubleJump,
        PickUp,
        Door,
        Walk;
    public void StopAudios()
    {
        Walk.Stop();
        Jump.Stop();
        DoubleJump.Stop();
    }
}
