using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {
    public Dropdown resList;
    public Text currentRes;
    Resolution[] resolutions;
    public Slider volumeSlider, qualitySlider, brightnessSlider;
    public Text volumeValue, qualityValue, brightnessValue;
    int qualityLevel;
    // Use this for initialization
    void Start () {
        Dropdown.OptionData temp;
        resList.ClearOptions();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            temp = new Dropdown.OptionData(ResToString(resolutions[i]));
            if (!exist(temp))
            {
                resList.options.Add(temp);
                if (Screen.currentResolution.height == resolutions[i].height && Screen.currentResolution.width == resolutions[i].width) 
                {
                    resList.value = resList.options.Count;
                    currentRes.text = ResToString(resolutions[i]);
                }
            }
        }
        resList.onValueChanged.AddListener(delegate { updateResolution(); });
        
        /* level 0:16, 1:32, 2:48, 3:64, 4:80, 5:100 */
        Debug.Log("Starting Value: " + QualitySettings.GetQualityLevel());
        qualitySlider.value = ((QualitySettings.GetQualityLevel()+1) * 16)/100f;
        qualitySlider.value = qualityTag(qualitySlider.value);
        Debug.Log("Slider Value: " + qualitySlider.value);
        qualityValue.text = "" + qualityTag(qualitySlider.value) + "%";
        qualitySlider.onValueChanged.AddListener(delegate { updateQuality(); });

        volumeSlider.value = AudioListener.volume;
        volumeValue.text = "" + (int)(volumeSlider.value * 100) + "%";
        volumeSlider.onValueChanged.AddListener(delegate { volumeValue.text = "" + (int)(volumeSlider.value * 100) + "%"; AudioListener.volume = volumeSlider.value; });

        brightnessValue.text = "" + (int)(brightnessSlider.value * 100) + "%";
        brightnessSlider.onValueChanged.AddListener(delegate { brightnessValue.text = "" + (int)(brightnessSlider.value * 100) + "%"; });
    }

    // Update is called once per frame
    void Update () {
		
	}

    void updateResolution()
    {
        Screen.SetResolution(resolutions[resList.value].width, resolutions[resList.value].height, true);
        currentRes.text = resList.options[resList.value].text;
        Debug.Log("resList value: " + resList.value);
    }

    void updateQuality()
    {
        int temp = (int)(qualitySlider.value * 100);
        int newLevel = 0;
        if (temp > 80)
        {
            qualitySlider.value = 1f;
            temp = 100;
            newLevel = 5;
        }
        else if (temp > 64)
        {
            qualitySlider.value = 0.8f;
            temp = 80;
            newLevel = 4;
        }
        else if (temp > 48)
        {
            qualitySlider.value = 0.64f;
            temp = 64;
            newLevel = 3;
        }
        else if (temp > 32)
        {
            qualitySlider.value = 0.48f;
            temp = 48;
            newLevel = 2;
        }
        else if (temp > 16)
        {
            qualitySlider.value = 0.32f;
            temp = 32;
            newLevel = 1;
        }
        else
        {
            qualitySlider.value = 0.16f;
            temp = 16;
            newLevel = 0;
        }
        qualityValue.text = "" + temp + "%";
        QualitySettings.SetQualityLevel(newLevel, true);
        //Debug.Log("Current Level Value: " + QualitySettings.GetQualityLevel());
        //Debug.Log("_______Slider Value: " + qualitySlider.value);
    }

    int qualityTag(float input)
    {
        if (input > 0.80)
        {
            return 100;
        }
        else if (input > 0.64)
        {
            return 80;
        }
        else if (input > 0.48)
        {
            return 64;
        }
        else if (input > 0.32)
        {
            return 48;
        }
        else if (input > 0.16)
        {
            return 32;
        }
        else
        {
            return 16;
        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    bool exist(Dropdown.OptionData input)
    {
        foreach(Dropdown.OptionData option in resList.options)
        {
            if (option.text.Equals(input.text))
                return true;
        }
        return false;
    }

    public void Back2Menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
