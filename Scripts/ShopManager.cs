using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image skinPreview;
   
    public GameObject Shop;

    public Button btn;
    public Sprite[] skins;
    int currentIndex=0;
    private bool[] purchasedSkins;

    public GameObject Squirrel;
    public Sprite BuyButton;
    private Sprite PutButton;

    public AudioSource audioSource;
    public AudioClip SkinChange;

    private SpriteRenderer sr;
    public int SkinPrice = 10;

    private void Start()
    {
        PutButton = btn.image.sprite;
        

        sr = Squirrel.GetComponent<SpriteRenderer>();


       LoadPurchasedSkins();
        UpdateUI();

        /*PlayerPrefs.SetInt("SkinPurchased", 0);
        PlayerPrefs.Save();*/
    }



    public void SkinChangeForward()
    {

        currentIndex = (currentIndex+1) % skins.Length;

        audioSource.PlayOneShot(SkinChange);

        UpdateUI();
    }

    public void SkinChangeBack()
    {
        currentIndex = (currentIndex-1) % skins.Length;
        audioSource.PlayOneShot(SkinChange);

        UpdateUI();
    }

    private void LoadPurchasedSkins()
    {
        purchasedSkins = new bool[skins.Length];

        for(int i = 0; i < skins.Length; i++)
        {
            purchasedSkins[i] = PlayerPrefs.GetInt("SkinPurchased" + i, 0) == 1;
        }
    }

    public void ResetPurchasedSkins()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            PlayerPrefs.DeleteKey("SkinPurchased" + i); 
            purchasedSkins[i] = false; 
        }

        PlayerPrefs.Save(); 
        UpdateUI(); 
        Debug.Log("All purchased skins have been reset!");
    }

    public void BuySkin()
    {
        if (purchasedSkins[currentIndex])
        {
            sr.sprite = skinPreview.sprite;
            Squirrel.GetComponent<SpriteRenderer>().sprite = sr.sprite;
            PrefabUtility.SaveAsPrefabAsset(Squirrel, "Assets/Prefabs/Squirrel.prefab");
            UpdateUI();
            Debug.Log("Skin Changed");
            return; 
        }

        if (ScoreManager.instance.Purchase(SkinPrice))
        {
            purchasedSkins[currentIndex] = true;
            PlayerPrefs.SetInt("SkinPurchased" + currentIndex, 1);
            PlayerPrefs.Save();
          
            UpdateUI();
        }
        else
        {
            Debug.Log("Insufficeint Money");
        }
        
    }

    private void UpdateUI()
    {
        skinPreview.sprite = skins[currentIndex];

        if (purchasedSkins[currentIndex])
        {
            btn.image.sprite = PutButton;
            btn.interactable = true;
        }
        else
        {
            btn.image.sprite = BuyButton;
            btn.interactable = true;
            
        }

        // if the player is wearing the currentIndex Skin Disabel the button

        if(skinPreview.sprite.name == sr.sprite.name)
        {
            btn. interactable= false;
        }
    }

    

    /*public void SelectSkin()
    {
        *//*PlayerPrefs.SetInt("currentIndex", currentIndex);
        PlayerPrefs.Save();

        player.sprite = skins[currentIndex];*//*
        Squirrel.GetComponent<SpriteRenderer>().sprite = sr.sprite;
        PrefabUtility.SaveAsPrefabAsset(Squirrel, "Assets/Prefabs/Squirrel.prefab");
    }*/

    



    public void HomePage()
    {
        Shop.SetActive(false);
    }

}
