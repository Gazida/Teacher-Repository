using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private InGameTimeManage inGameTimeManage;

    [Header("For UI")]
    [Tooltip("Görevler bu textte yazacak")][SerializeField] TextMeshProUGUI[] questTexts;
    [Tooltip("Görev tamamlanýnca bu obje aktif olacak. Görevin tamamlandýðýný gösterecek.")][SerializeField] GameObject[] isCompletedQuest;

    [Header("Other")]
    [Tooltip("Görevler")][SerializeField] private Quest quest;



}
