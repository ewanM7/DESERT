using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    public GameObject oneShotPrefab;


    public void setMusicVol(float vol, float masterVol)
    {
        if (vol * masterVol != 0)
        {
            masterMixer.SetFloat("musicVol", Mathf.Log10(vol * masterVol) * 20);
        }
        else
        {
            masterMixer.SetFloat("musicVol", -80f);
        }
    }

    public void setMasterVol(float vol)
    {
        if (vol != 0)
        {
            masterMixer.SetFloat("masterVol", Mathf.Log10(vol) * 20);
        }
        else
        {
            masterMixer.SetFloat("masterVol", -80f);
        }
    }

    /// <summary>
    /// Fades out the master mixer
    /// </summary>
    /// <param name="duration">duration of the fade</param>
    /// <returns></returns>
    public IEnumerator fadeOut(float duration)
    {
        float fadeOutTime = 0f;
        float startVol = GameManager.Instance._SettingsManager.settings.masterVol;

        while (fadeOutTime < duration)
        {
            float vol = startVol - ((fadeOutTime / duration) * startVol);
            masterMixer.SetFloat("masterVol", Mathf.Log10(vol) * 20);
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        masterMixer.SetFloat("masterVol", -80f);
        yield return null;
    }

    /// <summary>
    /// Fades in the master mixer
    /// </summary>
    /// <param name="duration">duration of the fade</param>
    /// <returns></returns>
    public IEnumerator fadeIn(float duration)
    {
        masterMixer.SetFloat("masterVol", -80f);
        float fadeOutTime = 0f;

        while (fadeOutTime < duration)
        {
            float vol = (fadeOutTime / duration) * GameManager.Instance._SettingsManager.settings.masterVol;
            masterMixer.SetFloat("masterVol", Mathf.Log10(vol) * 20);
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        masterMixer.SetFloat("masterVol", Mathf.Log10(GameManager.Instance._SettingsManager.settings.masterVol) * 20);
    }

    /// <summary>
    /// Fades out the music mixer
    /// </summary>
    /// <param name="duration">duration of the fade</param>
    /// <returns></returns>
    public IEnumerator fadeOutMusic(float duration)
    {
        float fadeOutTime = 0f;
        float startVol = GameManager.Instance._SettingsManager.settings.masterVol;

        while (fadeOutTime < duration)
        {
            float vol = startVol - ((fadeOutTime / duration) * startVol);
            masterMixer.SetFloat("musicVol", Mathf.Log10(vol) * 20);
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        masterMixer.SetFloat("musicVol", -80f);
        yield return null;
    }

    /// <summary>
    /// Fades in the music mixer
    /// </summary>
    /// <param name="duration">duration of the fade</param>
    /// <returns></returns>
    public IEnumerator fadeInMusic(float duration)
    {
        masterMixer.SetFloat("musicVol", -80f);
        float fadeOutTime = 0f;

        while (fadeOutTime < duration)
        {
            float vol = (fadeOutTime / duration) * GameManager.Instance._SettingsManager.settings.masterVol;
            masterMixer.SetFloat("musicVol", Mathf.Log10(vol) * 20);
            fadeOutTime += Time.deltaTime;
            yield return null;
        }

        masterMixer.SetFloat("musicVol", Mathf.Log10(GameManager.Instance._SettingsManager.settings.masterVol) * 20);
    }

    /// <summary>
    /// Play a non-spatial oneshot sound
    /// </summary>
    public void playOneshotSound(AudioClip soundClip, float volume, bool randomPitch, float minRandomPitch = 0.8f, float maxRandomPitch = 1.2f)
    {
        OneShotSound sound = Instantiate(oneShotPrefab, Vector3.zero, Quaternion.identity).GetComponent<OneShotSound>();

        if (randomPitch)
        {
            sound.audioSource.pitch = Random.Range(minRandomPitch, maxRandomPitch);
        }
        else
        {
            sound.audioSource.pitch = 1f;
        }

        sound.audioSource.clip = soundClip;
        sound.audioSource.spatialBlend = 0f;
        sound.audioSource.volume = volume;
        sound.audioSource.Play();
    }
}
