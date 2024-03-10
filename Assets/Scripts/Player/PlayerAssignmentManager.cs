using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAssignmentManager : MonoBehaviour, IGameTimeObserver
{
    [Header("FOR UI")]
    [SerializeField] private TMP_InputField numberOfQustionsInputField;
    [SerializeField] private TMP_InputField questionInputField;

    private int numberOfQuestion; // Oyuncunun kaç soru ödev vereceðini bu deðiþkende tutucaz

    public void SoruSayisiniOnayla()
    {
        numberOfQuestion = int.Parse(numberOfQustionsInputField.text);

        if (numberOfQuestion < 0)
        {
            numberOfQuestion = 1;
        }
        else if (numberOfQuestion > 10)
        {
            numberOfQuestion = 10;
        }
        Debug.Log(numberOfQuestion);
    }
    public void SoruyuOnayla()
    {
        Debug.Log(questionInputField.text);
    }
    public void ChooseA()
    {
        Debug.Log("A");
    }
    public void ChooseB()
    {
        Debug.Log("B");
    }
    public void ChooseC()
    {
        Debug.Log("C");
    }
    public void ChooseD()
    {
        Debug.Log("D");
    }

    public void NextDay()
    {
        throw new System.NotImplementedException();
    }

    public void NextWeek()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextWeek metoduyum.");
    }

    public void NextMonth()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextMonth metoduyum.");
    }

    public void NextYear()
    {
        Debug.Log("Ben AttendanceManager sýnýfýna ait NextYear metoduyum.");
    }
}
