using UnityEngine;

namespace Develop.Runtime.Services.AudioSystem
{
    public class AudioSystem: MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlaySound(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }
}