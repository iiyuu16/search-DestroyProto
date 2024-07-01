using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnter : MonoBehaviour
{
    public GameObject[] gameObjectsToEnable;
    public GameObject[] gameObjectsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in gameObjectsToEnable)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in gameObjectsToDisable)
            {
                obj.SetActive(false);
            }

            Debug.Log("player in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in gameObjectsToEnable)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in gameObjectsToDisable)
            {
                obj.SetActive(true);
            }

            Debug.Log("player out");
        }
    }
}
