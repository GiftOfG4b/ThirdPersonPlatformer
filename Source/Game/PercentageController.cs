using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// PercentageController Script.
/// moves platform according to how much hinge is rotated
/// </summary>
public class PercentageController : Script
{
    //public Actor hingeRotated;

    public HingeJoint hingeJoint;
    public RigidBody hingeRb;
    Vector3 rotationAxis;
    //public float rotationMag = 1f;
    //SliderJoint sliderJoint;

    public Actor movedActor;
    RigidBody movedActorRb;

    public Vector3 sliderActorStart;
    public Vector3 sliderActorEnd;
    Vector3 sliderDir;

    
    public float torqueBack= 1f;//amount of force to bring back to init value
    //public float maxAngle;
    //public float angle;
    public float gearRatio = 50f;//percentage = angle*gearRatio

    //float percentage;

    public CollisionWithInfo[] colliders;


    //simply return to initial post, as if having const force (== constant acceleration)
    //a = cst
    //v = at + c
    //c = v_0
    //x = (a*t**2)/2 + v_0*t + x_0

    //aka: moving object add force back to rest, rotational (percenter) add torque back to rest

    //hit adds quick force, 

    //but cap velocity (either setting friction or setting max directly)

    //then transfer angle to position: y = kx (capped between 0 and x_max)
    //y: angle difference from start
    //k: gear ratio
    //x: position difference from start
    
    //real question: add force to one == add force to other
    //door wants to fall back due to gravity, hinge wants to roll back into place

    //horizontal gear difference
    //set both friction (vs speed rotation)
    //and force that tries to bring back to original place
    
    //pushing against platform ,adds force to gear in relation to gearRatio


    /// <inheritdoc/>
    public override void OnStart()
    {
        // Here you can add code that needs to be called when script is created, just before the first game update
        
        rotationAxis = hingeRb.Transform.Up;

        sliderDir = (sliderActorEnd-sliderActorStart).Normalized;

       

        movedActorRb = (RigidBody)movedActor;
    }
    
    //todo: set limit according to distance moved obj can travel

    /// <inheritdoc/>
    public override void OnEnable()
    {
        // Here you can add code that needs to be called when script is enabled (eg. register for events)
        foreach (var collisioner in colliders)
        {
            collisioner.triggered += OnHingeHit;
        }
    }

    /// <inheritdoc/>
    public override void OnDisable()
    {
        // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        foreach (var collisioner in colliders)
        {
            collisioner.triggered -= OnHingeHit;
        }
    }

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        // Here you can add code that needs to be called every frame

        //if hit
        //v+=a_hit*t
        //
        //v+=a_normal*t
        //x+=vt
        //angle
        

        //if position != start position && position != end position
        //add force back
        //else
        //only move forward if start and back if end

        //percentage: 
        //hingeRotated.Rotation.Column1;
        //hingeRotated
        //hingeRotated.Transform.Orientation.EulerAngles;//pitch, yaw, roll 
        //if angle negative, subtract from 360
        //(probably up to 360)
        
        //hingeRb.AngularVelocity

        //brute optimization to get rid of the .Length: simply get components of angular velocity and rearange them to be in direction of moved's linear velocity
        movedActorRb.LinearVelocity = gearRatio*sliderDir*Vector3.Dot( hingeRb.AngularVelocity, rotationAxis);//gearRatio*sliderDir*hingeRb.AngularVelocity.Length;
        
        //magnitude: angular velocity projected on rotation axis
        
        //set velocity of object according to 


        //hingeRb.AddTorque(-rotationAxis*torqueBack, ForceMode.Force);//brings wheel back to initial position

        //hingeJoint.Transform.Forward
    }

    private void OnHingeHit(Collision collision)
    {

        Vector3 collisionPoint = collision.Contacts[0].Point;
        Vector3 collisionDir = collision.Contacts[0].Normal;

        //hingeRigidBody.AddForceAtPosition(collisionDir,collisionPoint);
        
    }
    //two version required: one that lets percentage get hit and one that doesnt (option: dont connect event for one that doesnt)
    private void OnMovedHit(Collision collision)
    {//actor that gets translated, decide if cheese force moves it back/forth

        Vector3 collisionPoint = collision.Contacts[0].Point;
        Vector3 collisionDir = collision.Contacts[0].Normal;

        //hingeRigidBody.AddForceAtPosition(collisionDir,collisionPoint);
        
    }
}
