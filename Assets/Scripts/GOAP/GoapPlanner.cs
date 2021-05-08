using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plan
{
    public List<Goap> actions;

    public int TotalCost
    {
        get
        {
            int totalCost = 0;

            for (int i = 0; i < actions.Count; i++)
                totalCost += actions[i].Cost;

            return totalCost;
        }
    }

    public Plan(Goap goalAction)
    {
        actions = new List<Goap>();
        actions.Add(goalAction);
    }
}

public class GoapPlanner : MonoBehaviour
{
    HashSet<Goap> availableActions;
    Queue<Goap> plan;


    float finalCost;

    List<Plan> possiblePlans;
    Plan actualPlan;

    KeyValuePair<string, object> currentState;
    KeyValuePair<string, object> goal;

    void CreateInitialPossiblePlans()
    {
        HashSet<Goap> actionsToRemove = new HashSet<Goap>();

        foreach (Goap action in availableActions)
        {
            foreach (KeyValuePair<string, object> effect in action.effects)
            {
                if (effect.Key == goal.Key)
                {
                    possiblePlans.Add(new Plan(action));
                    actionsToRemove.Add(action);
                    break;
                }
            }
        }

        foreach (Goap action in actionsToRemove)
            availableActions.Remove(action);
    }

    void Start()
    {
        availableActions = new HashSet<Goap>();
        possiblePlans = new List<Plan>();
        plan = new Queue<Goap>();

        currentState = new KeyValuePair<string, object>("Has Axe", false);
        goal = new KeyValuePair<string, object>("Make firewood", false);

        availableActions.Add(new ChopLogAction());
        availableActions.Add(new CollectWoodAction());
        availableActions.Add(new GetAxeAction());

        CreateInitialPossiblePlans();

        while (true)
        {
            bool newActionAdded = false;

            for (int i = 0; i < possiblePlans.Count; i++)
            {
                int finalIndex = possiblePlans[i].actions.Count - 1;

                foreach (KeyValuePair<string, object> preCondition in possiblePlans[i].actions[finalIndex].preConditions)
                {
                    foreach (Goap action in availableActions)
                    {
                        foreach (KeyValuePair<string, object> effect in action.effects)
                        {
                            if (preCondition.Key == effect.Key)
                            {
                                possiblePlans[i].actions.Add(action);
                                newActionAdded = true;
                            }
                        }
                    }
                }
            }

            if (!newActionAdded)
                break;
        }

        for (int i = 0; i < possiblePlans.Count; i++)
        {
            print("---");
            for (int j = 0; j < possiblePlans[i].actions.Count; j++)
            {
                print(possiblePlans[i].actions[j]);

            }
        }

        for (int i = 0; i < possiblePlans.Count; i++)
        {
            finalCost = possiblePlans[i].TotalCost;
            print(finalCost);
            if (i == 0)
            {
                continue;
            }
            else if (possiblePlans[i].TotalCost < possiblePlans[i-1].TotalCost)
            {
                actualPlan = possiblePlans[i];
            }
            else
            {
                actualPlan = possiblePlans[i - 1];
            }
        }

        for (int i = actualPlan.actions.Count - 1; i >= 0; i--)
        {
            plan.Enqueue(actualPlan.actions[i]);
            actualPlan.actions[i].Execute();
        }
    }

    void Update()
    {

    }
}
