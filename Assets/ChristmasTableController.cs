using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChristmasTableController : MonoBehaviour
{
    MeshRenderer mesh;
    Material mat;

    public GameObject uiPanel;
    
       



    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "mic 1")
        {
            uiPanel.SetActive(true);
        }
    }
}