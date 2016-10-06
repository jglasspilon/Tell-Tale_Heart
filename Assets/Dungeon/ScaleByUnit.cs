// Summary:
//
// Created by: Julian Glass-Pilon
//

using UnityEngine;
using System.Collections;

public class ScaleByUnit : MonoBehaviour 
{
	void Start()
    {
        transform.localScale = new Vector3(DungeonCreator.unitSize, transform.localScale.y, DungeonCreator.unitSize);
    }
}
