using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	[SerializeField]
	private IconToggle musicIconToggle;
	public IconToggle MusicIconToggle {
		get {
			return musicIconToggle;
		}
	}

	[SerializeField]
	private IconToggle sfxIconToggle;
	public IconToggle SfxIconToggle {
		get {
			return sfxIconToggle;
		}
	}

	[SerializeField]
	private AudioSource backgroundMusicSource;
	public AudioSource BackgroundMusicSource {
		get {
			return backgroundMusicSource;
		}
	}

	[SerializeField]
	private AudioClip gameOverSound;
	public AudioClip GameOverSound {
		get {
			return gameOverSound;
		}
	}

	[Range(0, 1)]
	private float musicVolume = 1.0f;
	public float MusicVolume {
		get {
			return musicVolume;
		}
	}

	[Range(0, 1)]
	private float sfxVolume = 1.0f;
	public float SfxVolume {
		get {
			return sfxVolume;
		}
	}

	private bool isMusicEnabled = true;
	public bool IsMusicEnabled {
		get {
			return isMusicEnabled;
		}
	}

	private bool isSfxEnabled = true;
	public bool IsSfxEnabled {
		get {
			return isSfxEnabled;
		}
	}

	[SerializeField]
	private AudioClip[] musicClips;
	private AudioClip backgroundMusic;
	private AudioClip randomMusicClip;

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			// DontDestroyOnLoad(this.gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	private void Start () {
		backgroundMusic = GetRandomClip(musicClips);
		PlayBackgroundMusic(backgroundMusic);
	}

	public void ToggleMusic () {
		isMusicEnabled = !isMusicEnabled;
		if (musicIconToggle) {
			musicIconToggle.ToggleIcon(isMusicEnabled);
		}
		UpdateMusic();
	}

	public void ToggleSfx () {
		isSfxEnabled = !isSfxEnabled;
		if (sfxIconToggle) {
			sfxIconToggle.ToggleIcon(isSfxEnabled);
		}
	}

	public AudioClip GetRandomClip (AudioClip[] clips) {
		AudioClip randomClip = clips[Random.Range(0, clips.Length)];
		return randomClip;
	}

	private void PlayBackgroundMusic (AudioClip musicClip) {
		if (!isMusicEnabled || !musicClip || !backgroundMusicSource) {
			return;
		}

		backgroundMusicSource.Stop();
		backgroundMusicSource.clip = musicClip;
		backgroundMusicSource.volume = musicVolume;
		backgroundMusicSource.loop = true;
		backgroundMusicSource.Play();
	}

	private void UpdateMusic () {
		if (backgroundMusicSource.isPlaying != isMusicEnabled) {
			if (isMusicEnabled) {
				PlayBackgroundMusic(backgroundMusic);
			} else {
				backgroundMusicSource.Stop();
			}
		}
	}

	public IEnumerator ReduceMusicVolumeOverTime (float time)
	{
		float t = 0;
		while (t < time)
		{
			t += Time.deltaTime;
			backgroundMusicSource.volume = Mathf.Lerp (1, 0, t / time);
			yield return null;
		}
	}
}