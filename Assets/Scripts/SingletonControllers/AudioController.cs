using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Singleton

    public static AudioController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public AudioSource audioSource;

    public AudioClip characterCreationMusic;
    public AudioClip buttonPressSound;
    public AudioClip spongePickedUp;
    public AudioClip impossibleChoice;

    private bool isMusicPlaying;

   /* 
   //Could be expanded with reading from dataStorage
   public float Volume 
    { 
        get 
        { 
            return audioSource.volume; 
        } 
        set 
        { 
            audioSource.volume = value;
            DataStorageManager.Instance.Volume = value;
        } 
    }
*/
    private void Start()
    {
        //Could be expanded with reading from dataStorage
    }

    public void PlayCharacterCreationMusic()
    {
        if (!isMusicPlaying)
        {
            audioSource.clip = characterCreationMusic;
            audioSource.Play();
            isMusicPlaying = true;
            Debug.Log("Started music");
        }
        
    }

    public void PlayButtonPressedSound()
    {
        Debug.Log("Play Audio");
        audioSource.PlayOneShot(buttonPressSound);
    }

    public void PlaySpongePickedUp()
    {
        Debug.Log("Play Audio");
        audioSource.PlayOneShot(spongePickedUp);
    }

    public void PlayImpossibleChoice()
    {
        audioSource.PlayOneShot(impossibleChoice);
    }
}
