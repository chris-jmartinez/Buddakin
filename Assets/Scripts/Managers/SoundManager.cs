using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance = null;

	[Header("Soundtrack Music")]
    public AudioSource m_backgroundMusicAudioSource;
	[Header("Sound Effects")]
    public AudioSource m_effectsAudioSource;
    public AudioClip m_coin;
	public AudioClip m_gunShot;
	public AudioClip m_ninjaAttack;
	public AudioClip m_laserHolyAudioclip;
	public AudioClip m_mibDiesAudioclip;
    public AudioClip m_pedrieuHit;
    public AudioClip m_zozzyPunch;
    public AudioClip m_bazookaBoom;
    public AudioClip m_pickup;

	public AudioClip laserGun;
	public AudioClip fireBall;
	public AudioClip gunFire;
	public AudioClip punch;
	public AudioClip sparkle;
	public AudioClip click;
	public AudioClip selection;

	private bool isMusicOn = true;
	private bool isSoundEffectsOn = true;

	void Awake () {
		if ( Instance == null )
			Instance = this;
		else if ( Instance != this )
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	void Start () {

	}

	public void ChangeClip(AudioClip clip) {
		if ( isMusicOn ) {
			m_backgroundMusicAudioSource.Stop ();
			m_backgroundMusicAudioSource.clip = clip;
			m_backgroundMusicAudioSource.Play ();
		}
	}

	public void CollectCoin() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_coin, 0.6f);
		}
	}

	public void CutTheirThroats() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_ninjaAttack, 0.5f);
		}
	}

	public void GunShot() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_gunShot, 0.7f);
		}
	}

	public void LaserHolyEnter() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.clip = m_laserHolyAudioclip;
			m_effectsAudioSource.Play ();
		}

	}

	public void LaserHolyExit() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.Stop ();
		}
	}

	public void mibDies() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_mibDiesAudioclip);
		}
	}

	public void LaserGun() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (laserGun, 0.7f);
		}
	}

	public void FireBall() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (fireBall, 0.5f);
		}	
	}

	public void GunFire() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (gunFire, 0.7f);
		}
	}

	public void Sparkle() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (sparkle, 0.7f);
		}
	}

	public void Click() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (click, 0.1f);
		}	
	}

	public void Punch() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (punch, 0.7f);
		}
	}

	public void Select() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (selection, 0.7f);
		}
	}

	public void pedrieuHit() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_pedrieuHit, 0.7f);
		}
	}

	public void zozzyPunch() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_zozzyPunch, 0.7f);
		}
	}


	public void bazookaBoom() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_bazookaBoom, 0.7f);
		}
	}

	public void pickup() {
		if ( isSoundEffectsOn ) {
			m_effectsAudioSource.PlayOneShot (m_pickup, 0.3f);
		}

	}

	public void RestartMusic() {
		if ( !m_backgroundMusicAudioSource.isPlaying ) {
			m_backgroundMusicAudioSource.UnPause ();
		}
	}

	public void PauseMusic() {
		if ( m_backgroundMusicAudioSource.isPlaying ) {
			m_backgroundMusicAudioSource.Pause ();
		}
	}

	public bool IsMusicOn {
		get { 
			return isMusicOn;
		} set { 
			isMusicOn = value;
		}
	}

	public bool IsSoundEffectsOn {
		get { 
			return isSoundEffectsOn;
		} set { 
			isSoundEffectsOn = value;
		}
	}

}