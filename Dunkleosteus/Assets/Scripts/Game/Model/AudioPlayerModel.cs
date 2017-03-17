using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;

public class AudioPlayerModel : MonoBehaviour {
    public AudioClip opBgm;
    public AudioClip selectBgm;
    public AudioClip gameBgm;
    public AudioClip clickSound;
    public AudioClip choseSound;
    public AudioClip unchoseSound;
    public AudioClip winSound;
    public AudioClip flopCardSound;
    public AudioClip switchSound;
    public AudioClip purchaseSound;

    public AudioClip p1;
    public AudioClip p2;
    public AudioClip p3;
    public AudioClip p4;
    public AudioClip p5;
    public AudioClip p6;
    public AudioClip p7;

    private int littleStarIndex = 0; 
    private bool isPlayMusic = true;
    private bool isPlaySound = true;
    private List<AudioClip> littleStar = new List<AudioClip>();

    private AudioSource audioSource;

    void Awake() {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        InitLittleStar();
    } 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void InitLittleStar()
    {
        littleStar.Add(p1);
        littleStar.Add(p1);
        littleStar.Add(p5);
        littleStar.Add(p5);
        littleStar.Add(p6);
        littleStar.Add(p6);
        littleStar.Add(p5);

        littleStar.Add(p4);
        littleStar.Add(p4);
        littleStar.Add(p3);
        littleStar.Add(p3);
        littleStar.Add(p2);
        littleStar.Add(p2);
        littleStar.Add(p1);

        littleStar.Add(p5);
        littleStar.Add(p5);
        littleStar.Add(p4);
        littleStar.Add(p4);
        littleStar.Add(p3);
        littleStar.Add(p3);
        littleStar.Add(p2);

        littleStar.Add(p5);
        littleStar.Add(p5);
        littleStar.Add(p4);
        littleStar.Add(p4);
        littleStar.Add(p3);
        littleStar.Add(p3);
        littleStar.Add(p2);
    }

    public void SetIsPlayMusic(bool value) 
    {
        isPlayMusic = value;
        if (value) {
            audioSource.Play();
        }
        else {
            audioSource.Stop();
        }
    }

    public void SetIsPlaySound(bool value)
    {
        isPlaySound = value;
    }

    public void PlayOpBGM()
    {
        //audioSource.Pause();
        audioSource.clip = opBgm;
        if(isPlayMusic) {
            audioSource.Play();
        }
    }

    public void PlaySelectBGM()
    {
       // audioSource.Pause();
        
        if(isPlayMusic) {
            if(audioSource.clip.name != "select_bgm" || !audioSource.isPlaying) {
                audioSource.clip = selectBgm;
                audioSource.Play();
            }
        } 
    }

    public void PlayGameBGM()
    {
        //audioSource.Pause();
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

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayLittleStar()
    {
        if (!isPlaySound) return;
        audioSource.PlayOneShot(littleStar[littleStarIndex]);
        littleStarIndex++;
        if (littleStarIndex >= littleStar.Count) {
            littleStarIndex = 0;
        }
    }

    public void ClearLittleStarIndex()
    {
        littleStarIndex = 0;
    }

    public void PlaySwitchSound()
    {
        if (!isPlaySound) return;
        audioSource.PlayOneShot(switchSound);
    }

    public void PlayPurchaseSound()
    {
        if(!isPlaySound) return;
        audioSource.PlayOneShot(purchaseSound);
    }
}
