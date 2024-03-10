using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFeatures", menuName = "Scriptable Objects/PlayerFeatures")]
public class PlayerFeatures : ScriptableObject
{
    [Header("Player")]
    [Tooltip("Karakter ismi")] public string characterName;
    [Tooltip("Karakter ya��")] public byte characterAge;
    [Tooltip("��retmenlik Bran��")] public string characterBranch;

    [Header("Bank Account")]
    [Tooltip("Maa�")] public float salary;
    [Tooltip("Hesap bakiyesi")] public float accountBalance;
    [Tooltip("Kart �ifresi")] public int cardPassword;

    // Bu de�erler oyun i�i "HUD UI" sisteminde image'lar�n fillAmount de�erlerini belirleyece�i i�in 0-1 aras�nda s�n�rland�r�ld� 
    [Header("Humanoid Features")]
    [Tooltip("Ac�kma durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float hunger;
    [Tooltip("Susama durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float thirst;
    [Tooltip("Mana'n�n durumunun kontrol edilece�i de�i�ken")][Range(0, 1)] public float mana;
    [Tooltip("Tuvalet ihtiyac�n�n kontrol edilece�i de�i�ken")][Range(0, 1)] public float needForToilet;

    private void Awake()
    {
        // Bu de�erler oyun i�i "HUD UI" sisteminde image'lar�n fillAmount de�erlerini belirleyece�i i�in 0-1 aras�nda s�n�rland�r�ld� 
        hunger = Mathf.Clamp(hunger, 0, 1);
        thirst = Mathf.Clamp(thirst, 0, 1);
        mana = Mathf.Clamp(mana, 0, 1);
        needForToilet = Mathf.Clamp(needForToilet, 0, 1);
    }
}
