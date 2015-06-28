using UnityEngine;
using System.Collections;
using BrainCloud;
using System;
using JsonFx.Json;
using System.Collections.Generic;

public class BrainCloudManager : MonoBehaviour {

    public enum AchievementTypes { Deprivation, Parachute, Spotlight, Traitor, AFK }

	public string username, password;
	public bool login, achieve;

    private static BrainCloudManager _instance;

    private Action<bool[]> _registeredCallback;

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

            PopupManager.Instance.ShowPopup("Life sucks, doesn't it?", "Reach the ending and be disappointed by the meaning of life");
        }
        else if (response.Contains("ACH05"))
        {
            // AFK
            // Die from sugar deprivation without moving

            PopupManager.Instance.ShowPopup("AFK", "Die from sugar deprivation without moving");
        }
    }

    public void GetUnlockedAchievements(Action<bool[]> callback)
    {
        BrainCloudClient bcc = BrainCloudWrapper.GetBC();
		BrainCloudGamification bcg = bcc.GetGamificationService();

        _registeredCallback = callback;

        bcg.ReadAchievements(false, ReadCallback, null, null);
    }

    private void ReadCallback(string in_data, object cbObject)
    {
        Debug.Log("READ ACHIEVEMENTS: " + in_data);

        Dictionary<string, object> response = JsonReader.Deserialize<Dictionary<string, object>>(in_data);

        Dictionary<string, object> data = (Dictionary<string, object>)response[OperationParam.GamificationServiceAchievementsData.Value];
        Dictionary<string, object>[] achievements = (Dictionary<string, object>[])data[OperationParam.GamificationServiceAchievementsName.Value];

        bool[] array = new bool[5];

        for (int i = 0; i < achievements.Length; i++)
        {
            int index = int.Parse(((string)achievements[i]["id"]).Substring(4, 1)) - 1;

            if (index < 5)
	        {
		        if (((string)achievements[i]["status"]).ToUpper() == "AWARDED")
                {
                    array[index] = true;
                }
                else
                {
                    array[index] = false;
                }
	        }
        }

        if (_registeredCallback != null)
        {
            _registeredCallback(array);
            _registeredCallback = null;
        }
    }
}
