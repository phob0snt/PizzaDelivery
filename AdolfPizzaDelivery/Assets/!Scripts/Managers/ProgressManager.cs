using UnityEngine;
using Zenject;

public class ProgressManager : MonoBehaviour
{
    private PizzeriaLevel _level;
    [Inject] private MoneyManager _moneyManager;

    private void Awake()
    {
        _level = Resources.Load<PizzeriaLevel>("PizzeriaLevel");
    }

    public void AddLevelProgress()
    {
        _level.AddProgress(_moneyManager.GetOrderMoney()[0]);
    }

    public int GetLevel()
    {
        return _level.GetLevel();
    }

    public int[] GetProgress()
    {
        return _level.GetProgress();
    }
    /// <summary>
    /// If level = 0 returns current level value
    /// </summary>
    public int GetLevelMaxOrders(int level = 0)
    {
        return _level.GetLevelMaxOrders(level);
    }

    /// <summary>
    /// If level = 0 returns current level value
    /// </summary>
    public float GetLevelEarnMultiplier(int level = 0)
    {
        return _level.GetLevelEarnMultiplier(level);
    }

}
