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
    [Tooltip("Acýkma Barý")] public Image hungerBar;
    [Tooltip("Susama Barý")] public Image thirstBar;
    [Tooltip("Mana Bar")] public Image manaBar;
    [Tooltip("Tuvalet Ýhtiyacý Barý")] public Image needForToiletBar;

    [Header("UI Bar Rate of Decrease")]
    [Range(0f, 1f)][Tooltip("Karakterin acýkma hýzý")] public float hungerChangeRate = .1f; 
    [Range(0f, 1f)][Tooltip("Karakterin susama hýzý")] public float thirstChangeRate = .1f; 
    [Range(0f, 1f)][Tooltip("Mana'nýn deðiþim hýzý")] public float manaChangeRate = .1f;

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
        // Mide seviyesi(açlýk) 0.1'in altýna düþerse hýzlý koþma özelliði olmasýn
        if (playerFeatures.hunger <= .1f)
        {
            playerFeatures.mana -= Time.deltaTime * 2;
            manaBar.fillAmount = playerFeatures.mana;
            firstPersonController.SetManaEnough(false);
        }
        // FPS kontrolcüsünde hýzlý koþma durumunu kontrol eden bir bool deðiþkeni var(manaEnough). Bu deðiþken true ise hýzlý koþma eylemi için gerekli kodlar aþaðýda iþlenir.
        else if (firstPersonController.GetManaEnough())
        {
            // Shift'e basýlýyorsa ve karakter hareketliyse mana barý seviyesini azalt
            if (starterAssetsInputs.sprint && starterAssetsInputs.move != Vector2.zero)
            {
                playerFeatures.mana -= manaChangeRate * Time.deltaTime;
                manaBar.fillAmount = playerFeatures.mana;
            }

            // manaEnough deðiþkeni true iken ve (sprint aktif deðil || hareket yoksa) || her ikisi birdense mana barýný zamanla artýr
            else
            {
                playerFeatures.mana += manaChangeRate * Time.deltaTime;
                manaBar.fillAmount = playerFeatures.mana;
            }

            // Mana 0'ýn altýna düþerse mana yeterli mi deðiþkenini false yap
            if (playerFeatures.mana <= 0.01f)
                firstPersonController.SetManaEnough(false);
        }
        // manaEnough false ise yani karakter yorgunsa mana barýný zamanla artýr
        else
        {
            playerFeatures.mana += manaChangeRate * Time.deltaTime;
            manaBar.fillAmount = playerFeatures.mana;

            // Mana deðeri .5f'e (yarýsýna) geldiðinde karakter tekrar koþabilir hale gelsin
            if (playerFeatures.mana > .5f)
                firstPersonController.SetManaEnough(true);
        }
    }

    // PlayerFeatures scriptindeki humanoid deðiþkenler float tipinde olduðu için 0'ýn altýna düþebilir veya 1'in üstüne çýkabilir.
    // Bunun olmasýný istemiyoruz çünkü image componentindeki fillamount özelliði 0-1 arasýnda deðerler almalý.
    // Örneðin her framede manaBar.fillAmount = playerFeatures.mana; yaptýðýmýzda bir sýnýr koymazsak 0-1 dýþýndaki deðerleri eþitlemeye çalýþýrýz.
    // Bu da her framede playerFeatures.mana deðiþkenini artýrsak bile "negatif deðerlerden kurtulamaz" problemine sebep olur.
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