using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class PlayerHumanoidFeaturesManager : MonoBehaviour
{
    [Header("Referances Scripts")]
    public PlayerFeatures playerFeatures;
    public FirstPersonController firstPersonController;
    public StarterAssetsInputs starterAssetsInputs;

    [Header("HUD UI")]
    [Tooltip("Ac�kma Bar�")] public Image hungerBar;
    [Tooltip("Susama Bar�")] public Image thirstBar;
    [Tooltip("Mana Bar")] public Image manaBar;
    [Tooltip("Tuvalet �htiyac� Bar�")] public Image needForToiletBar;

    [Header("UI Bar Rate of Decrease")]
    [Range(0f, 1f)][Tooltip("Karakterin ac�kma h�z�")] public float hungerChangeRate = .1f; 
    [Range(0f, 1f)][Tooltip("Karakterin susama h�z�")] public float thirstChangeRate = .1f; 
    [Range(0f, 1f)][Tooltip("Mana'n�n de�i�im h�z�")] public float manaChangeRate = .1f;

    private void Update()
    {
        HungerControls();
        ThirsControls();
        ManaControls();
        ClampThePlayerHumanoidFeatures();
    }

    private void HungerControls()
    {
        if (Time.timeScale != 0)
        {
            playerFeatures.hunger -= Time.deltaTime * hungerChangeRate;
        }
        hungerBar.fillAmount = playerFeatures.hunger;
    }

    private void ThirsControls()
    {
        if (Time.timeScale != 0)
        {
            playerFeatures.thirst -= Time.deltaTime * thirstChangeRate;
        }
        thirstBar.fillAmount = playerFeatures.thirst;
    }

    private void ManaControls()
    {
        // Mide seviyesi(a�l�k) 0.1'in alt�na d��erse h�zl� ko�ma �zelli�i olmas�n
        if (playerFeatures.hunger <= .1f)
        {
            playerFeatures.mana -= Time.deltaTime * 2;
            manaBar.fillAmount = playerFeatures.mana;
            firstPersonController.SetManaEnough(false);
        }
        // FPS kontrolc�s�nde h�zl� ko�ma durumunu kontrol eden bir bool de�i�keni var(manaEnough). Bu de�i�ken true ise h�zl� ko�ma eylemi i�in gerekli kodlar a�a��da i�lenir.
        else if (firstPersonController.GetManaEnough())
        {
            // Shift'e bas�l�yorsa ve karakter hareketliyse mana bar� seviyesini azalt
            if (starterAssetsInputs.sprint && starterAssetsInputs.move != Vector2.zero)
            {
                playerFeatures.mana -= manaChangeRate * Time.deltaTime;
                manaBar.fillAmount = playerFeatures.mana;
            }

            // manaEnough de�i�keni true iken ve (sprint aktif de�il || hareket yoksa) || her ikisi birdense mana bar�n� zamanla art�r
            else
            {
                playerFeatures.mana += manaChangeRate * Time.deltaTime;
                manaBar.fillAmount = playerFeatures.mana;
            }

            // Mana 0'�n alt�na d��erse mana yeterli mi de�i�kenini false yap
            if (playerFeatures.mana <= 0.01f)
                firstPersonController.SetManaEnough(false);
        }
        // manaEnough false ise yani karakter yorgunsa mana bar�n� zamanla art�r
        else
        {
            playerFeatures.mana += manaChangeRate * Time.deltaTime;
            manaBar.fillAmount = playerFeatures.mana;

            // Mana de�eri .5f'e (yar�s�na) geldi�inde karakter tekrar ko�abilir hale gelsin
            if (playerFeatures.mana > .5f)
                firstPersonController.SetManaEnough(true);
        }
    }

    // PlayerFeatures scriptindeki humanoid de�i�kenler float tipinde oldu�u i�in 0'�n alt�na d��ebilir veya 1'in �st�ne ��kabilir.
    // Bunun olmas�n� istemiyoruz ��nk� image componentindeki fillamount �zelli�i 0-1 aras�nda de�erler almal�.
    // �rne�in her framede manaBar.fillAmount = playerFeatures.mana; yapt���m�zda bir s�n�r koymazsak 0-1 d���ndaki de�erleri e�itlemeye �al���r�z.
    // Bu da her framede playerFeatures.mana de�i�kenini art�rsak bile "negatif de�erlerden kurtulamaz" problemine sebep olur.
    private void ClampThePlayerHumanoidFeatures()
    {
        if (playerFeatures.hunger < 0)
        {
            playerFeatures.hunger = 0;
        }
        else if (playerFeatures.hunger > 1)
        {
            playerFeatures.hunger = 1;
        }
        if (playerFeatures.thirst < 0)
        {
            playerFeatures.thirst = 0;
        }
        else if (playerFeatures.thirst > 1)
        {
            playerFeatures.thirst = 1;
        }
        if (playerFeatures.mana < 0)
        {
            playerFeatures.mana = 0;
        }
        else if (playerFeatures.mana > 1)
        {
            playerFeatures.mana = 1;
        }
        if (playerFeatures.needForToilet < 0)
        {
            playerFeatures.needForToilet = 0;
        }
        else if (playerFeatures.needForToilet > 1)
        {
            playerFeatures.needForToilet = 1;
        }
    }
}