using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance
    public AudioSource musicSource; // For background music
    public AudioSource SFXSource; // For sound effects

    [Header(" ------ Audio Clips --------")]
    public AudioClip backgroundMusic; // Background music
    public AudioClip coinSound; // Coin collection sound
    public AudioClip deathSound;
    
     // Death sound for enemies

    private void Awake()
    {
        // Implement the singleton pattern to ensure one instance of AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the AudioManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy if there's another instance
        }
    }

    private void Start()
    {
        // Play background music
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; // Loop the music
            musicSource.Play();
        }
    }

    // Play a sound effect by providing an AudioClip
    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null && clip != null)
        {
            SFXSource.PlayOneShot(clip); // Play the provided sound effect
        }
    }

    // Play a sound effect by name (optional)
    public void PlaySFX(string soundName)
    {
        AudioClip clipToPlay = null;

        switch (soundName)
        {
            case "coin":
                clipToPlay = coinSound;
                break;
            case "death":
                clipToPlay = deathSound;
                break;
            // Add more cases for other sounds as needed
            default:
                Debug.LogWarning("Sound name not recognized: " + soundName);
                break;
        }

        if (clipToPlay != null)
        {
            PlaySFX(clipToPlay); // Play the appropriate sound
        }
    }
}