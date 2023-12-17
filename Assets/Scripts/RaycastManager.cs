using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public string targetTag = "Interactive";
    public Transform thirdObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // here we actually shoot the ray, starting from our current position, going forward unto infinity
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
        {
            Debug.Log("Hit detected! Hit object name: " + hit.transform.gameObject.name);

            // we access the gameobject that we've hit
            GameObject objectHit = hit.transform.gameObject;

            // and we scale it up
            if (objectHit.CompareTag(targetTag))
            {
                Debug.Log("Object with tag " + targetTag + " hit!");
                thirdObject.position = new Vector3(48, 14, 21);
            }
        }
    }
}