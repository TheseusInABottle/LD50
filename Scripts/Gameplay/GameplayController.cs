using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public PlayerResourcesSO playerData;
    public PlayerCharacterSO[] users;

    private int randomEvent;
    [SerializeField]private bool eventActive;
    private float eTime;
    public float timeForEvent;
    public bool inEvent;
    public bool gameover;
    public bool restDay;

    private int x;

    [Header("UI Buttons")]
    public Slider daysLeft;
    public Button huntButton;
    public Button hideButton;
    public Button yesButton;
    public Button noButton;
    public Button continueButton;

    [Header("UI Slider")]
    public Slider pOneHp;
    public Slider pTwoHp;
    public Slider pThreeHp;
    public Slider pFourHp;
    public Slider pFiveHp;
    
    [Header("UI Text")]
    public TMP_Text eventText;
    public TMP_Text weeklyChoiceText;
    public TMP_Text foodText;
    public TMP_Text fuelText;
    public TMP_Text suppliesText;
    public TMP_Text bulletsCount;
    public TMP_Text uOneText;
    public TMP_Text uTwoText;
    public TMP_Text uThreeText;
    public TMP_Text uFourText;
    public TMP_Text uFiveText;
    

    // Start is called before the first frame update
    void Start()
    {
        eventActive = false;
        daysLeft.value = playerData.currentWeek * 7;

        // This is how long untill the first event
        randomEvent = Random.Range(0,10);
        eTime = 5f;

        eventText.text = "";
        restDay = false;

        uOneText.text = users[0].userName;
        uTwoText.text = users[1].userName;
        uThreeText.text = users[2].userName;
        uFourText.text = users[3].userName;
        uFiveText.text = users[4].userName;
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        pOneHp.value = users[0].health;
        pTwoHp.value = users[1].health;
        pThreeHp.value = users[2].health;
        pFourHp.value = users[3].health;
        pFiveHp.value = users[4].health;

        if (users[1].health <= 0 || users[1].health <= 0 || users[1].health <= 0 || users[1].health <= 0 || users[1].health <= 0 || users[1].health <= 0)
        {
            eventText.text = "The team falls apart due to sorrow. You Lose.";
            gameover = true;
        }

        if (playerData.food <= 0 || playerData.fuel <= 0 || playerData.supply <= 0 || playerData.bullets <= 0)
        {
            eventText.text = "You run low on resources and die. You Lose.";
            gameover = true;
        }

        foodText.text = playerData.food.ToString() + " pounds of food";
        fuelText.text = playerData.fuel.ToString() + " gallons of fuel";
        suppliesText.text = playerData.supply.ToString() + " total supplies";
        bulletsCount.text = playerData.bullets.ToString() + " rounds of ammo";

        if (playerData.weeklyMiniGameComplete != true && gameover == false)
        {
            weeklyChoiceText.gameObject.SetActive(true);
            playerData.currentWeekTime = 0f;
            playerData.weekStarted = false;
            huntButton.gameObject.SetActive(true);
            hideButton.gameObject.SetActive(true);
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);

        }
        else 
        {
            weeklyChoiceText.gameObject.SetActive(false);
            huntButton.gameObject.SetActive(false);
            hideButton.gameObject.SetActive(false);
            playerData.weekStarted = true;
        }

        if (playerData.weekStarted == true && eventActive != true && gameover == false)
        {

            randomEvent = Random.Range(0,9);

            // This counts up what day of the week it is
            playerData.currentWeekTime += Time.deltaTime;
            if(playerData.currentWeekTime >= 30f)
            {
                playerData.currentWeek++;
                playerData.weeklyMiniGameComplete = false;
            }

            if (eTime>= timeForEvent)
            {
                eTime -= Time.deltaTime;
            }
            else 
            {

                eventActive = true;
            }
        }

        if (eventActive == true && inEvent == false && restDay == false && gameover == false)
        {
            EventSelection(randomEvent);
        }
    }

    public void HuntEventButton()
    {
        playerData.weeklyMiniGameComplete = true;
    }

    public void HideButtonEvent()
    {
        playerData.weeklyMiniGameComplete = true;
        users[Random.Range(0,users.Length)].health++;
    }

    public void continueButtonEvent()
    {
        playerData.currentWeekTime = 0f;
        continueButton.gameObject.SetActive(false);
        eventText.text = "";
        eventActive = false;
        inEvent = false;
    }

    public void EventSelection(int number) 
    {
        switch(number)
        {
            case 0: 
                inEvent = true;
                eventText.text = "A zombie has appeared inside the base! It tried to bite " + users[Random.Range(0,users.Length)].userName + " but it didn't succeed";
                continueButton.gameObject.SetActive(true);
            break;

            case 1: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " hurt themselves moving some boxes";
                users[x].health--;
                if(users[x].health <= 0)
                {
                    users[x].alive = false;
                }
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 2: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " ate spoiled food";
                users[x].health--;
                playerData.food--;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 3: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " shot themselves in the foot";
                users[x].health--;
                playerData.bullets--;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 4: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " fell alseep on the cold ground";
                users[x].health--;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 5: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " got high and ate a bunch of food";
                users[x].health--;
                playerData.food -= 2;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 6: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " found some berry bushes";
                playerData.food += 2;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 7: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " dropped a cannister of fuel";
                playerData.fuel--;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 8: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " found some spair fuel";
                playerData.fuel++;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 9: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " waste supplies building a fort";
                playerData.supply--;
                users[x].health++;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;

            case 10: 
                inEvent = true;
                x = Random.Range(0,users.Length);
                eventText.text = users[x].userName + " hurt themselves moving some boxes";
                users[x].health--;
                continueButton.gameObject.SetActive(true);
                eTime = 5;
            break;
        }
    }
}
