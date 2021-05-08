using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAxeAction : Goap
{
    bool hasAxe = false;

    public GetAxeAction() : base(2)
    {
        AddPreCondition("Axe Exist In World", true);
        AddEffect("Has Axe", true);
    }

    public override void Execute()
    {
        hasAxe = true;
        Debug.Log("Got Axe");
    }

    public override bool isDone()
    {
        return hasAxe;
    }
}
