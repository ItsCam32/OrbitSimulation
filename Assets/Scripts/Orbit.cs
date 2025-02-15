using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    // vv Private Exposed vv //

    [SerializeField]
    private Rigidbody planetRigidbody;

    [SerializeField]
    private Vector3 initialVelocity;

    // vv Private vv //

    private GameObject[] entities;
    private bool started = false;

    ////////////////////////////////////////

    #region Private Functions

    private void Start()
    {
        Time.timeScale = 1.5f;
        entities = GameObject.FindGameObjectsWithTag("Entity");
    }

    private void FixedUpdate()
    {
        if (!started) return;

        // Calculate gravitational force every frame and apply it to planet
        planetRigidbody.AddForce(calculateForce(), ForceMode.Impulse);
    }

    private Vector3 calculateForce()
    {
        foreach (GameObject entity in entities)
        {
            // Distance between sun and planet
            float distance = Vector3.Distance(this.transform.position, entity.transform.position);

            // Gravitational Constant
            //float G = 0.00667f;
            float G = 0.07f;

            // Newton's law of universal gravitation
            float F = G * entity.GetComponent<Rigidbody>().mass * planetRigidbody.mass / (distance * distance);

            // Pointing towards the sun
            Vector3 heading = entity.transform.position - this.transform.position;

            // Convert force to Vector3 (give it direction)
            Vector3 force = F * (heading / heading.magnitude);
            force *= 0.1f;
            return force;
        }
        return Vector3.zero;
    }
    #endregion

    #region Public Functions

    public void EnableOrbit()
    {
        if (started) return;

        started = true;
        planetRigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
    }
    #endregion
}
