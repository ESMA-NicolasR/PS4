using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JunctionManager : MonoBehaviour
{
    [SerializeField]
    private List<Object> _junctionList; 

    private void Update()
    {
        if (_junctionList[1].GetComponent<JunctionScript>().right && _junctionList[0].GetComponent<JunctionScript>().left)
        {
            WinCondition();
        }
    }
    private void WinCondition()
    {
        if (_junctionList[1].GetComponent<JunctionScript>().up && _junctionList[0].GetComponent<JunctionScript>().right)
        {
            print("cleared");
        }
    }
}
