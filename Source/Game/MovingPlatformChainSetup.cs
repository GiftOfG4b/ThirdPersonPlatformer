using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// MovingPlatformChainSetup Script.
/// </summary>
[ExecuteInEditMode]
public class MovingPlatformChainSetup : Script
{
    public float speed = 200f;

    public Actor pathHolder;

    Vector3[] waypoints;

    public Actor platformHolder;
    Actor[] platForms;//or prefabs?, decide how to

    int platformNum = 8;

    public float firstStartTime = .125f;//this is how long before the first reaches and starts moving
    /// <inheritdoc/>
    public override void OnStart()
    {

        waypoints = new Vector3[pathHolder.ChildrenCount];

        int waypointLen = waypoints.Length;
        for (int i = 0; i< waypointLen; i++){
            waypoints[i] = pathHolder.GetChild(i).Position;
        }

        platformNum = platformHolder.ChildrenCount;
        platForms = new Actor[platformNum];

        for (int i = 0; i< platformNum; i++){
            platForms[i] = platformHolder.GetChild(i);
        }

        //knowing the speed, the total path, and the number of platforms to set up, set up the starting interval between them
        //so that it loops perfectly

        //to loop perfectly, when the first is within distance n of the start, same distance between all others 
        //the last is about to leave

        //a) total distance
        float totalDist = PathTotDistance();

        //b) distance between each point when all moving

        float localDist = totalDist/platformNum;

        //c) time between points

        float timeToDist = localDist/speed;//x/t = v; t*x/v

        //d)for each platform, add that time to dist to their starting time
        float time = firstStartTime;

        
        //to do: make it proper
        //sets pos for each actor, between the path, and set their correct index
        //calculate time to reach by position to distance
        //on start the actors will naturally continue path and set themselves right

        //start at waypoint 0, when time large enough to get to other point

        
        int index = 0;//platformNum
        int prevIndex = waypointLen-1;
        float intervalTTR = CalcTimeToReach(waypoints[index], waypoints[prevIndex]);
        foreach (var platformActor in platForms)
        {
            //if time smaller than time to reach next waypoint
            //continue
            //else
            //subtract the previous time to waypoints from time 
            //set the two new platforms to calculate time to reach
            //(loop until smaller, otherwise bug)
            MovingPlatform platform = platformActor.GetScript<MovingPlatform>();
            while (time>intervalTTR)
            {
                time-=intervalTTR;
                prevIndex = index;
                index = (index+1)%waypointLen; 
                intervalTTR = CalcTimeToReach(waypoints[index], waypoints[prevIndex]);
                //place the platform by time
            }
            

            platformActor.Position = Vector3.Lerp(waypoints[prevIndex], waypoints[index], time/intervalTTR);//needs to be set before platform sets its own position as prevwaypoint
            //set first time to reach = 0.005 (very small value)
            //then set next index to correct value
            //Debug.Log("time: " + time + " ttr: " + intervalTTR + " index: " + index);
            platform.targetWaypointIndex = index;
            //time to dist
            platform.timeToReach = intervalTTR-time;
            //platform.time = time;



            time +=timeToDist;


            
            
        }

    }

    float PathTotDistance(){
        float lenSum = 0;
        for (int i = 1; i < waypoints.Length; i++)
        {
            lenSum += (waypoints[i]-waypoints[i-1]).Length;
        }
        if(waypoints.Length>2){
            lenSum += (waypoints[waypoints.Length-1]-waypoints[0]).Length;
        }
        return lenSum;
    }

    public float CalcTimeToReach(Vector3 targetWaypoint, Vector3 prevWayPoint){
        float tTR = (targetWaypoint-prevWayPoint).Length/speed;
        //time = timeToReach*Vector3.Dot((platformActor.Position-prevWayPoint),(platformActor.Position-prevWayPoint))/Vector3.Dot((targetWaypoint-prevWayPoint),(platformActor.Position-prevWayPoint));
        //Debug.Log("time: "+ time + " timepercent: " + time/timeToReach);
        return tTR;
    }

    
}
