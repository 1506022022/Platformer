using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageList", menuName ="Custom/StageList")]
public class StageList : ScriptableObject
{
    [SerializeField] List<string> mStageNames;
    public List<string> Names => mStageNames;
}
