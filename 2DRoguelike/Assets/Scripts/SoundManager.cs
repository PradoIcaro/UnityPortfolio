
using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private  AudioSource m_efxSource;                    //Drag a reference to the audio source which will play the sound effects.
    [SerializeField] AudioSource m_musicSource;
    private const float LOW_PITCH_RANGE = .95f;                //The lowest a sound effect will be randomly pitched.
    private const float HIGH_PITCH_RANGE = 1.05f;            //The highest a sound effect will be randomly pitched.

    public static SoundManager Instance  = null;
    public AudioSource GetMusicSource => m_musicSource;
    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (Instance == null)
            //if not, set it to this.
            Instance = this;
        //If instance already exists:
        else if (Instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        m_efxSource.clip = clip;

        //Play the clip.
        m_efxSource.Play();
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(LOW_PITCH_RANGE, HIGH_PITCH_RANGE);

        //Set the pitch of the audio source to the randomly chosen pitch.
        m_efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        m_efxSource.clip = clips[randomIndex];

        //Play the clip.
        m_efxSource.Play();
    }
}