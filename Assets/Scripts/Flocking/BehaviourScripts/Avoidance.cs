using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class Avoidance : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int numAvoid = 0;
        foreach (Transform c in context)
        {
            if (Vector2.SqrMagnitude(c.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - c.position);
            }
            
        }
        if (numAvoid > 0 )
        {
            avoidanceMove /= numAvoid;
        }
        return avoidanceMove;
    }

}
