using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goap
{
    public HashSet<KeyValuePair<string, object>> preConditions { get; private set; }
    public HashSet<KeyValuePair<string, object>> effects { get; private set; }

    public int Cost { get; private set; }

    public Goap(int cost)
    {
        Cost = cost;

        preConditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void AddPreCondition(string key, object value)
    {
        preConditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void RemovePreCondition(string key)
    {
        foreach (KeyValuePair<string, object> preCondition in preConditions)
        {
            if (preCondition.Key == key)
            {
                preConditions.Remove(preCondition);
                break;
            }
        }
    }

    public void AddEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }

    public void RemoveEffect(string key)
    {
        foreach (KeyValuePair<string, object> effect in effects)
        {
            if (effect.Key == key)
            {
                preConditions.Remove(effect);
                break;
            }
        }
    }

    public abstract bool isDone();
    public abstract void Execute();
}