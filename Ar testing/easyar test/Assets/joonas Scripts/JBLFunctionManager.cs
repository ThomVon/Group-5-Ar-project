using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    AudioSource OnAudio;
    AudioSource OffAudio;
    AudioSource PairingAudio;
    AudioSource PlayPauseAudio;

    public bool JBL_DevicePowerON = false;
    public bool JBL_BluetoothActivated = false;
    public bool JBL_PairingActivated = false;
    public bool JBL_SongActivated = false;
    public bool SongIsPlaying = false;
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
        // starts playing song if not activated yet
        if (JBL_SongActivated == false)
        {
            JBL_SongActivated = true;
            SongIsPlaying = true;
            PlayPauseAudio.Play(0);
        }

        else
        {
            // Pause music
            if (SongIsPlaying == true)
            {
                PlayPauseAudio.Pause();
                SongIsPlaying = false;
            }
            // Unpause music
            else
            {
                PlayPauseAudio.UnPause();
                SongIsPlaying = true;
            }
        }
        yield return new WaitForSeconds(1f);
        JBL_ProcessingActive = false;
    }
}
