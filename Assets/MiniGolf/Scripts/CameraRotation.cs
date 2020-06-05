using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script which makes camera rotate around ball
/// </summary>
public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;    //rotation speed

    public static CameraRotation instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Metod called to rotate camera
    /// </summary>
    /// <param name="XaxisRotation">Mouse X value</param>
    public void RotateCamera(float XaxisRotation)           
    {
        transform.Rotate(Vector3.down, -XaxisRotation * rotationSpeed); //rotate the camera
    }
}
