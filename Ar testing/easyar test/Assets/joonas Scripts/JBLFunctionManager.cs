using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JBLFunctionManager : MonoBehaviour
{
    public GameObject OnSoundManager;
    public GameObject OffSoundManager;
    public GameObject PairingButton;
    public GameObject PlayPauseButton;

    public GameObject BluetoothIcon;
    public GameObject OnOffIcon;
    public GameObject PairingIcon;
    public GameObject PlayPauseIcon;
    public GameObject VolumeDownIcon;
    public GameObject VolumeUpIcon;

    public Slider volumeSlider;

    AudioSource OnAudio;
    AudioSource OffAudio;
    AudioSource PairingAudio;
    AudioSource PlayPauseAudio;

    public bool JBL_DevicePowerON = false;
    public bool JBL_BluetoothActivated = false;
    public bool JBL_PairingActivated = false;
    public bool JBL_SongActivated = false;
    public bool SongIsPlaying = false;
    public bool SongPaused = false;
    public bool VolumeAdjustingInProgress = false;
    // To prevent over clicking
    public bool JBL_ProcessingActive = false;
    public bool JBL_BluetoothProcessingActive = false;

    // Start is called before the first frame update
    void Start()
    {
        OnAudio = OnSoundManager.GetComponent<AudioSource>();
        OffAudio = OffSoundManager.GetComponent<AudioSource>();
        PairingAudio = PairingButton.GetComponent<AudioSource>();
        PlayPauseAudio = PlayPauseButton.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If JBL is powered ON
        if (OnOffIcon.activeSelf == true && JBL_DevicePowerON == false && JBL_ProcessingActive == false)
        {
            JBL_ProcessingActive = true;
            StartCoroutine(JBLPoweredOn());
        }

        // If JBL is powered OFF
        if (OnOffIcon.activeSelf == true && JBL_DevicePowerON == true && JBL_ProcessingActive == false)
        {
            JBL_ProcessingActive = true;
            StartCoroutine(JBLPoweredOff());
        }

        // Bluetooth button touched
        if (BluetoothIcon.activeSelf == true && JBL_DevicePowerON == true && JBL_BluetoothProcessingActive == false)
        {

            JBL_BluetoothProcessingActive = true;

            if (JBL_BluetoothActivated == true)
            {
                JBL_BluetoothActivated = false;
            }

            if (JBL_BluetoothActivated == false)
            {
                JBL_BluetoothActivated = true;
            }

            JBL_BluetoothProcessingActive = false;
        }

        // If pairing is activated
        if (PairingIcon.activeSelf == true && JBL_BluetoothActivated == true && JBL_PairingActivated == false && JBL_ProcessingActive == false)
        {
            JBL_ProcessingActive = true;
            StartCoroutine(JBLPairingActive()); 
        }

        // if playbutton is activated
        if (PlayPauseIcon.activeSelf == true && JBL_DevicePowerON == true && JBL_BluetoothActivated == true && JBL_PairingActivated == true && JBL_ProcessingActive == false)
        {
            JBL_ProcessingActive = true;
            StartCoroutine(JBLSongPlaying());
        }

        // if playbutton is disabled
        if (PlayPauseIcon.activeSelf == false && JBL_DevicePowerON == true && JBL_BluetoothActivated == true && JBL_PairingActivated == true && SongIsPlaying == true)
        {
            JBL_ProcessingActive = true;
            StartCoroutine(JBLSongPause());
        }

        // if VolumeDown button is pressed
        if (VolumeDownIcon.activeSelf == true && JBL_DevicePowerON == true && VolumeAdjustingInProgress == false)
        {
            VolumeAdjustingInProgress = true;
            StartCoroutine(JBL_LowerVolume());
        }

        // if VolumeUp button is pressed
        if (VolumeUpIcon.activeSelf == true && JBL_DevicePowerON == true && VolumeAdjustingInProgress == false)
        {
            VolumeAdjustingInProgress = true;
            StartCoroutine(JBL_IncreaseVolume());
        }
    }

    IEnumerator JBLPoweredOn()
    {
        OnAudio.Play(0);
        yield return new WaitForSeconds(1.75f);
        JBL_DevicePowerON = true;
        JBL_ProcessingActive = false;
    }

    IEnumerator JBLPoweredOff()
    {
        // Song starts from zero next time device is activated
        if (SongIsPlaying == true)
        {
            JBL_SongActivated = false;
            SongIsPlaying = false;
            SongPaused = false;
            PlayPauseAudio.Stop();
        }

        OffAudio.Play(0);
        yield return new WaitForSeconds(1.75f);
        JBL_DevicePowerON = false;
        JBL_BluetoothActivated = false;
        JBL_PairingActivated = false;
        JBL_ProcessingActive = false;
        JBL_SongActivated = false;
        SongIsPlaying = false;
        SongPaused = false;


    }

    IEnumerator JBLPairingActive()
    {
        PairingAudio.Play(0);
        yield return new WaitForSeconds(3.4f);
        JBL_PairingActivated = true;
        JBL_ProcessingActive = false;
    }

    IEnumerator JBLSongPlaying()
    {
        // UnPause music if song is already activated
        if (SongPaused == true)
        {
            PlayPauseAudio.UnPause();
            SongPaused = false;
        }
        else
        { 
            // starts playing song if not activated yet
            if (JBL_SongActivated == false)
                {
                    JBL_SongActivated = true;
                    SongIsPlaying = true;
                    PlayPauseAudio.Play(0);
                }

        }

        yield return new WaitForSeconds(1f);
        JBL_ProcessingActive = false;
    }

    IEnumerator JBLSongPause()
    {
        // Pauses song if song is playing
        if (SongIsPlaying == true)
        {

            if (SongPaused == false)
            {
                PlayPauseAudio.Pause();
                SongPaused = true;
            }
        }

        yield return new WaitForSeconds(1f);
        JBL_ProcessingActive = false;
    }

    IEnumerator JBL_LowerVolume()
    {
        // Lower volume if above 0, otherwise ignore the button press
        if (volumeSlider.value >= 0)
        {
            volumeSlider.value = (volumeSlider.value - 0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        VolumeAdjustingInProgress = false;
    }

    IEnumerator JBL_IncreaseVolume()
    {
        // Increase volume if below 1, otherwise ignore the button press
        if (volumeSlider.value <= 1)
        {
            volumeSlider.value = (volumeSlider.value + 0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        VolumeAdjustingInProgress = false;
    }
}
