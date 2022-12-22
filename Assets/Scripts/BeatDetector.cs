using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    public AudioSource audioSource;
    public float beatThreshold = 0.2f;

    private float[] samples;
    private float previousVolume;

    void Start()
    {
        // Initialize the samples array
        samples = new float[audioSource.clip.samples];
        audioSource.clip.GetData(samples, 0);
    }

    void Update()
    {
        // Get the current volume of the audio
        float currentVolume = GetVolume();

        // If the current volume is greater than the beat threshold
        // and the previous volume was below the beat threshold,
        // a beat has been detected
        if (currentVolume > beatThreshold && previousVolume <= beatThreshold)
        {
            // Do something in response to the beat (e.g. trigger an animation or action)
            Debug.Log("Beat detected!");
        }

        // Update the previous volume
        previousVolume = currentVolume;
    }

    // Returns the current volume of the audio
    private float GetVolume()
    {
        // Get the current position of the audio playback
        int currentSample = (int)(audioSource.time * audioSource.clip.frequency);

        // Calculate the average volume of the audio over the previous 0.1 seconds
        float sum = 0;
        for (int i = currentSample - (int)(audioSource.clip.frequency * 0.1f); i < currentSample; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        return sum / (audioSource.clip.frequency * 0.1f);
    }
}
