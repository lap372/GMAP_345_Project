using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DayNightDuskCycle : MonoBehaviour
{
    public float phaseDuration = 30f; // Duration for day, dusk, and night
    public Image clockFace;             //Reference to the UI image for the face of the clock
    public GameObject[] spawners;      // Array of spawner GameObjects
    public Image background;            // Reference to the UI Image for background
    public List<Sprite> timeSprites;    // List of Sprites for Day, Dusk, and Night
      
    private float timer = 0f;          // Timer to keep track of the cycle
    public enum TimeOfDay { Day, Night, Dusk, Dawn }
    public TimeOfDay currentTimeOfDay; // Current state of the day/night cycle

    void Start()
    {
        currentTimeOfDay = TimeOfDay.Day; // Start with Day
        UpdateSlider(); // Initialize the slider to full
        UpdateSpawners(); // Ensure spawners are correctly set at the start
        UpdateBackgroundImage();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if the timer has exceeded the phase duration
        if (timer >= phaseDuration)
        {
            // Update the time of day
            switch (currentTimeOfDay)
            {
                case TimeOfDay.Day:
                    currentTimeOfDay = TimeOfDay.Night;
                    Debug.Log("Switching to Night");
                    break;
                case TimeOfDay.Night:
                    currentTimeOfDay = TimeOfDay.Dusk;
                    Debug.Log("Switching to Dusk");
                    break;
                case TimeOfDay.Dusk:
                    currentTimeOfDay = TimeOfDay.Day;
                    Debug.Log("Switching to Day");
                    break;
                case TimeOfDay.Dawn:
                    currentTimeOfDay = TimeOfDay.Dawn;
                    Debug.Log("Switching to Dawn");
                    break;
            }

            // Reset timer for the next phase
            timer = 0f;

            // Update spawners and background after changing the time of day
            UpdateSpawners();
            UpdateBackgroundImage();
        }

        UpdateSlider();
    }

    private void UpdateSpawners()
    {
        bool isNight = currentTimeOfDay == TimeOfDay.Night;

        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(isNight); // Activate spawners only during Night phase
            spawner.GetComponent<EnemySpawner>()?.SetActive(isNight); // Ensure the spawner resumes spawning if activated
        }

        // Debugging log to see the current state
        Debug.Log($"Current Time of Day: {currentTimeOfDay}. Spawners active: {isNight}");
    } 


    private void UpdateSlider()
    {
        // Update the slider based on the current timer and phase duration

        //rotates clock image as time counts down 
        float newValue = 1 - (timer/phaseDuration);
        //clockFace.transform.Rotate(0.0f, 0.0f, Mathf.Clamp01(newValue));
        if (timer <= phaseDuration)
        {

            clockFace.transform.Rotate(0.0f, 0.0f, Mathf.Clamp01(newValue));

        }
        

    }

    private void UpdateBackgroundImage()
    {
        // Update the background image based on the current time of day
        switch (currentTimeOfDay)
        {
            case TimeOfDay.Day:
                background.sprite = timeSprites[0]; // Day sprite
                break;
            case TimeOfDay.Night:
                background.sprite = timeSprites[1]; // Night sprite
                break;
            case TimeOfDay.Dusk:
                background.sprite = timeSprites[2]; // Dusk sprite
                break;
        }
    }
}
