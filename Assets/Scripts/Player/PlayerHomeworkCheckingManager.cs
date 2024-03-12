using UnityEngine;

public class PlayerHomeworkCheckingManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    [SerializeField] private PlayerFeatures playerFeatures;
    [SerializeField] private StudentsAndFeatures studentsAndFeatures;

    private void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            Debug.Log(playerFeatures.questionsCount);
        }
    }
}
