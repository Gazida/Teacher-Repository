using UnityEngine;

public interface IGameTimeObserver
{
    void NextDay();
    void NextWeek();
    void NextMonth();
    void NextYear();
}
