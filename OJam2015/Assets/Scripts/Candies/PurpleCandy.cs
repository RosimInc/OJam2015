using UnityEngine;
using System.Collections;
using Collectibles;

public class PurpleCandy : Candy
{
    protected override void CollectCandy()
    {
        // Give points here
        SugarBar.Instance.AddCandy(SecondsValue);
    }
}
