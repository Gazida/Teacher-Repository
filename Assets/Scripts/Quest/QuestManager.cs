using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;

    [Header("For UI")]
    [Tooltip("G�revler bu textte yazacak")][SerializeField] TextMeshProUGUI[] questTexts;
    [Tooltip("G�rev tamamlan�nca bu obje aktif olacak. G�revin tamamland���n� g�sterecek.")][SerializeField] GameObject[] isCompletedQuest;

    [Header("Other")]
    [Tooltip("G�revler")][SerializeField] private Quest quest;



}
