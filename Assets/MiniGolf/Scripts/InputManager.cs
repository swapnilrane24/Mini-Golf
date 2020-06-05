using UnityEngine;

/// <summary>
/// Script which detect mouse click and decide who will take input Ball or Camera
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float distanceBetweenBallAndMouseClickLimit = 1.5f; //variable to decide who will take input Ball or Camera

    private float distanceBetweenBallAndMouseClick;             //variable to track the distance
    private bool canRotate = false;                             //bool

    // Update is called once per frame
    void Update()
    {
        if (GameManager.singleton.gameStatus != GameStatus.Playing) return; //if gameStatus is not playing, return

        if (Input.GetMouseButtonDown(0) && !canRotate)          //if mouse button is clicked and canRotate is false
        {
            GetDistance();                                      //get the distance between mouseClick point and ball
            canRotate = true;                                   //set canRotate to true

            //if distance is less than the limit allowed
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseDownMethod();         //we control the ball
            }
        }

        if (canRotate)                                          //if canRotate is true
        {
            if (Input.GetMouseButton(0))                        //if mousebutton is clicked
            {                                                   //if distance is less than the limit allowed
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseNormalMethod();   //call ball method
                }
                else
                {                                               //else call camera method
                    CameraRotation.instance.RotateCamera(Input.GetAxis("Mouse X"));
                }
            }

            if (Input.GetMouseButtonUp(0))                      //on mouse click is left
            {
                canRotate = false;                              //canRotate is set false
                                                                //if distance is less than the limit allowed
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseUpMethod();       //call ball method
                }
            }
        }
    }

    /// <summary>
    /// Method which give us distance between click point in world and ball
    /// </summary>
    void GetDistance()
    {
        //we create a plane whose mid point is at ball position and whose normal is toward Camera
        var plane = new Plane(Camera.main.transform.forward, BallControl.instance.transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //create a ray
        float dist;                                                     //varibale to get ditance
        if (plane.Raycast(ray, out dist))
        {
            var v3Pos = ray.GetPoint(dist);                             //get the point at the given distance
            //calculate the distance
            distanceBetweenBallAndMouseClick = Vector3.Distance(v3Pos, BallControl.instance.transform.position);
        }
    }

}
