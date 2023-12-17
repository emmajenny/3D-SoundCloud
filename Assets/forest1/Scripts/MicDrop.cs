using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicDrop : MonoBehaviour
{
    public GameObject mic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "mic 1")
        {
            SceneManager.LoadScene("HipHop");
            Debug.Log("HipHop");
        }
    }

}
