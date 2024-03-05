using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PizzeriaLevel")]
public class PizzeriaLevel : ScriptableObject
{
    [Range(1, 12)]
    [SerializeField] private int _level = 1;

    private int _progress;

    private readonly List<int> _progressForLevels = new() { 0, 0, 100, 220, 350, 500, 800, 1150, 1500, 1900, 2350, 2650, 3000 };
    private readonly List<int> _levelMaxOrders = new() { 0, 3, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10 };
    private readonly List<float> _levelEarnMultiplier = new() { 0, 1, 1.1f, 1.2f, 1.25f, 1.35f, 1.45f, 1.55f, 1.65f, 1.75f, 1.85f, 1.95f, 2, 2.05f };

    public int Progress
    {
        get { return _progress; }
        private set
        {
            if (value < 0)
                _progress = 0;
            else
                _progress = value;
        }
    }

    

    public int GetLevel()
    {
        return _level;
    }

    /// <summary>
    /// Returns two values: current level progress and max level progress
    /// </summary>
    public int[] GetProgress()
    {
        return new int[] { _progress, _progressForLevels[_level + 1] };
    }

    public void AddProgress(int earned)
    {
        _progress += earned;
        if (_progress >= _progressForLevels[_level + 1])
        {
            _progress -= _progressForLevels[_level + 1];
            _level++;
        }
    }

    public int GetLevelMaxOrders(int level)
    {
        if (level == 0)
            return _levelMaxOrders[_level];
        else
            return _levelMaxOrders[level];
    }

    public float GetLevelEarnMultiplier(int level)
    {
        if (level == 0)
            return _levelEarnMultiplier[_level];
        else
            return _levelEarnMultiplier[level];
    }
}
