using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InGameTimeManage : MonoBehaviour
{
    [SerializeField] private int numberOfDaysInWeek = 4;
    [SerializeField] private int numberOfWeekInMonth = 4;
    [SerializeField] private int numberOfMonthInYear = 3;

    private int currentNumberOfDay;
    public int CurrentNumberOfDay { get { return currentNumberOfDay; } }

    private int currentNumberOfWeek;
    public int CurrentNumberOfWeek { get { return currentNumberOfWeek; } }

    private int currentNumberOfMonth;
    public int CurrentNumberOfMonth { get { return currentNumberOfMonth; } }

    private int currentNumberOfYear;
    public int CurrentNumberOfYear { get { return currentNumberOfYear; } }

    private List<IGameTimeObserver> gameTimeObservers = new List<IGameTimeObserver>();

    public void Attach(IGameTimeObserver observer)
    {
        gameTimeObservers.Add(observer);
    }
    public void NotifyObserversNextDay()
    {
        foreach (var observer in gameTimeObservers)
        {
            observer.NextDay();
        }
    }
    public void NotifyObserversNextWeek()
    {
        foreach (var observer in gameTimeObservers)
        {
            observer.NextWeek();
        }
    }
    public void NotifyObserversNextMonth()
    {
        foreach (var observer in gameTimeObservers)
        {
            observer.NextMonth();
        }
    }
    public void NotifyObserversNextYear()
    {
        foreach (var observer in gameTimeObservers)
        {
            observer.NextYear();
        }
    }
    public void IncreaseNumberOfDays()
    {
        currentNumberOfDay++;
        NotifyObserversNextDay();
        Debug.Log($"{(currentNumberOfDay + 1)}.gün");

        if (currentNumberOfDay % numberOfDaysInWeek == 0)
        {
            currentNumberOfWeek++;
            NotifyObserversNextWeek();
            Debug.Log($"{(currentNumberOfWeek + 1)}.hafta");
            if (currentNumberOfWeek % numberOfWeekInMonth == 0)
            {
                currentNumberOfMonth++;
                NotifyObserversNextMonth();
                Debug.Log($"{(currentNumberOfMonth + 1)}.ay");
                if (currentNumberOfMonth % numberOfMonthInYear == 0)
                {
                    currentNumberOfYear++;
                    NotifyObserversNextYear();
                    Debug.Log($"{(currentNumberOfYear + 1)}.yýl");
                }
            }
        }
    }
}
