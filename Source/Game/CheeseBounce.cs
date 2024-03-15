using System;
using FlaxEngine;
using Game;


public class CheeseBounce : Script
{
    // Groups names for UI
    private const string MovementGroup = "Movement";
    private const string CameraGroup = "Camera";
    private float timeout;

    public CheeseHealth cheeseHealth;//on death, freeze rigidbody

    //add cheese visual ref at some point
    //public StaticModel ModelToControl;

    // public MaterialBase MaterialOn;
    // public MaterialBase MaterialOff;

    public RigidBody rigidBody;

    public float verticalSpeed = 500f;

    [Range(100f, 10000f)]
    public float speedForceMag = 20f;

    // Camera
    [ExpandGroups]

    [Tooltip("The camera view for player"), EditorDisplay(CameraGroup, "Camera View"), EditorOrder(8)]
    public Camera CameraView { get; set; } = null;
    
    [Range(0, 100f), Tooltip("Sensitivity of the mouse"), EditorDisplay(CameraGroup, "Mouse Sensitivity"), EditorOrder(9)]
    public float MouseSensitivity { get; set; } = 50f;

    [Tooltip("Determines the min and max pitch value for the camera"), EditorDisplay(CameraGroup, "Pitch Min Max"), EditorOrder(12)]
    public Float2 PitchMinMax { get; set; } = new Float2(-60, 60);

    [Range(0f, 100f), Tooltip("Lag of the camera, lower = slower"), EditorDisplay(CameraGroup, "Camera Lag"), EditorOrder(10)]
    public float CameraLag { get; set; } = 20;

    private float _yaw;
    private float _pitch;

    public float gravityMag = 9.8f;
    public Vector3 gravityDir = Vector3.Down;

    //Vector3 prevVelocity = Vector3.Zero;

    bool dead = false;

    /// <inheritdoc />
    public override void OnStart()
    {
        timeout = 0;
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;
    }

    /// <inheritdoc />
    public override void OnEnable()
    {
        rigidBody.CollisionEnter += OnCollisionEnter;
        cheeseHealth.cheeseDeath += RigorMortis;
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        rigidBody.CollisionEnter -= OnCollisionEnter;
        cheeseHealth.cheeseDeath -= RigorMortis;
    }

