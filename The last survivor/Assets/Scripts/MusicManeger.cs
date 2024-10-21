using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManeger : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject mute;
    [SerializeField] private GameObject unMute;

    public void MuteMethod()
    {
        music.SetActive(false);
        unMute.SetActive(true);
        mute.SetActive(false);
    }
    public void UnMuteMethod()
    {
        music.SetActive(true);
        mute.SetActive(true);
        unMute.SetActive(false);
    }
}
