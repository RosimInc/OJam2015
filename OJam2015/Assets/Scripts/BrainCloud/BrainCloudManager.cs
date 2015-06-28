using UnityEngine;
using System.Collections;
using BrainCloud;

public class BrainCloudManager : MonoBehaviour {

    public enum AchievementTypes { Deprivation, Parachute, Spotlight, Traitor, AFK, TermsAndConditions }

	public string username, password;
	public bool login, achieve;

    private static BrainCloudManager _instance;

    public static BrainCloudManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
	    {
            _instance = this;
	    }
    }

	// Use this for initialization
	void Start () 
	{
		BrainCloudWrapper.Initialize();
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (Input.GetKeyDown(KeyCode.R))
		{
			//LOGIN
		}*/

		if (login)
		{
			login = false;
			Login();
		}

		if (achieve)
		{
			achieve = false;
			AddAchievement(AchievementTypes.Parachute);
		}
	}

	public void Login()
	{
		BrainCloudWrapper.GetInstance().AuthenticateUniversal(username, password, true, OnSuccess_Authenticate, OnError_Authenticate);
	}

	private void OnSuccess_Authenticate(string response, object cbObject)
	{
		Debug.Log("Logged in! " + response);
	}


	private void OnError_Authenticate(int statusCode, int reasonCode, string message, object cbObject)
	{
		Debug.LogError("Wah, no login....\n" + message);
	}


	public void LoadStats()
	{
		//TODO
	}

	public void SaveStats()
	{
		//TODO
	}

	public void AddAchievement(AchievementTypes achievementType)
	{
        string achievementId = "";

        switch (achievementType)
        {
            case AchievementTypes.Deprivation:
                achievementId = "ACH01";
                break;
            case AchievementTypes.Parachute:
                achievementId = "ACH02";
                break;
            case AchievementTypes.Spotlight:
                achievementId = "ACH03";
                break;
            case AchievementTypes.Traitor:
                achievementId = "ACH04";
                break;
            case AchievementTypes.AFK:
                achievementId = "ACH05";
                break;
            case AchievementTypes.TermsAndConditions:
                achievementId = "ACH06";
                break;
        }

		BrainCloudClient bcc = BrainCloudWrapper.GetBC();
		BrainCloudGamification bcg = bcc.GetGamificationService();
		//bcg.AwardAchievements("2,3");

        string list = "{\"id\":\"" + achievementId + "\"}";

        bcg.AwardAchievements(list, OnSuccessAwarded, OnError_Authenticate);

		//bcg.AwardAchievements("ACH02", null, null, null);

		//bcg.AwardAchievements("ach");

		bcg.ReadAchievements(false, OnSuccess_Authenticate);
	}

    private void OnSuccessAwarded(string response, object cbObject)
    {
        Debug.Log("Awarded! " + response);

        if (response.Contains("ACH01"))
        {
            // Deprivation
            // Die before eating any candy

            PopupManager.Instance.ShowPopup("Deprivation", "Die before eating any candy");
        }
        else if (response.Contains("ACH02"))
        {
            // Parachute Needed
            // Die from falling

            PopupManager.Instance.ShowPopup("Parachute Needed", "Die from falling");
        }
        else if (response.Contains("ACH03"))
        {
            // Spotlight
            // Have your head lit up by the bat for 15 consecutive seconds

            PopupManager.Instance.ShowPopup("Spotlight", "Have your head lit up by the bat for 15 consecutive seconds");
        }
        else if (response.Contains("ACH04"))
        {
            // Traitor
            // Refuse to activate a switch to save the boy

            PopupManager.Instance.ShowPopup("Traitor", "Refuse to activate a switch to save the boy");
        }
        else if (response.Contains("ACH05"))
        {
            // AFK
            // Die from sugar deprivation without moving

            PopupManager.Instance.ShowPopup("AFK", "Die from sugar deprivation without moving");
        }
        else if (response.Contains("ACH06"))
        {
            // I Have Read And Accept The Terms And Conditions
            // Press B to activate a switch

            PopupManager.Instance.ShowPopup("I Have Read And Accept The Terms And Conditions", "Press B to activate a switch");
        }
    }
}
