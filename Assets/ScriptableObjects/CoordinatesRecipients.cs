using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CoordinatesRecipients", menuName = "Data/SelectableValues/CoordinatesRecipients", order = 1)]
public class CoordinatesRecipients : SelectableValues
{ 
    public override Type enumType => typeof(CoordinatesRecipient);
}
