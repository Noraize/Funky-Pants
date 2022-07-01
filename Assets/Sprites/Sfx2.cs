using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sfx2 : MonoBehaviour
{
    [SerializeField] Slider volumeslider;
    bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();
        }
        else
        {
            load();
        }
    }

    public void changevolume()
    {
        AudioListener.volume = volumeslider.value;
        save();
    }

    private void load()
    {
        volumeslider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeslider.value);
    }

    public void button()
    {
        if (muted == false)
        {
            AudioListener.volume = 0;
            muted = true;
        }
        else
        {
            AudioListener.volume = 1;
            muted = false;
        }
    }
}
