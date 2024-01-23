using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _soundEffectSource;

	[SerializeField, Tooltip("Prefab object for spatial sound.")]
	private GameObject _worldEffectSource;

	private List<AudioSource> _audioCache = new List<AudioSource>();

	private static SoundManager _instance;

	public static SoundManager Instance
	{
		get
		{
			if (!_instance)
				_instance = FindObjectOfType<SoundManager>();

			if (!_instance)
			{
				Debug.Log("No SoundManager found in scene. Creating one.");
				_instance = new GameObject("SoundManager").AddComponent<SoundManager>();
			}

			return _instance;
		}
	}

	private void Update()
	{
		for (int i = 0; i < _audioCache.Count; i++)
		{
			AudioSource audioSource = _audioCache[i];
			if (!audioSource.isPlaying)
				ObjectPoolBehaviour.Instance.ReturnObject(audioSource.gameObject);
		}
	}

	/// <summary>
	/// Plays a sound globally.
	/// </summary>
	/// <param name="sound">The sound to play.</param>
	/// <param name="volume">Optional volume parameter.</param>
	public void PlaySound(AudioClip sound, float volume = 1)
	{
		_soundEffectSource.PlayOneShot(sound, volume);
	}

	/// <summary>
	/// Plays a sound locally at a given position by spawning a _worldEffectSource instance.
	/// </summary>
	/// <param name="position">Position to spawn the source at.</param>
	/// <param name="sound">The sound to play.</param>
	/// <param name="volume">Optional volume parameter.</param>
	public void PlaySoundAtPosition(Vector3 position, AudioClip sound, float volume = 1, float minDistance = 1, float maxDistance = 500)
	{
		GameObject obj = ObjectPoolBehaviour.Instance.GetObject(_worldEffectSource, position, Quaternion.identity);

		AudioSource source = obj.GetComponent<AudioSource>();

		if (!source)
			return;
		source.minDistance = minDistance;
		source.maxDistance = maxDistance;
		source.PlayOneShot(sound, volume);

		_audioCache.Add(source);
	}
}
