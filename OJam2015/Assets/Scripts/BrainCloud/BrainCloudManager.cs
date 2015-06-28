using UnityEngine;
using System.Collections;
using BrainCloud;

public class BrainCloudManager : MonoBehaviour {

	public string username, password;
	public bool login, achieve;
	public string achievementId;

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
			AddAchievement(achievementId);
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

	public void AddAchievement(string achievementId)
	{
		BrainCloudClient bcc = BrainCloudWrapper.GetBC();
		BrainCloudGamification bcg = bcc.GetGamificationService();
		//bcg.AwardAchievements("2,3");

		string list = "{\"id\":\"ACH02\"},{\"id\":\"ACH03\"}";

		bcg.AwardAchievements(list, OnSuccess_Authenticate, OnError_Authenticate);

		//bcg.AwardAchievements("ACH02", null, null, null);

		//bcg.AwardAchievements("ach");

		bcg.ReadAchievements(false, OnSuccess_Authenticate);
	}
}
