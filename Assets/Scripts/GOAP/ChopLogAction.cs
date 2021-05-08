using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopLogAction : Goap
{
    bool choppedWood = false;

    public ChopLogAction() : base(4)
    {
        AddPreCondition("Has Axe", true);
        AddEffect("Make firewood", true);
    }

    public override void Execute()
    {
        choppedWood = true;
        Debug.Log("Chopped Wood");
    }

    public override bool isDone()
    {
        return choppedWood;
    }
}
