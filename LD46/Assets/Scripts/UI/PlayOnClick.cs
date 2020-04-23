using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnClick : MonoBehaviour
{
  public AudioClip noise;
  
  public void PlayNoise()
  {
    AudioManager.Instance.Play(noise, channel: AudioManager.AudioChannel.Sound);
  }

}
