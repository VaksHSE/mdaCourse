using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettSlider : MonoBehaviour
{
    [SerializeField] public Slider settingSlider;
    [SerializeField] public Slider settingEFFSlider;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.settingSlider = settingSlider;
        settingSlider.value = AudioManager.instance.audioSources[2].volume;
        settingEFFSlider.value = AudioManager.instance.audioSources[0].volume;
    }
    public void ChangeSound()
    {
        //Debug.Log(AudioManager.instance.audioSources);
        //Debug.Log(AudioManager.instance.audioSources[1].volume);
        AudioManager.instance.settingSlider = settingSlider;
        //Debug.Log(AudioManager.instance.settingSlider.value);
        
        for (int i = 2; i < AudioManager.instance.audioSources.Length-3; i++)
        {
            AudioManager.instance.audioSources[i].volume = AudioManager.instance.settingSlider.value;
        }
    }

    public void ChangeSoundEFF()
    {
        Debug.Log(AudioManager.instance.audioSources);
        Debug.Log(AudioManager.instance.audioSources[1].volume);
        AudioManager.instance.settingSlider = settingEFFSlider;
        Debug.Log(AudioManager.instance.settingSlider.value);

        for (int i = 0; i < 2; i++)
        {
            AudioManager.instance.audioSources[i].volume = AudioManager.instance.settingSlider.value;
        }
        for (int i = 7; i < AudioManager.instance.audioSources.Length; i++)
        {
            AudioManager.instance.audioSources[i].volume = AudioManager.instance.settingSlider.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
