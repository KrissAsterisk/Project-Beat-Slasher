using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BeatDetection : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public float beatThreshold = 0.1f; // Adjust this threshold based on your audio and desired sensitivity

    private float[] spectrumData = new float[1024]; // Array to store audio spectrum data
    private float prevAmplitude = 0f;
    private float currentAmplitude = 0f;
    private bool beatDetected = false;
    public float CurrentAmplitude { get { return currentAmplitude; } }
    public float PreviousAmplitude { get { return prevAmplitude; } }

    private void Start()
    {
        // Start playing the music here
        audioSource.Play();
    }

    private void Update()
    {
        // Get the spectrum data from the audio source
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Calculate the current amplitude of the music
        float sum = 0f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            sum += spectrumData[i];
        }
        prevAmplitude = currentAmplitude;
        currentAmplitude = sum / spectrumData.Length;

        // Check for a beat using the onset detection algorithm
        if (prevAmplitude <= beatThreshold && currentAmplitude > beatThreshold)
        {
            beatDetected = true;
            Debug.Log("Beat Detected!");
        }
        else
        {
            beatDetected = false;
        }
    }

    public bool IsBeatDetected()
    {
        return beatDetected;
    }
}