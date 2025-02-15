using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0.2f, 0));
    }
}
