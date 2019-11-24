using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

 public GameObject GOCanvas;


  void Start()
    {
GOCanvas.SetActive(false);

}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
			GOCanvas.SetActive(true);
        }
    }
}
