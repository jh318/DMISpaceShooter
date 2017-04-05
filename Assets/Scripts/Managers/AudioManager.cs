using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioClip music;
	private int currentMusicSource = 0;
	private AudioSource[] musicSources;

	public AudioClip[] clips;

	public int sfxSourcesCount = 5;
	private int currentSFXSource = 0;
	private AudioSource[] sfxSources;

	void Awake(){
		if (instance == null)
			instance = this;
	}

	void Start(){
		sfxSources = new AudioSource[sfxSourcesCount];
		for (int i = 0; i < sfxSourcesCount; i++) {
			GameObject g = new GameObject ("SFXSource" + i);
			g.transform.parent = transform;
			AudioSource s = g.AddComponent<AudioSource> ();
			sfxSources [i] = s;
		}

		musicSources = new AudioSource[2];
		for (int i = 0; i < 2; i++) {
			GameObject g = new GameObject ("MusicSource" + i);
			g.transform.parent = transform;
			AudioSource s = g.AddComponent<AudioSource> ();
			s.loop = true;
			musicSources [i] = s;
		}
		musicSources [currentMusicSource].clip = music;
		musicSources [currentMusicSource].Play ();

		PlayMusic ();
	}

	public static void PlayVariedEffect(string clipName, float variation = 0.1f){
		PlayEffect(
			clipName,
			Random.Range(1-variation, 1 + variation),
			Random.Range(1 - variation, 1 + variation)
			);
	}

	public static void PlayEffect(string clipName, float pitch = 1, float volume = 1){
		AudioClip clip = null;
		for(int i = 0; i < instance.clips.Length; i++){
			if(instance.clips[i].name == clipName) clip = instance.clips[i];
		}

		if (instance.clips == null) return;

		AudioSource source = instance.sfxSources [instance.currentSFXSource];
		instance.currentSFXSource = (instance.currentSFXSource + 1) % instance.sfxSourcesCount;

		source.clip = clip;
		source.pitch = pitch;
		source.volume = volume;
		source.Play ();
	}

	public static void PlayMusic(){
		instance.musicSources [instance.currentMusicSource].clip = instance.music;
		instance.musicSources [instance.currentMusicSource].Play ();

	}

	public static void CrossfadeMusic(AudioClip clip, float duration){
		AudioSource nextSource = instance.musicSources [(instance.currentMusicSource + 1) % 2];
		nextSource.clip = clip;
		instance.StartCoroutine ("CrossfadeMusicCoroutine", duration);
	}

	IEnumerator CrossfadeMusicCoroutine (float duration){
		AudioSource a = musicSources [currentMusicSource];
		currentMusicSource = (currentMusicSource + 1) % 2;
		AudioSource b = musicSources [currentMusicSource];
		b.volume = 0;
		b.Play ();

		for (float t = 0; t < duration; t += Time.deltaTime) {
			float frac = t / duration;
			a.volume = 1 - frac;
			b.volume = frac;
			yield return new WaitForEndOfFrame();
			}

		a.volume = 0;
		a.Stop ();
		b.volume = 1;
	}
}