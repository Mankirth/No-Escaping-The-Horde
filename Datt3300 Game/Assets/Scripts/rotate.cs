using UnityEngine;

public class rotate : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 5);
    }
}