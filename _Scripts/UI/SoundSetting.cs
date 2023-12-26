using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine.UI;
using static MoreMountains.Tools.MMSoundManager;

public class SoundSetting : MonoBehaviour //set sound volume and input mode
{
    // Start is called before the first frame update
    public MMF_Player musicFB, soundFB;
    public MMF_MMSoundManagerTrackControl musicVolume, soundVolume;
    public Slider myMusicSlider,mySoundSlider;
    private void Awake()
    {
        musicVolume = musicFB.GetFeedbackOfType<MMF_MMSoundManagerTrackControl>();
        soundVolume = soundFB.GetFeedbackOfType<MMF_MMSoundManagerTrackControl>();
    }
 
    private void OnEnable()
    {
        myMusicSlider.value=MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Music, false);
        mySoundSlider.value=MMSoundManager.Instance.GetTrackVolume(MMSoundManagerTracks.Sfx, false);
    }
    public void ChangeMusicVolume(float amount)
    {
        musicVolume.Volume = amount;
        musicFB.PlayFeedbacks();

    }

    public void ChangeSoundVolume(float amount)
    {
        soundVolume.Volume = amount;
        soundFB.PlayFeedbacks();
       
    }
    
    
}
