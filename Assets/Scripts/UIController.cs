using System.Threading;
using UnityEngine;
using UnityEngine.UI;
//UI controller for the sound panel


public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    //sound on off boxes
    public Button musicButton;
    public Sprite muteMusicImage;
    public Sprite MusicImage;
    public Button SFXButton;
    public Sprite muteSFXImage;
    public Sprite SFXImage;


    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();

        //Toggles the music sound on/off image
       
        if (AudioManager.Instance.musicSource.mute)
        {
            musicButton.image.sprite = muteMusicImage;
        }
        else
        {
            musicButton.image.sprite = MusicImage;
        }
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();

        //Toggles the VFX sound on/off image

        if (AudioManager.Instance.sfxSource.mute)
        {
            SFXButton.image.sprite = muteSFXImage;
        }
        else
        {
            SFXButton.image.sprite = SFXImage;
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);

    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);

    }
}
