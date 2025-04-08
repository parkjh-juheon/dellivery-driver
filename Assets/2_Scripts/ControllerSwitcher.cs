using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    public Driver driverScript;
    public Drift driftScript;

    void Start()
    {
        driverScript.enabled = true;
        driftScript.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            driverScript.enabled = false;
            driftScript.enabled = true;
        }
        else
        {
            driverScript.enabled = true;
            driftScript.enabled = false;
        }
    }
}

