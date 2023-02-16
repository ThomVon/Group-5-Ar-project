using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FreezeScript : MonoBehaviour
{
    public GameObject stereo;

    void Start()
    {
        stereo = GetComponent<m_Rigidbody>();
    }

    void FreezePosition()
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
