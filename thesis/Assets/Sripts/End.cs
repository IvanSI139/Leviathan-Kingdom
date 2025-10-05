using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject TheEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TheEnd.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
