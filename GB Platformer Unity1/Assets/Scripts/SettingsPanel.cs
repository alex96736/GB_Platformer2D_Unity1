using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Slider SliderVolume;
    public Toggle ToggleMute;
    public bool mute;
    public float volume;
    private float PreVolume;
    public GameObject Panel_MainMenu;

    public void SliderVolumeSinc()
    {
        if (SliderVolume.value == 0)
        {
            ToggleMute.isOn = true;
        } 
        else
        {
            PreVolume = SliderVolume.value;
            ToggleMute.isOn = false;
        }
    }
    public void ToggleMuteSinc()
    {
        if(ToggleMute.isOn == true)
        {
            SliderVolume.value = 0;
        } 
        else
        {
            SliderVolume.value = PreVolume;
        }
    }
    public void SaveSettings()
    {
        mute = ToggleMute.isOn;
        volume = SliderVolume.value;
        BackToMenu();
    }
    public void BackToMenu()
    {
        Panel_MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
