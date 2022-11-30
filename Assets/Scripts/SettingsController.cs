using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    private float music;
    public Slider musicSlider;
    public Text musicText;
    private float sound;
    public Slider soundSlider;
    public Text soundText;

    private bool vibration;
    public Toggle vibrationToggle;


    public void SetMusic() 
    {
        music = musicSlider.value;
        musicText.text = (music * 100).ToString("f0");
    }

    public void SetSound()
    {
        sound = soundSlider.value;
        soundText.text = (sound * 100).ToString("f0");
        
    }

    public void Open() 
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        soundSlider.value = PlayerPrefs.GetFloat("Sound");

        SetMusic();
        SetSound();
    }

    public void Close() 
    {
        PlayerPrefs.SetFloat("Music", music);
        PlayerPrefs.SetFloat("Sound", sound);
    }
}