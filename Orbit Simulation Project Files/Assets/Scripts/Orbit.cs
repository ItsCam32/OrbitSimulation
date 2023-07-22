using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private GameObject[] entities;
    private Rigidbody planetRigidbody;
    private bool started = false;

    private void Start()
    {
      Time.timeScale = 1.5f;
      entities = GameObject.FindGameObjectsWithTag("Entity");

      // Give planet initial velocity
      planetRigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (MainMenu.simulationActive && !started)
        {
            started = true;
            SetInitialVelocities();
        }
    }

    public void SetInitialVelocities()
    {
        switch (name)
        {
            case "Mercury|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 5f), ForceMode.VelocityChange);
                break;

            case "Venus|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 4.8f), ForceMode.VelocityChange);
                break;

            case "Earth|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 4.6f), ForceMode.VelocityChange);
                break;

            case "Mars|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 4.4f), ForceMode.VelocityChange);
                break;

            case "Jupiter|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 4.4f), ForceMode.VelocityChange);
                break;

            case "Saturn|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 4.0f), ForceMode.VelocityChange);
                break;

            case "Uranus|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 3.8f), ForceMode.VelocityChange);
                break;

            case "Neptune|Parent":
                planetRigidbody.AddForce(new Vector3(0f, 0f, 3.8f), ForceMode.VelocityChange);
                break;
        }
    }

    private void FixedUpdate()
    {
        // Calculate gravitational force every frame and apply it to planet
        if (started)
        {
            planetRigidbody.AddForce(calculateForce(), ForceMode.Impulse);
        }
    }

    private Vector3 calculateForce()
    {
      foreach (GameObject entity in entities)
      {
        // Distance between sun and planet
        float distance = Vector3.Distance(this.transform.position, entity.transform.position);

        // Gravitational Constant
        float G = 0.00667f;

        // Newton's law of universal gravitation
        float F = G * entity.GetComponent<Rigidbody>().mass * planetRigidbody.mass / (distance * distance);

        // Pointing towards the sun
        Vector3 heading = entity.transform.position - this.transform.position;

        // Convert force to Vector3 (give it direction)
        Vector3 force = F * (heading / heading.magnitude);

        // As increased timestep is used in engine for more accurate physics calculations
        // The force must be reduced to compensate
        force *= 0.1f;
        return force;
      }
      return Vector3.zero;
    }
}
