using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFeatures", menuName = "Scriptable Objects/PlayerFeatures")]
public class PlayerFeatures : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Karakter ismi")] public string characterName;
    [Tooltip("Karakter yaþý")] public byte characterAge;
    [Tooltip("Öðretmenlik Branþý")] public string characterBranch;

    [Header("Bank Account")]
    [Tooltip("Maaþ")] public float salary;
    [Tooltip("Hesap bakiyesi")] public float accountBalance;
    [Tooltip("Kart þifresi")] public int cardPassword;

    // Bu deðerler oyun içi "HUD UI" sisteminde image'larýn fillAmount deðerlerini belirleyeceði için 0-1 arasýnda sýnýrlandýrýldý 
    [Header("Humanoid Features")]
    [Tooltip("Acýkma durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float hunger;
    [Tooltip("Susama durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float thirst;
    [Tooltip("Mana'nýn durumunun kontrol edileceði deðiþken")][Range(0, 1)] public float mana;
    [Tooltip("Tuvalet ihtiyacýnýn kontrol edileceði deðiþken")][Range(0, 1)] public float needForToilet;

    private void Awake()
    {
        // Bu deðerler oyun içi "HUD UI" sisteminde image'larýn fillAmount deðerlerini belirleyeceði için 0-1 arasýnda sýnýrlandýrýldý 
        hunger = Mathf.Clamp(hunger, 0, 1);
        thirst = Mathf.Clamp(thirst, 0, 1);
        mana = Mathf.Clamp(mana, 0, 1);
        needForToilet = Mathf.Clamp(needForToilet, 0, 1);
    }
}
