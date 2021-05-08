using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class Cohesion : FlockBehaviour
{

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform c in context)
        {
            cohesionMove += (Vector2)c.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }


}
