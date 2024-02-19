using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PizzeriaLevel")]
public class PizzeriaLevel : ScriptableObject
{
    [SerializeField] private int _Level = 1;

    public int Progress;

    private List<int> progressForLevels = new() { 0, 100, 400, 1000, 1700, 2600, 4000 };

    public int GetLevel()
    {
        return _Level;
    }

    public int[] GetProgress()
    {
        return new int[] { Progress, progressForLevels[_Level] };
    }

    public void AddProgress(int earned)
    {
        Progress += earned;
        if (Progress >= progressForLevels[_Level])
        {
            Progress -= progressForLevels[_Level];
            _Level++;
        }
    }
}
