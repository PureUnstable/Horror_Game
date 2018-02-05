using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music};

    public float masterVolumePercent { get; private set; }
    public float musicVolumePercent  { get; private set; }
	public float sfxVolumePercent  { get; private set; }

    AudioSource[]  musicSources;
    int activeMusicSourceIndex;

    public static AudioManager instance;

    void Awake()
    {
        instance = this;

        musicSources = new AudioSource[3];
        for(int i = 0; i < 3; i++)
        {
            GameObject newMusicSource = new GameObject("Music Source" + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

		masterVolumePercent = 1;
		PlayerPrefs.SetFloat("master vol", 1);
		musicVolumePercent = 1;
		PlayerPrefs.SetFloat("music vol", 1);
		sfxVolumePercent = 1;
		PlayerPrefs.SetFloat("sfx vol", 1);
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = musicVolumePercent = masterVolumePercent;
        musicSources[1].volume = musicVolumePercent = masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);

    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

		StartCoroutine("AnimateMusicCrossfade");
    }
    
    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent = masterVolumePercent);

        }
    }

    IEnumerator AnimateMusicCrossfade(/*float duration*/)
    {
		float duration = 1;
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime + 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent = masterVolumePercent, percent);
            musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent = masterVolumePercent, 0, percent);
			yield return null;

        }


    }
}
