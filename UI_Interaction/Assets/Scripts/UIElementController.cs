using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class UIElementController : MonoBehaviour


{

    public Slider volumeSlider;
    public Slider sizeSlider;
    public Toggle soundToggle;
    public Toggle lightToggle;
    public Text timerText;
    public GameObject timeUpText;
    public Light sceneLight;
    //public GameObject _lightpanel;
    public AudioSource backgroundSound;
    public GameObject objectToResize;

    private float startTime;
    private bool timerRunning;
    private float targetTime = 65f;


    void Start()
    {
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        sizeSlider.onValueChanged.AddListener(AdjustObjectSize);

        soundToggle.onValueChanged.AddListener(ToggleSound);
        lightToggle.onValueChanged.AddListener(ToggleLight);

        timerRunning = true;
        startTime = Time.time;
        timeUpText.SetActive(false);
    }

    
    void Update()
    {
        if (timerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimer(elapsedTime);

           
            if (elapsedTime >= targetTime)
            {
                StopTimer();
            }
        }

    }

    public void AdjustVolume(float value)
    {
        backgroundSound.volume = value;
    }

    public void AdjustObjectSize(float value)
    {
        objectToResize.transform.localScale = Vector3.one * value;
    }

    public void ToggleSound(bool isOn)
    {
        backgroundSound.mute = !isOn;
    }
    public void ToggleLight(bool isOn)
    {

        if (isOn)
        {
            sceneLight.enabled = true;
            sceneLight.intensity = 1f;
        }
        else
        {
            sceneLight.intensity = 0f; 
            sceneLight.enabled = false;
        }

    }

    private void UpdateTimer(float elapsedTime)
    {

        if (elapsedTime < targetTime)
        {
            int hours = Mathf.FloorToInt(elapsedTime / 3600f);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        else
        {
            StopTimer();
        }
    }

    private void StopTimer()
    {
        timerRunning = false;
        timerText.text = "Time Up";
        timeUpText.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
