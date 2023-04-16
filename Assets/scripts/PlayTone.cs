using UnityEngine;

public class PlayTone : MonoBehaviour
{
    public float baseFrequency = 440; // the base frequency of the tones
    public float minValue = 0; // the minimum value of the range
    public float maxValue = 3; // the maximum value of the range
    public JumpController jumpController; // the JumpController component to get holdTime from

    private AudioSource source; // the AudioSource component
    private float pitchOffset; // the current pitch offset based on holdTime
    private float frequency; // the current frequency based on pitchOffset

    void Start()
    {
        // get the AudioSource component
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        // map the range of values to the range of pitch offsets
        float minPitchOffset = 0;
        float maxPitchOffset = Mathf.Log(maxValue / minValue, 2);

        // generate a tone based on the pitch offset controlled by holdTime
        pitchOffset = Mathf.Lerp(minPitchOffset, maxPitchOffset, jumpController.holdTime / maxValue);
        frequency = baseFrequency * Mathf.Pow(2, pitchOffset);

        // create and play the audio clip using the modified OnAudioRead method
        AudioClip clip = AudioClip.Create("tone", 44100, 1, 44100, false, (data) => OnAudioRead(frequency, data));
        source.clip = clip;
        source.Play();
    }

    // this method is called by the AudioClip to generate the tone
    bool OnAudioRead(float frequency, float[] data)
    {
        float increment = frequency * 2 * Mathf.PI / 44100;
        float phase = 0;

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(phase);
            phase += increment;
        }

        return true;
    }
}
