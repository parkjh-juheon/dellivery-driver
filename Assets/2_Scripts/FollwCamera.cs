using UnityEngine;

public class FollwCame : MonoBehaviour
{
    [SerializeField]GameObject follwTarget;
    void LateUpdate()
    {
      transform.position = follwTarget.transform.position + new Vector3(0, 0, -10);
    }
}
