using DG.Tweening;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class SoundLauncher : MonoBehaviour
{
    static public SoundLauncher instance;
    [SerializeField] SoundData soundData;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource turretSource;
    [SerializeField] AudioSource bouttonSource;
    [SerializeField] AudioSource dissolveSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AudioListener.volume = 0.5f;
    }

    public void VolumeSlider(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    public void PlayClickButton()
    {
        if (soundData.clicksBoutton.Length > 0)
        {
            AudioClip clip = soundData.clicksBoutton[Random.Range(0, soundData.clicksBoutton.Length)];
            bouttonSource.PlayOneShot(clip);
        }
    }

    public void PlayClickButtonFail()
    {
        if (soundData.clickBouttonFail.Length > 0)
        {
            AudioClip clip = soundData.clickBouttonFail[Random.Range(0, soundData.clickBouttonFail.Length)];
            bouttonSource.PlayOneShot(clip);
        }
    }

    public void PlayPickUpItem()
    {
        if (soundData.pickUpItem.Length > 0)
        {
            AudioClip clip = soundData.pickUpItem[Random.Range(0, soundData.pickUpItem.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayAltarPowerOn()
    {
        if (soundData.altarPowerOn.Length > 0)
        {
            AudioClip clip = soundData.altarPowerOn[Random.Range(0, soundData.altarPowerOn.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayAltarPowerOff()
    {
        if (soundData.altarPowerOff.Length > 0)
        {
            AudioClip clip = soundData.altarPowerOff[Random.Range(0, soundData.altarPowerOff.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayRecepteurPowerOn()
    {
        if (soundData.recepteurPowerOn.Length > 0)
        {
            AudioClip clip = soundData.recepteurPowerOn[Random.Range(0, soundData.recepteurPowerOn.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayRecepteurPowerOff()
    {
        if (soundData.recepteurPowerOff.Length > 0)
        {
            AudioClip clip = soundData.recepteurPowerOff[Random.Range(0, soundData.recepteurPowerOff.Length)];
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayStructureMove(AudioSource source)
    {
        if (soundData.structureMove != null)
        {
            source.clip = soundData.structureMove;
            source.Play();
        }
    }


    public void PlayTurrentMove()
    {
        if (soundData.turrentMove != null)
        {
            AudioClip clip = soundData.turrentMove;
            turretSource.PlayOneShot(clip);
        }
    }

    public void PlayDissolve()
    {
        if (soundData.dissolve != null)
        {
            AudioClip clip = soundData.dissolve;
            dissolveSource.PlayOneShot(clip);
        }
    }





}
