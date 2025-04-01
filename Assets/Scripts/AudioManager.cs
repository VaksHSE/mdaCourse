using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioSource[] audioSources;

    [SerializeField] public Slider settingSlider;

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
        }

    }
 
    public void PlaySound(int ind, bool dop=false)
    {
        if (ind > audioSources.Length)
            return;
        if(dop)
            audioSources[ind].pitch = Random.Range(0.7f, 1f);
        audioSources[ind].Play();
    }
    public void StopSound(int ind)
    {
        if (ind > audioSources.Length)
            return;
        audioSources[ind].Stop();
    }
    public void OffSound()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = 0;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AudioManager.instance.audioSources.Length; i++)
        {
            AudioManager.instance.audioSources[i].volume = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
