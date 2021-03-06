using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alingment")]
public class Allignment : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, maintain current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }

        //add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform c in context)
        {
            alignmentMove += (Vector2)c.transform.up;
        }
        alignmentMove /= context.Count;
        
        return alignmentMove;
    }

}
