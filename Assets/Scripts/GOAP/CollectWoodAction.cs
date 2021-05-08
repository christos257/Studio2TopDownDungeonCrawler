using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWoodAction : Goap
{
    bool madeFirewood = false;

    public CollectWoodAction() : base(8)
    {
        AddPreCondition("None", true);
        AddEffect("Make firewood", true);
    }

    public override void Execute()
    {
        madeFirewood = true;
        Debug.Log("Collected Wood");
    }

    public override bool isDone()
    {
        return madeFirewood;
    }
}
