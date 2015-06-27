using UnityEngine;
using System.Collections;
using Collectibles;

public class PinkLollipop : Candy
{
    protected override void CollectCandy()
    {
        // Give points here
        SugarBar.Instance.AddCandy(SecondsValue);
    }
}
