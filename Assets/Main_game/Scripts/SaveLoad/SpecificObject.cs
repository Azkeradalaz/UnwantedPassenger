using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificObject : SaveableObject
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float strength;

    // Update is called once per frame

    public override void Save(int id)
    {
        saveStats = speed.ToString() + "_" + strength.ToString();
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        speed = float.Parse(values[4]);
        strength = float.Parse(values[5]);
        base.Load(values);
    }
    
}