    void RigorMortis(){
        rigidBody.LinearVelocity /=2;
        //rigidBody.
        //rigidBody.AngularVelocity = Vector3.Zero;
        //rigidBody.Constraints = RigidbodyConstraints.LockAll;
        //rigidBody.IsKinematic = true;
        //Debug.Log("iskinematic: " + rigidBody.IsKinematic);
        this.Enabled = false;
        dead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {//check for way to remove wall friction, to not lose speed while touching one
    //as it is, collider physics material still impacts bounce 

        //error: if wall linked to floor, and slide down wall, wont register the floor as a seperate collision
        //todo: shapecast in gravity direction, by short distance
        //Physics.SphereCast(...)
        //when hits do collision

        // if (dead)
        // {
        //     return;
        // }

        Vector3 avgNormal = Vector3.Zero;
        foreach (var contact in collision.Contacts)
        {
            avgNormal=contact.Normal;
            if (Vector3.Dot(avgNormal,gravityDir)<-0.6f||Vector3.Dot(avgNormal,gravityDir)>0.6f)
            {
                //if hit floor-like or ceiling-like
                //Debug.Log("hit floor surface");
                
                


                if(Vector3.Project(rigidBody.LinearVelocity, avgNormal).LengthSquared<verticalSpeed*verticalSpeed){//check not boosted several times 
                    
                    //remove force in direction of avgNormal first (sorta works?)
                    //rigidBody.AddForce(Vector3.Project(rigidBody.LinearVelocity, avgNormal),ForceMode.VelocityChange);
                    //then add force in direction of avgNormal
                    //rigidBody.AddForce(avgNormal*verticalSpeed,ForceMode.VelocityChange);
                    //velocity directily
                    rigidBody.LinearVelocity-= Vector3.Project(rigidBody.LinearVelocity,avgNormal);
                    rigidBody.LinearVelocity+=avgNormal*verticalSpeed;

                    
                    //currently, no force even added, the collision is just regular

                    // if (collision.OtherActor.AttachedRigidBody){//if rigidbody attached to other collider
                    //     //check if theres not better way for this
                    //     collision.OtherActor.AttachedRigidBody.AddForce(-(Vector3.Project(rigidBody.LinearVelocity, avgNormal)+avgNormal*verticalSpeed)*rigidBody.Mass,ForceMode.Impulse);
                    // }
                }

                //todo: check impulse property use
                //The total impulse applied to this contact pair to resolve the collision.


                //if(collider belongs to rigidbody)
                //add force at collision point in avgNormal opposite direction
                

                return;
            }

        }
        // // avgNormal /=collision.ContactsCount;
        // avgNormal = collision.Contacts[0].Normal;
        
        // //if one of the contact points has normal facing up or down, add force in its direction
        


        // //Debug.Log("hit surface");
        // //rigidBody.AddForce(avgNormal*verticalSpeed,ForceMode.VelocityChange);
        // if (Vector3.Dot(avgNormal,gravityDir)<-0.6f||Vector3.Dot(avgNormal,gravityDir)>0.6f)
        // {
        //     //if hit floor-like or ceiling-like
        //     //Debug.Log("hit floor surface");
            
            


        //     if(Vector3.Project(rigidBody.LinearVelocity, avgNormal).LengthSquared<verticalSpeed*verticalSpeed){//check not boosted several times 
                
        //         //remove force in direction of avgNormal first (sorta works?)
        //         //rigidBody.AddForce(-Vector3.Project(rigidBody.LinearVelocity, avgNormal),ForceMode.VelocityChange);
        //         //then add force in direction of avgNormal
        //         //rigidBody.AddForce(avgNormal*verticalSpeed,ForceMode.VelocityChange);
        //         //velocity directily
        //         rigidBody.LinearVelocity-= Vector3.Project(rigidBody.LinearVelocity,avgNormal);
        //         rigidBody.LinearVelocity+=avgNormal*verticalSpeed;

                
        //         if (collision.OtherActor.AttachedRigidBody){//if rigidbody attached to other collider
        //             //check if theres not better way for this
        //             collision.OtherActor.AttachedRigidBody.AddForce(-(Vector3.Project(rigidBody.LinearVelocity, avgNormal)+avgNormal*verticalSpeed)*rigidBody.Mass,ForceMode.Impulse);
        //         }
        //     }

        //     //todo: check impulse property use
        //     //The total impulse applied to this contact pair to resolve the collision.


        //     //if(collider belongs to rigidbody)
        //     //add force at collision point in avgNormal opposite direction
            

        //     return;
        // }
        // else
        // {
        //     Debug.Log("hit non floor surface");
        // }

        //timeout = 0.5f;

    }

    /// <inheritdoc />
    // public override void OnUpdate()
    // {
    //     // bool isOn = false;
    //     // if (timeout > 0)
    //     // {
    //     //     timeout -= Time.UnscaledDeltaTime;
    //     //     isOn = timeout > 0;
    //     // }

    //     // if (ModelToControl)
    //     //     ModelToControl.SetMaterial(0, isOn ? MaterialOn : MaterialOff);
    // }


    public override void OnFixedUpdate()
    {
        // Camera Rotation
        {
            // Get mouse axis values and clamp pitch
            _yaw += Input.GetAxis("Mouse X") * MouseSensitivity* Time.DeltaTime; // H//* Time.DeltaTime
            _pitch += Input.GetAxis("Mouse Y") * MouseSensitivity* Time.DeltaTime ; // V* Time.DeltaTime
            _pitch = Mathf.Clamp(_pitch, PitchMinMax.X, PitchMinMax.Y);
            //_pitch
            // The camera's parent should be another actor, like a spring arm for instance
            CameraView.Parent.Orientation = Quaternion.Euler(_pitch, _yaw, 0);//Quaternion.Lerp(CameraView.Parent.Orientation, Quaternion.Euler(_pitch, _yaw, 0), Time.DeltaTime * CameraLag);
            //CharacterObj.Orientation = Quaternion.Euler(0, _yaw, 0);//rotate character visual (not in this case)

        }

        // Character Movement
        {
            // if (dead)
            // {
            //     return;
            // }
            // Get input axes
            var inputH = Input.GetAxis("Horizontal");
            var inputV = Input.GetAxis("Vertical");

            // Apply movement towards the camera direction
            var movement = new Vector3(inputH, 0.0f, inputV);
            var movementDirection = CameraView.Transform.TransformDirection(movement);
            

            Vector3 velocInDir = Vector3.Project(rigidBody.LinearVelocity, movementDirection);

            if(velocInDir.LengthSquared<750f*750f){//max allowed horizontal speed provided by input
                rigidBody.AddForce(movementDirection*speedForceMag);
            }

            //Vector3 horizVeloc = Vector3.ProjectOnPlane( rigidBody.LinearVelocity, Vector3.Up);

            // if(Vector3.Dot(prevVelocity,horizVeloc)<0f){
            //     Debug.Log("speed before hitting wall:"+horizVeloc.Length);
            // }

            
            // prevVelocity =  horizVeloc;
        }
    }
}

