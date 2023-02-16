using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class FreezeScript : MonoBehaviour
{

    public GameObject tracking;
    public GameObject frezbut;
    

    

   

    public void FreezePosition()
    {
        if (tracking.activeSelf == false)
        {
            tracking.SetActive(true);
            frezbut.SetActive(false);
        }
        else
        {
            tracking.SetActive(false);
            frezbut.SetActive(true);
        }

        
    }

    
}
