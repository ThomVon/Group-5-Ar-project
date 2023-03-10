using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    private int _interactionCount;
    public GameObject popup;
    public GameObject JBL_HighPoly;
    AudioSource JBLaudio;

    public void Interact()
    {
        // Button pressed
        if (_interactionCount == 0)
        {
            JBLaudio = JBL_HighPoly.GetComponent<AudioSource>();
            JBLaudio.Play(0);
            popup.SetActive(true);
            ChangeColour(Color.cyan);
            
        }
        else if (_interactionCount == 1)
        {
            popup.SetActive(false);
            ChangeColour(Color.white);
            
        }
        else
        {
            _interactionCount = -1;
        }
        _interactionCount++;
    }

    private void ChangeColour(Color colour)
    {
        var material = GetComponent<MeshRenderer>().material;
        material.color = colour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
