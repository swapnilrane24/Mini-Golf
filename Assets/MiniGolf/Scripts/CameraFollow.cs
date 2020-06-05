using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script which makes camera follow ball
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    [SerializeField] private ActiveVectors activeVectors;   //class which decide axis allowed to follow

    private GameObject followTarget;                        //reference target to follow
    private Vector3 offset;                                 //offset between camera and ball
    private Vector3 changePos;                              //keep track of camera pos

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Method to set target from other scripts
    /// </summary>
    public void SetTarget(GameObject target)
    {
        followTarget = target;                                          //set the target
        offset = followTarget.transform.position - transform.position;  //set the offset
        changePos = transform.position;                                 //set the changePos
    }

    /// <summary>
    /// Unity method, we use lateUpdate as we want to change our camera position after Update method
    /// </summary>
    private void LateUpdate()
    {
        if (followTarget)                                               //if target is present
        {
            if (activeVectors.x)                                        //if x axis is allowed
            {                                                           //set the changePos x
                changePos.x = followTarget.transform.position.x - offset.x;
            }
            if (activeVectors.y)                                        //if y axis is allowed
            {                                                           //set the changePos y
                changePos.y = followTarget.transform.position.y - offset.y;
            }
            if (activeVectors.z)                                        //if z axis is allowed
            {                                                           //set the changePos z
                changePos.z = followTarget.transform.position.z - offset.z;
            }
            transform.position = changePos;                             //set the transform of camera
        }
    }
}

[System.Serializable]
public class ActiveVectors
{
    public bool x, y, z;
}
