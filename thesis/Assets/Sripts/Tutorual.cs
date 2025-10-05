using UnityEngine;
using System.Collections;

public class Tutorual : MonoBehaviour
{
    [SerializeField] private GameObject Tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tutorial.SetActive(false);
        }
    }
}
