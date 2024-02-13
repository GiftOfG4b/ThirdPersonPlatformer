using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// MovingPlatformChainSetup Script.
/// </summary>
public class MovingPlatformChainSetup : Script
{
    public float speed = 200f;

    public Actor pathHolder;

    Vector3[] waypoints;

    public Actor platformHolder;
    Actor[] platForms;//or prefabs?, decide how to

    int platformNum = 8;

    float firstStartTime = .125f;//this is how long before the first reaches and starts moving
    /// <inheritdoc/>
    public override void OnStart()
    {

        waypoints = new Vector3[pathHolder.ChildrenCount];

        for (int i = 0; i< waypoints.Length; i++){
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

        foreach (var platformActor in platForms)
        {
            MovingPlatform platform = platformActor.GetScript<MovingPlatform>();
            platform.timeToReach = time;
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

    
}
