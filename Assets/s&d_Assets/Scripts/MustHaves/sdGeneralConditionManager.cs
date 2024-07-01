using UnityEngine;

public class sdGeneralConditionManager : MonoBehaviour
{
    public GameObject[] targetGameObjects;
    public GameObject[] objToBeEnabled;
    public GameObject[] objToBeDisabled;

    private bool isComplete;

    private void Update()
    {
        if (!isComplete && isConditionDone())
        {
            HandleConditionMet();

            isComplete = true;
        }
    }

    private bool isConditionDone()
    {
        foreach (GameObject target in targetGameObjects)
        {
            if (target != null)
            {
                return false;
            }
        }
        return true;
    }

    private void HandleConditionMet()
    {
        foreach (GameObject obj in objToBeEnabled)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objToBeDisabled)
        {
            obj.SetActive(false);
        }
    }
}
