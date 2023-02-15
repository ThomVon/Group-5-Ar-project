using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControll : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;
        var touch = Input.GetTouch(index: 0);

        if (touch.phase == TouchPhase.Began)
        {
            var ray = Camera.main.ScreenPointToRay((Vector3)touch.position);
            Physics.Raycast(ray, out var raycastHit, 100f);
            var collider = raycastHit.collider;
            if (collider != null)
            {
                var cube = collider.GetComponent<InteractableButton>();
                if (cube != null)
                {
                    cube.Interact();
                }
            }
        }
    }
}
