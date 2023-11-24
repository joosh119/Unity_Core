using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class AudioManager : MonoBehaviour
{
    //Singleton pattern
    public static AudioManager instance;
    void Awake()
    {
        if(instance != null){
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    private AudioSource soundEffectSource;
    public static void PlaySound(AudioData aD){
        if(aD.timesToRepeat <=1){
            float modifiedVolume = aD.volume * GetVolumeModifier(aD.audioCategory);
            instance.soundEffectSource.PlayOneShot(aD.audioClip, modifiedVolume);
        }
        else{
            PlayRepeated(aD, aD.timesToRepeat, aD.audioClip.length);
        }
    }

    //Repeats audio clip a certain number of times
    public static void PlayRepeated(AudioData aD, int timesToRepeat, float delayTime){
        Timing.RunCoroutine(_PlayRepeated(aD, timesToRepeat, delayTime).CancelWith(instance.gameObject));
    }
    public static IEnumerator<float> _PlayRepeated(AudioData aD, int timesToRepeat, float delayTime){
        int c = 0;
        while(c < timesToRepeat){
            float modifiedVolume = aD.volume * GetVolumeModifier(aD.audioCategory);
            instance.soundEffectSource.PlayOneShot(aD.audioClip, modifiedVolume);

            yield return Timing.WaitForSeconds(delayTime);
        }
    }



    private static float GetVolumeModifier(AudioData.AudioCategory audioCategory){
        return .5f;
    }
}
