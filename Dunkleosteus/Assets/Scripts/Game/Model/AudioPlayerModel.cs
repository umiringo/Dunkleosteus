using UnityEngine;
using System.Collections;

public class AudioPlayerModel : MonoBehaviour {
    public AudioClip opBgm;
    public AudioClip selectBgm;
    public AudioClip gameBgm;
    public AudioClip clickSound;
    public AudioClip choseSound;
    public AudioClip unchoseSound;
    public AudioClip winSound;
    public AudioClip flopCardSound;

    private bool isPlayMusic;
    private bool isPlaySound;

    private AudioSource audioSource;

    void Awake() {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 1.0f;
    } 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   if(!isPlayMusic) {
            audioSource.Pause();
       }
	}

    public void SetIsPlayMusic(bool value) 
    {
        isPlayMusic = value;
    }

    public void SetIsPlaySound(bool value)
    {
        isPlaySound = value;
    }

    public void PlayOpBGM()
    {
        audioSource.Pause();
        audioSource.clip = opBgm;
        if(isPlayMusic) {
            audioSource.Play();
        }
    }

    public void PlaySelectBGM()
    {
        audioSource.Pause();
        audioSource.clip = selectBgm;
        if(isPlayMusic) {
            audioSource.Play();
        } 
    }

    public void PlayGameBGM()
    {
        audioSource.Pause();
        audioSource.clip = gameBgm;
        if(isPlayMusic) {
            audioSource.Play();          
        }
    }

    public void PlayClickSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayChoseSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(choseSound);
    }

    public void PlayUnChoseSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(unchoseSound);
    }

    public void PlayWinSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(winSound);
    }

    public void PlayFlopCardSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(flopCardSound);
    }
}
