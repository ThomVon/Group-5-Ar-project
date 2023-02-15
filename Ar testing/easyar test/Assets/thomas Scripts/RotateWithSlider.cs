using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateWithSlider : MonoBehaviour
{
     // Assign in the inspector
     public GameObject objectToRotate;
     public Slider slider;
 
     // Preserve the original and current orientation
     private float previousValue;
 
     void Awake ()
     {
         // Assign a callback for when t slider changes
         slider.onValueChanged.AddListener (OnSliderChanged);
 
         // And current value
         previousValue = slider.value;
     }
 
     void OnSliderChanged (float value)
     {
         // How much we've changed
         float delta = value - previousValue;
         objectToRotate.transform.Rotate (Vector3.forward * delta * 360);
 
         // Set our previous value for the next change
         previousValue = value;
     }
 }
