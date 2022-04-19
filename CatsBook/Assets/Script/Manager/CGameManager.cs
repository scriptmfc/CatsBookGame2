using System.Collections.Generic;
using UnityEngine;
using GameUtils;
using System;
//qusing BackEnd;
using System.Collections;


public enum PetId // 고양이 선택
{
	Default = 0,
	Cat1 = 1000,
	Cat2 = 2000,
	Cat3 = 3000,
	Cat4 = 4000,
	Cat5 = 5000,
	Cat6 = 6000,
}

public class CConstData
{
	public int Default_Gold = 0;
	public int Default_Gem = 0;	


	public float Attribute_ResetTime = 0;
	public float Attribute_Win = 0;
	public float Attribute_Draw = 0;
	public float Attribute_Lose = 0;

	public float Hot_Time_Gold = 0;
	public float Hot_Time_Exp = 0;
}

public class CGameManager : MonoBehaviour
{

	private static CGameManager instance = null;
	public static CGameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType(typeof(CGameManager)) as CGameManager;
				if (instance == null)
				{
					instance = new GameObject("CGameManager", typeof(CGameManager)).GetComponent<CGameManager>();
				}
			}
			return instance;
		}
	}

	public string version = "1.0.0";
	[SerializeField] private ChartDataLoad loadHandler;
	public bool isClientUpdate = false;
	//[SerializeField] private JsonHandler jsonHandler;

	//public List<StageData> liStageDatas = new List<StageData>();
	//public List<CharStatLevelData> liCharStatLevelDatas = new List<CharStatLevelData>();
	//public List<EquipmentData> liWeaponDatas = new List<EquipmentData>();

	

	//public Dictionary<int, SsuckSkillData> dicSsuckSkillDatasById = new Dictionary<int, SsuckSkillData>();
	//public Dictionary<int, SkillLevelData> liSkillLevelDatasById = new Dictionary<int, SkillLevelData>();

	public Dictionary<int, string> dicPackageById = new Dictionary<int, string>();
	public Dictionary<int, string> dicCashById = new Dictionary<int, string>();
	public Dictionary<int, string> dicPassById = new Dictionary<int, string>();

	public Dictionary<string, int> dicAchievementCountById = new Dictionary<string, int>();
	public Dictionary<string, int> dicAchievementLevelById = new Dictionary<string, int>();


	public CConstData constData = new CConstData();
	public Dictionary<string, string> dicString = new Dictionary<string, string>();

	public Dictionary<int, List<int>> dicRandomLockById = new Dictionary<int, List<int>>();
	public Dictionary<int, List<int>> dicRandomEffectById = new Dictionary<int, List<int>>();

	public Dictionary<int, int> dicFreeKillPassById = new Dictionary<int, int>();
	public Dictionary<int, int> dicChargeKillPassById = new Dictionary<int, int>();
	public Dictionary<int, int> dicFreeLevelPassById = new Dictionary<int, int>();
	public Dictionary<int, int> dicLevelPassById = new Dictionary<int, int>();

	public Dictionary<int, int> dicFreeEventPassById = new Dictionary<int, int>();
	public Dictionary<int, int> dicChargeEventPassById = new Dictionary<int, int>();


	public Dictionary<string, int> dicSsuckSkillLevelById = new Dictionary<string, int>();
	public List<int> liSkillApply = new List<int>();

	//public List<int> liRandomLocks = new List<int>();
	//public List<int> liRandomEffect = new List<int>();

	//public GameObject enemyObj;


	//public CPlayer player;



	public bool isNetwork = true;
	public string userId = "";
	public string nickName = "";
	public PetId petId = PetId.Default;
	public string nationType = "";
	public int stageID = 1;
	public int currentStageId = 1;

	public bool isCurrentBossStage = true;
	public bool isInfinityStage = false;

	public int userLevel {
		get; set;
	} = 1;

	public string offlineTime = "";
	public string adGoodsTime = "";
	public double userExp = 0;
	public double userGold = 0;
	public double userGem = 0;

	public int petCurrentId = 1;

	public bool isDamageDungeon = false;
	public bool isGiantDungeon = false;
	public bool isPetDungeon = false;
	public bool isWorldFight = false;
	public bool isFirstLogin = false;

	public int attendStep = 1;
	public int attendLevel = 0;
	public bool isAttend = false;
	public bool isRemoveADS = false;
	public string resetDayDate = "";

	public string loginType = "Guest";

	public float goldPercent = 0f;
	public float expPercent = 0f;

	public bool isConnect = true;


	//public Action<int, CShopData> actGotchaWeaponEventListener;

	public Action<double> physicalEventListener;
	public Action<float> moveSpeedEventListener;
	public Action<bool> actRunEventListener;

	public void setGameDataInit()
	{
		DontDestroyOnLoad(gameObject);
		instance = this;
		loadHandler.setDataInit();
	}

	public void onGameReady()
    {
		isConnect = true;
		onEnterWorldStage();
		CUIManager.Instance.onInitUI();


		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 301,
		 (time) => {
			 if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
			 {
				 //CInGameDataManager.Instance.onUpdateUserInfo();
			 }
		 }, "UserInfo");


		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 400,
			(time) => {
				if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
				{
					//Backend.BMember.IsAccessTokenAlive((accessToekenBackend) => {
					//	if (!accessToekenBackend.IsSuccess())
					//	{
					//		if (accessToekenBackend.GetStatusCode().Equals("401") &&
					//			accessToekenBackend.GetMessage().Contains("잘못된 bad,accessToken"))
					//		{
					//			COneButtonPopup popup = CPopupManager.Instance.openPopup(CUIManager.Instance.popupTrs, "OneButtonPopup").GetComponent<COneButtonPopup>();
					//			popup.onUpdateInfo("다른 기기에서 접속하여 게임이 종료됩니다.", () => {
					//				Application.Quit();
					//			});
					//			Time.timeScale = 0;
					//			return;
					//		}
					//	}
					//});
				}
			}, "AccessToeken");


		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 597,
		(time) => {
			if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
			{
				//Backend.Notice.GetTempNotice(callback =>
				//{
				//	string notice = string.Format("{0}", callback.Substring(26, callback.Length - 26 - 2).Replace("\\n", "\n"));
				//	CUIManager.Instance.onUpdateNotice(notice);
				//});
			}
		}, "Notice");


		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 697,
		(time) => {
			if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
			{
				//CGameUtils.isServerStatus(() => {
				//	COneButtonPopup popup = CPopupManager.Instance.openPopup(CUIManager.Instance.popupTrs, "OneButtonPopup").GetComponent<COneButtonPopup>();
				//	popup.onUpdateInfo("점검으로 인해 20초 후에 게임이 종료될 예정입니다.");

				//	if (nickName == "테스트" || nickName == "이이이" || nickName == "운영자" ||
				//	nickName == "너불" || nickName == "진짜진짜01" || nickName == "eodjduy" ||
				//	nickName == "가즈아" || nickName == "진짜진짜02")
				//	{
				//		return;
				//	}
				//	StartCoroutine(delayUserKick());
				//});
			}
		}, "ServerStatus");

		//CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 822,
		// (time) => {
		//	 if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
		//	 {
		//		 CInGameDataManager.Instance.onUpdateAchievementInfo();
		//	 }
		// }, "AchievementInfo");
	
		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 1187,
        (time) =>
        {
            if (time.Equals(0) && !CPopupManager.Instance.isOpenPopup && isNetwork)
            {
				DateTime current = CGameUtils.getServerDateTime();
				offlineTime = current.ToString();
				//CInGameDataManager.Instance.onUpdateOfflineTime();
			}
        }, "OfflineReward");


        CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 3601,
        (time) =>
        {			
			if (time.Equals(0) && isNetwork)
            {
				//CInGameDataManager.Instance.onUpdateInsertLog();
				GC.Collect();
			}
        }, "GC");


		//CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 3737,
		//(time) =>
		//{
		//	if (time.Equals(0) && isNetwork)
		//	{
		//		CInGameDataManager.Instance.onUpdateInsertLog("weapon");
		//	}
		//}, "weapon");

		//CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 3817,
		//(time) =>
		//{
		//	if (time.Equals(0) && isNetwork)
		//	{
		//		CInGameDataManager.Instance.onUpdateInsertLog("costume");
		//			}
		//}, "costume");


		//CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 3887,
		//(time) =>
		//{
		//	if (time.Equals(0) && isNetwork)
		//	{
		//		CInGameDataManager.Instance.onUpdateInsertLog("treasure");
		//	}
		//}, "treasure");


		CTimeManager.Instance.addTimeEventListener(TIMEER_TYPE.CONTINUITY, 40001,
		  (time) =>
		  {
			  if (time.Equals(0) && isNetwork)
			  {
				  //Backend.BMember.RefreshTheBackendToken((callback)=> {});
			  }
		  }, "RefrshToken");
	}


	IEnumerator delayUserKick()
	{
		yield return CGameUtils.WaitForSeconds(20f);
		Application.Quit();
	}


	public void onEnterWorldStage()
	{
		//petManager.gameObject.SetActive(false);
		//giantManager.gameObject.SetActive(false);
		//damageManager.gameObject.SetActive(false);
		//CUIManager.Instance.onPlayFadeInOut();
		//worldManager.gameObject.SetActive(true);		
		//worldManager.onStartBackGround();
		//resetStageInfo();
		onPlayRun();
	}

	
	//private void resetDamageDungeonInfo()
	//{
	//	currentDamageData = dicDamageDungeonDatasById[1];
	//	damageManager.resetBackGround(currentDamageData);
	//	CUIManager.Instance.onUpdateDamageDungeonText();
	//	damageEnemyObj.gameObject.SetActive(true);
	//	damageEnemyObj.GetComponent<CDamageEnemy>().resetDamageEnemyData(currentDamageData);
	//	//onPlayRun();
	//}

	public void onPlayRun()
    {
		//player.onPlayRun();
  //      if (isPetDungeon) petManager.isRun = true;
		//else if (isGiantDungeon) giantManager.isRun = true;
		//else if (isDamageDungeon){ }
		//else worldManager.isRun = true;
	}

	public void onStopRun()
	{
		//player.isFight = false;
		//player.playerAni.SetBool("isAttack", false);		
		//player.playerAni.SetBool("isCriticalLevel1", false);
		//player.playerAni.SetBool("isCriticalLevel2", false);
		//player.playerAni.SetBool("isCriticalLevel3", false);
		//player.playerAni.SetBool("isCriticalLevel4", false);
		//player.playerAni.SetBool("isCriticalLevel5", false);
		//player.playerAni.SetBool("isCriticalLevel6", false);
		//player.onStopRun();
		////player.playerAni.SetBool("isRun", false);


		//if (isPetDungeon) petManager.onStopBackGround();
		//else if (isGiantDungeon) giantManager.onStopBackGround();
		////else if (isDamageDungeon) damageManager.onStopBackGround();
		//else worldManager.onStopBackGround();
	}
	public void onStopDamageDungeon(bool isdead,double value, bool _isauto = false, int count = 1)
	{
		//if(!_isauto)
  //      {
		//	CUIManager.Instance.onStopBossTimer();
		//	CUIManager.Instance.onEnableHp(false);
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//}
  //      else
  //      {
		//	damageDungeonTicket -= count;
		//	List<double> lireward = new List<double>();
			
		//	DamageDungeonRewardData data = new DamageDungeonRewardData();
		//	double skillreward = 0;
		//	if (myDamageRankData.Score > 1)
		//	{
		//		int temp = Mathf.FloorToInt((float)myDamageRankData.Score);
		//		DamageDungeonRewardData reward = dicDamageDungeonRewardDataById[temp];
		//		double multi = myDamageRankData.Score - Math.Truncate(myDamageRankData.Score);
		//		value = (multi * reward.Dmg_Val) * 10;
		//	}
		//	else
		//	{
		//		int temp = Mathf.FloorToInt((float)myDamageRankData.Score);
		//		value = myDamageRankData.Score * 1000;
		//	}

		//	for (int i = 0; i < liDamageDungeonRewardDatas.Count; i++)
		//	{
		//		if (value >= 1000)
		//		{
		//			if (value >= liDamageDungeonRewardDatas[i].Dmg_Val)
		//			{
		//				data = liDamageDungeonRewardDatas[i];
		//				skillreward = data.Reward_Val;
		//			}
		//		}
		//		else
		//		{
		//			skillreward = 0;
		//		}
		//	}

		//	skillreward = skillreward * count;
		//	lireward.Add(skillreward);
		//	earnGoodsId((GoodsId)data.Reward_Type, skillreward);
		//	CUIManager.Instance.openDamageResultPopup(isdead, value, lireward, 0,true);
		//	return;
  //      }

		//if (isdead)
  //      {
		//	damageDungeonTicket--;
		//	if (damageDungeonTicket <= 0)
		//	{
		//		damageDungeonTicket = 0;
		//	}
		//	List<double> lireward = new List<double>();
		//	DamageDungeonRewardData data = new DamageDungeonRewardData();
		//	double skillreward = 0;
		//	float rankscore = 0;
		//	for (int i = 0; i < liDamageDungeonRewardDatas.Count; i++)
		//	{
		//		if (value >= 1000)
		//		{
		//			if (value >= liDamageDungeonRewardDatas[i].Dmg_Val)
		//			{
		//				data = liDamageDungeonRewardDatas[i];
		//				float temp = (float)(value / data.Dmg_Val) / 10;
		//				rankscore = temp + (float)data.Substitute_Score;
		//				skillreward = data.Reward_Val;
		//			}
		//		}
		//		else
		//		{
		//			rankscore = (float)(value / 1000);
		//			skillreward = 0;
		//		}
		//	}

		//	lireward.Add(skillreward);
		//	earnGoodsId((GoodsId)data.Reward_Type, skillreward);
		//	CUIManager.Instance.openDamageResultPopup(isdead,value, lireward, rankscore);
		//}
  //      else
  //      {
		//	CUIManager.Instance.openDamageResultPopup(isdead, value, new List<double>(), 0);
		//}
		//CInGameDataManager.Instance.onUpdateUserInfo();
		//CUIManager.Instance.onUpdateDamageAttackText("0", false);
	}


	public void onStartDamageDungeon()
	{
		isDamageDungeon = true;
		onStopWorldFight(false);
	}


	public void onStartPetDungeon()
    {
		isPetDungeon = true;
		onStopWorldFight(false);
	}

	public void onStartStageMove()
	{
  //      if (player.isFight)
  //      {
		//	player.enemyData.onDeadEnemy(false,true);
		//}
  //      else
  //      {
		//	onStopWorldFight(false, 0, true);
		//}
    }

	public void onStopWorldFight(bool isdead,float deleytime = 0 ,bool isstagemove = false)
    {
		//CUIManager.Instance.onStopBossTimer();
		//CUIManager.Instance.onEnableHp(false);
		//isWorldFight = false;

		//if(isConnect == false)
  //      {
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//	return;
		//}

		//if (isPetDungeon)
  //      {
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//	onEnterPetDungeon();
		//	return;
		//}

		//if (isGiantDungeon)
		//{
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//	onEnterGiantDungeon();
		//	return;
		//}

		//if (isDamageDungeon)
		//{
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//	onEnterDamageDungeon();
		//	return;
		//}

		//if (isstagemove)
  //      {
		//	CUIManager.Instance.enableBossButton(false);
		//	onStopRun();
		//	onEnterWorldStage();
		//	return;
  //      }

		//TreasureData relic = dicTreasureDatasById[(int)TreasureType.Get_Gold_UP_Per];
		//int level = dicTreasureLevelById[(int)TreasureType.Get_Gold_UP_Per];
		//goldPercent = CGameUtils.getCalRelicPassive(relic.Passive_Skill_Value, relic.Passive_Skill_Increase_Value, level);

		//goldPercent += getRandomEffect("Set_Get_Gold_UP_Per") + getSkillPassive(SkillType.SsuckSkill_402);
		//goldPercent += buffGoldUpMulti;

		//expPercent = getSkillPassive(SkillType.SsuckSkill_403);

		//double resultexp = Math.Truncate(currentStageData.Reward_Exp + (currentStageData.Reward_Exp * expPercent));

		//double resultgold = Math.Truncate(currentStageData.Reward_Gold + (currentStageData.Reward_Gold * goldPercent));

		//resultexp += resultexp * constData.Hot_Time_Exp;
		//resultgold += resultgold * constData.Hot_Time_Gold;


		//if (isCurrentBossStage && !isdead)
  //      {
		//	if(currentStageData.Stage_Type == "Boss")
		//	CSoundManager.Instance.onPlayVoiceSound(VoiceSound.boss_fail);

		//	CUIManager.Instance.onUpdateUserGoods(0 ,0, 0, 0, -1);
		//	isCurrentBossStage = false;
		//	isInfinityStage = true;
		//	CUIManager.Instance.enableBossButton(true, "보스 재도전");
  //      }
		//else if (isCurrentBossStage && isdead)
		//{
		//	CAchievementManager.Instance.setAchievementPlusCount("Ach_KillEnemy", 1);
		//	if (currentStageId >= stageID)//&& deleytime > 0
		//	{
		//		if (deleytime > 0)
  //              {
  //                  if (currentStageData.Frist_Reward_Cash == 0)
  //                  {
  //                      currentStageData = dicStageDatasById[currentStageId];
  //                  }
  //              }
  //              CUIManager.Instance.onUpdateUserGoods(resultexp,
		//		resultgold, currentStageData.Frist_Reward_Cash, 0, 1);
  //              if (currentStageData.Frist_Reward_Cash > 0)
  //              {
		//			CInGameDataManager.Instance.onUpdateUserInfo();
  //              }
		//	}
  //          else
		//	{
		//		CUIManager.Instance.onUpdateUserGoods(resultexp,
		//		resultgold, 0, 0, 1);
		//	}
			
		//	CUIManager.Instance.enableBossButton(false);
		//	isInfinityStage = false;
		//	CAchievementManager.Instance.onUpdateGuideQuest();
		//}
		//else if (!isCurrentBossStage && isdead)
  //      {
		//	CAchievementManager.Instance.setAchievementPlusCount("Ach_KillEnemy", 1);
		//	CUIManager.Instance.onUpdateUserGoods(resultexp,
		//		resultgold, 0, 0, 0);
		//}

		//if (deleytime > 0)
		//{
		//	CAchievementManager.Instance.setAchievementPlusCount("Ach_StageProgress", 1);
		//	if (currentStageData.Story_Event == "0")
  //          {
		//		onUpdateStep();
  //          }
		//	if (!isPetDungeon && !isGiantDungeon)
  //          {
		//		StartCoroutine(delayPlayRun());
		//	}				
		//}
		//else
		//{
		//	onUpdateStep();
		//	onPlayRun();
		//}

	}

	private IEnumerator delayPlayRun()
    {
		yield return CGameUtils.WaitForSeconds(1.2f);
		//if (!isPetDungeon && !isGiantDungeon && !isDamageDungeon)
		//{
		//	if (currentStageData.Story_Event != "0")
		//	{
		//		worldManager.gameObject.SetActive(false);
		//		CUIManager.Instance.onPlayStory(dicString[currentStageData.Story_Event], () => {
		//			worldManager.gameObject.SetActive(true);
		//			CUIManager.Instance.onPlayStageClearEffect();
		//			onUpdateStep();
		//			onPlayRun();
		//		});
		//	}
		//	else
		//	{
		//		CUIManager.Instance.onPlayStageClearEffect();
		//		onPlayRun();
		//	}
		//}	
	}


	private void enablePet()
    {
  //      if (phoenix.gameObject.activeSelf == false && dicPetLevelById[(int)PetId.Phoenix] > 0)
  //      {
		//	phoenix.gameObject.SetActive(true);
		//}

		//if (sammiho.gameObject.activeSelf == false && dicPetLevelById[(int)PetId.Sammiho] > 0)
		//{
		//	sammiho.gameObject.SetActive(true);
		//}

		//if (minirobot.gameObject.activeSelf == false && dicPetLevelById[(int)PetId.MiniRobot] > 0)
		//{
		//	minirobot.gameObject.SetActive(true);
		//}
	}
	

	public void onUpdatePetCostume()
    {
		//phoenix.resetCostume();
		//sammiho.resetCostume();
		//minirobot.resetCostume();
	}
	

	public float getCostumeAttribute(string name)
	{
		float murimvalue = 0;
		float fantasyvalue = 0;
		float contemporaryvalue = 0;

		if (name == "Murim")
		{
			//for (int i = 0; i < liMurimCostumeDatas.Count; i++)
			//{

			//}
		}
		else if (name == "Fantasy")
		{

		}
		else if (name == "Contemporary")
		{

		}

		return murimvalue + fantasyvalue + contemporaryvalue;
	}

	//코스튬 정보 갱신
	public void onUpdateCostume()
	{
		//EquipmentData costume = dicCostumeDatasById[equipedCostumeMurim];
		//int level = dicCostumeLevelById[costume.EquipmentTable_Id];
		//costumeMurimAttackSpeed = CGameUtils.getCalCostumeSpeed(costume.Speed, costume.Speed_Rate, level);
		//costumeMurimAttribute = CGameUtils.getCalCostumeAttributeAttack(costume.Attribute_Atk,
		//	costume.Attribute_Atk * costume.Attribute_Atk_Rate, level);

		//for (int i = 0; i < liMurimCostumeDatas.Count; i++)
		//{
		//	if (dicCostumeLevelById[liMurimCostumeDatas[i].EquipmentTable_Id] > 0)
		//	{
		//		int effectlevel = dicCostumeLevelById[liMurimCostumeDatas[i].EquipmentTable_Id];
		//		EquipmentData tempcostume = liMurimCostumeDatas[i];
		//		float temp = CGameUtils.getCalCostumeAttributeAttack(tempcostume.Attribute_Atk,
		//			tempcostume.Attribute_Atk * tempcostume.Attribute_Atk_Rate, effectlevel) * 0.01f;
		//		costumeMurimAttribute += (temp * (tempcostume.Have_Effect * 100));
		//	}
		//}


		//costume = dicCostumeDatasById[equipedCostumeFantasy];
		//level = dicCostumeLevelById[costume.EquipmentTable_Id];
		//costumeFantasyAttackSpeed = CGameUtils.getCalCostumeSpeed(costume.Speed, costume.Speed_Rate, level);
		//costumeFantasyAttribute = CGameUtils.getCalCostumeAttributeAttack(costume.Attribute_Atk,
		//	costume.Attribute_Atk * costume.Attribute_Atk_Rate, level);

		//for (int i = 0; i < liFantasyCostumeDatas.Count; i++)
		//{
		//	if (dicCostumeLevelById[liFantasyCostumeDatas[i].EquipmentTable_Id] > 0)
		//	{
		//		int effectlevel = dicCostumeLevelById[liFantasyCostumeDatas[i].EquipmentTable_Id];
		//		EquipmentData tempcostume = liFantasyCostumeDatas[i];
		//		float temp = CGameUtils.getCalCostumeAttributeAttack(tempcostume.Attribute_Atk,
		//			tempcostume.Attribute_Atk * tempcostume.Attribute_Atk_Rate, effectlevel) * 0.01f;

		//		costumeFantasyAttribute += (temp * (tempcostume.Have_Effect * 100));
		//	}
		//}


		//costume = dicCostumeDatasById[equipedCostumeContemporary];
		//level = dicCostumeLevelById[costume.EquipmentTable_Id];
		//costumeContemporaryAttackSpeed = CGameUtils.getCalCostumeSpeed(costume.Speed, costume.Speed_Rate, level);
		//costumeContemporaryAttribute = CGameUtils.getCalCostumeAttributeAttack(costume.Attribute_Atk,
		//	costume.Attribute_Atk * costume.Attribute_Atk_Rate, level);

		//for (int i = 0; i < liContemporaryCostumeDatas.Count; i++)
		//{
		//	if (dicCostumeLevelById[liContemporaryCostumeDatas[i].EquipmentTable_Id] > 0)
		//	{
		//		int effectlevel = dicCostumeLevelById[liContemporaryCostumeDatas[i].EquipmentTable_Id];
		//		EquipmentData tempcostume = liContemporaryCostumeDatas[i];
		//		float temp = CGameUtils.getCalCostumeAttributeAttack(tempcostume.Attribute_Atk,
		//			tempcostume.Attribute_Atk * tempcostume.Attribute_Atk_Rate, effectlevel) * 0.01f;
		//		costumeContemporaryAttribute += (temp * (tempcostume.Have_Effect * 100));
		//	}
		//}

		//onUpdateBattleStatus();
	}



	public void equipmentAutoCostume()
	{
		//double value = 0;
		//int id = 0;
		//for (int i = 0; i < liMurimCostumeDatas.Count; i++)
		//{
		//	EquipmentData data = liMurimCostumeDatas[i];
		//	if (dicCostumeHaveById.ContainsKey(data.EquipmentTable_Id))
		//	{
		//		int level = dicCostumeLevelById[data.EquipmentTable_Id];
		//		if (level == 0)
		//			continue;
		//		float speed = CGameUtils.getCalCostumeSpeed(data.Speed, data.Speed_Rate, level);
		//		if (speed >= value)
		//		{
		//			value = speed;
		//			id = data.EquipmentTable_Id;
		//		}
		//	}
		//}
		//equipedCostumeMurim = id;
		//value = 0;
		//id = 0;

		//for (int i = 0; i < liFantasyCostumeDatas.Count; i++)
		//{
		//	EquipmentData data = liFantasyCostumeDatas[i];
		//	if (dicCostumeHaveById.ContainsKey(data.EquipmentTable_Id))
		//	{
		//		int level = dicCostumeLevelById[data.EquipmentTable_Id];
		//		if (level == 0)
		//			continue;
		//		float speed = CGameUtils.getCalCostumeSpeed(data.Speed, data.Speed_Rate, level);
		//		if (speed >= value)
		//		{
		//			value = speed;
		//			id = data.EquipmentTable_Id;
		//		}
		//	}
		//}
		//equipedCostumeFantasy = id;

		//value = 0;
		//id = 0;
		//for (int i = 0; i < liContemporaryCostumeDatas.Count; i++)
		//{
		//	EquipmentData data = liContemporaryCostumeDatas[i];
		//	if (dicCostumeHaveById.ContainsKey(data.EquipmentTable_Id))
		//	{
		//		int level = dicCostumeLevelById[data.EquipmentTable_Id];
		//		if (level == 0)
		//			continue;
		//		float speed = CGameUtils.getCalCostumeSpeed(data.Speed, data.Speed_Rate, level);
		//		if (speed >= value)
		//		{
		//			value = speed;
		//			id = data.EquipmentTable_Id;
		//		}
		//	}
		//}

		//equipedCostumeContemporary = id;
		//string[] logformat = { "fantasyCostume_", equipedCostumeFantasy.ToString(),
		//	",murimCostume_",equipedCostumeMurim.ToString(),
		//	",ContemporaryCostume_", equipedCostumeContemporary.ToString() };
		//Firebase.Analytics.FirebaseAnalytics.LogEvent(logformat.setStringBuilder());
		//onUpdateCostume();
		//onUpdateBattleStatus();
	}

	public bool hasGoodsById(GoodsId id, double amount)
	{
		double ownAmount = getGoodsById(id);

		switch (id)
		{
			//정수비교
			case GoodsId.Cash:
				return ownAmount >= amount;
			case GoodsId.Gold:
				return ownAmount >= amount;
			default:
				return ownAmount >= amount;
		}
	}


	public void setConsumeItem(string type, int id, double count)
	{
		//switch (type)
		//{
		//	case "Goods":
		//		earnGoodsId((GoodsId)id, -count);
		//		break;
		//	case "Weapon":
		//		dicWeaponHaveById[id] -= int.Parse(count.ToString());
		//		break;
		//	case "Costume":
		//		dicCostumeHaveById[id] -= int.Parse(count.ToString());
		//		break;
		//}
	}
	public double getGoodsById(GoodsId key)
	{
		//switch (key)
		//{
		//	case GoodsId.Cash:
		//		return userGem;
		//	case GoodsId.Gold:
		//		return userGold;
		//	case GoodsId.Attribute_Stone_1:
		//		return Attribute_Stone_1;
		//	case GoodsId.Attribute_Stone_2:
		//		return Attribute_Stone_2;
		//	case GoodsId.Attribute_Stone_3:
		//		return Attribute_Stone_3;
		//	case GoodsId.Option_Stone_1:
		//		return Option_Stone_1;
		//	case GoodsId.SsuckCoin:
		//		return ssuckCoin;
		//	case GoodsId.Skill_Stone_1:
		//		return skill_Stone;

		//	default:
		//		return 0;
		//}

		return 0;
	}

	public void earnMaterialKey(string key, int amount)
	{
		//switch (key)
		//{
		//	case "Attribute_Stone_1":
		//		earnGoodsId(GoodsId.Attribute_Stone_1, amount);
		//		break;
		//	case "Attribute_Stone_2":
		//		earnGoodsId(GoodsId.Attribute_Stone_2, amount);
		//		break;
		//	case "Attribute_Stone_3":
		//		earnGoodsId(GoodsId.Attribute_Stone_3, amount);
		//		break;
		//	case "Option_Stone_1":
		//		earnGoodsId(GoodsId.Option_Stone_1, amount);
		//		break;

		//}
	}

	public void earnGoodsId(GoodsId key, double amount, bool isupdate = true)
	{
		//switch (key)
		//{
		//	case GoodsId.Cash:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, amount);
		//		break;
		//	case GoodsId.Gold:
		//		CUIManager.Instance.onUpdateUserGoods(0, amount, 0,0,0,0,0,0,0,0, isupdate);
		//		break;
		//	case GoodsId.Exp:
		//		CUIManager.Instance.onUpdateUserGoods(amount);
		//		break;
		//	case GoodsId.Attribute_Stone_1:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, 0, 0, 0, amount);
		//		break;
		//	case GoodsId.Attribute_Stone_2:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, 0, 0, 0, 0, amount);
		//		break;
		//	case GoodsId.Attribute_Stone_3:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, 0, 0, 0, 0, 0, amount);
		//		break;
		//	case GoodsId.Option_Stone_1:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, 0, 0, 0, 0, 0, 0, amount);
		//		break;
		//	case GoodsId.Pet_Ticket:
		//		petDungeonTicket += int.Parse(amount.ToString());
		//		if (CUIManager.Instance.actUserGoodsEventListener != null)
		//		{
		//			CUIManager.Instance.actUserGoodsEventListener();
		//		}
		//		break;
		//	case GoodsId.Giant_Ticket:
		//		giantDungeonTicket += int.Parse(amount.ToString());
		//		if (CUIManager.Instance.actUserGoodsEventListener != null)
		//		{
		//			CUIManager.Instance.actUserGoodsEventListener();
		//		}
		//		break;
		//	case GoodsId.SsuckCoin:
		//		ssuckCoin += double.Parse(amount.ToString());
		//		if (CUIManager.Instance.actUserGoodsEventListener != null)
		//		{
		//			CUIManager.Instance.actUserGoodsEventListener();
		//		}
		//		break;

		//	case GoodsId.Damage_Ticket:
		//		damageDungeonTicket += int.Parse(amount.ToString());
		//		if (CUIManager.Instance.actUserGoodsEventListener != null)
		//		{
		//			CUIManager.Instance.actUserGoodsEventListener();
		//		}
		//		break;

		//	case GoodsId.Skill_Stone_1:
		//		CUIManager.Instance.onUpdateUserGoods(0, 0, 0, 0, 0, 0, 0, 0, 0,amount);

		//		break;
				
		//	default:
		//		break;
		//}
	}


	public string getMaterialName(string key)
	{
		//switch (key)
		//{
		//	case "Attribute_Stone_1":
		//		 return getGoodsName(GoodsId.Attribute_Stone_1);
		//	case "Attribute_Stone_2":
		//		return getGoodsName(GoodsId.Attribute_Stone_2);
		//	case "Attribute_Stone_3":
		//		return getGoodsName(GoodsId.Attribute_Stone_3);
		//	case "Option_Stone_1":
		//		return getGoodsName(GoodsId.Option_Stone_1);
		//}
		return "";

	}

	public string getGoodsName(GoodsId key)
	{
		//switch (key)
		//{
		//	case GoodsId.Cash:
		//		return "보석";
		//	case GoodsId.Gold:
		//		return "골드";
		//	case GoodsId.Attribute_Stone_1:
		//		return "판타지석";
		//	case GoodsId.Attribute_Stone_2:
		//		return "무협석";
		//	case GoodsId.Attribute_Stone_3:
		//		return "현대석";
		//	case GoodsId.Option_Stone_1:
		//		return "옵션석";
		//	case GoodsId.Pet_Ticket:
		//		return "펫 던전 티켓";
		//	case GoodsId.Giant_Ticket:
		//		return "거인 던전 티켓";
		//	case GoodsId.SsuckCoin:
		//		return "썩코인";
		//	case GoodsId.Damage_Ticket:
		//		return "용가리 던전 티켓";
		//	case GoodsId.Skill_Stone_1:
		//		return "스킬석";
		//	default:
		//		return "";
		//}
		return "";
	}
	public string getGoodsResourceName(GoodsId key)
	{
		//switch (key)
		//{
		//	case GoodsId.Cash:
		//		return "dia";
		//	case GoodsId.Gold:
		//		return "slimecoin";
		//	case GoodsId.Exp:
		//		return "EXP";
		//	case GoodsId.Attribute_Stone_1:
		//		return "Attribute_Stone_1";
		//	case GoodsId.Attribute_Stone_2:
		//		return "Attribute_Stone_2";
		//	case GoodsId.Attribute_Stone_3:
		//		return "Attribute_Stone_3";
		//	case GoodsId.Option_Stone_1:
		//		return "Option_Stone_1";
		//	case GoodsId.Pet_Ticket:
		//		return "PetDungeon_Ticket";
		//	case GoodsId.Giant_Ticket:
		//		return "GiantDungeon_Ticket";
		//	case GoodsId.SsuckCoin:
		//		return "Ssuck_Coin";
		//	case GoodsId.Damage_Ticket:
		//		return "DmgDungeon_Ticket";
		//	case GoodsId.Skill_Stone_1:
		//		return "Skill_Stone_1";

		//	default:
		//		return "";
		//}

		return "";
	}

	public float getAttributeResetTime()
    {
		float value = constData.Attribute_ResetTime;
		return value;
	}
	public void setCostumeGrade(int exp)
	{
		//costumeGotchaExp += exp;
		//if (costumeGotchaExp >= (dicGotchaGradeById[costumeGrade].Gotcha_Val/5))
		//{
		//	costumeGotchaExp = costumeGotchaExp - (dicGotchaGradeById[costumeGrade].Gotcha_Val/5);
		//	if (dicGotchaGradeById.Count > costumeGrade)
		//	{
		//		costumeGrade++;
		//	}
		//}
	}

	public void onUpdateCostume(List<int> list)
	{
		//isNewEquipment = false;
		//for (int i = 0; i < list.Count; i++)
		//{
		//	if (dicCostumeLevelById.ContainsKey(list[i]))
		//	{
		//		if (1 > dicCostumeLevelById[list[i]])
		//		{
		//			dicCostumeLevelById[list[i]] = 1;
		//		}
		//	}
		//	else
		//	{
		//		dicCostumeLevelById.Add(list[i], 1);
		//	}
		//	if (dicCostumeHaveById.ContainsKey(list[i]))
		//	{
		//		dicCostumeHaveById[list[i]]++;
		//		CUIManager.Instance.onEnableNoticeIcon("Equipment", true);
		//	}
		//	else
		//	{
		//		dicCostumeHaveById.Add(list[i], 1);
		//		isNewEquipment = true;
		//	}
		//}
		//CUIManager.Instance.onStartIndigator();
		//CInGameDataManager.Instance.onUpdateEquipment(false, () => {
		//	CUIManager.Instance.onStopIndigator();
		//});
	}

	public void onUpdateTreasure(List<int> list)
	{
		//isNewEquipment = false;
		//for (int i = 0; i < list.Count; i++)
		//{
		//	if (dicTreasureHaveById.ContainsKey(list[i]))
		//	{
		//		dicTreasureHaveById[list[i]]++;
		//	}
		//	else
		//	{
		//		dicTreasureHaveById.Add(list[i], 1);
		//		isNewEquipment = true;
		//	}
		//}
		//CUIManager.Instance.onEnableNoticeIcon("Relic", true);
		//CUIManager.Instance.onStartIndigator();
		//CInGameDataManager.Instance.onUpdateEquipment(false, () => {
		//	CUIManager.Instance.onStopIndigator();
		//});
	}

	private bool resetSet(string type,string[] types, string[] ids,int id)
    {
		List<int> weaponlist = new List<int>();
		List<int> costumelist = new List<int>();
		bool result = false;
		for (int i = 0; i < types.Length; i++)
		{
			if (types[i] == "weapon")
				weaponlist.Add(int.Parse(ids[i]));
			else
				costumelist.Add(int.Parse(ids[i]));
		}
		if (type == "weapon")
		{
			if (!weaponlist.Contains(id))
				result = false;
            else
				result = true;
		}
		else
		{
			if (!costumelist.Contains(id))
				result = false;
			else
				result = true;
		}
		return result;
	}


	public float getRandomEffect(string name)
    {
		//float murimvalue = 0;
		//float fantasyvalue = 0;
		//float contemporaryvalue = 0;
		//if (equipedSetMurim > 0)
		//      {
		//          for (int i = 0; i < dicRandomEffectById[equipedSetMurim].Count; i++)
		//          {
		//		if (dicRandomEffectById[equipedSetMurim][i] > 0)
		//              {
		//			RandomEffectData data = dicRandomEffectDatasById[dicRandomEffectById[equipedSetMurim][i]];
		//			if (data.RandomEffect == name)
		//			{
		//				murimvalue += data.RandomEffect_Val;
		//			}
		//		}			
		//	}
		//	murimvalue *= dicSetLevelById[equipedSetMurim];			
		//}

		//if (equipedSetFantasy > 0)
		//{
		//	for (int i = 0; i < dicRandomEffectById[equipedSetFantasy].Count; i++)
		//	{
		//		if(dicRandomEffectById[equipedSetFantasy][i] > 0)
		//              {
		//			RandomEffectData data = dicRandomEffectDatasById[dicRandomEffectById[equipedSetFantasy][i]];
		//			if (data.RandomEffect == name)
		//			{
		//				fantasyvalue += data.RandomEffect_Val;
		//			}
		//		}			
		//	}
		//	fantasyvalue *= dicSetLevelById[equipedSetFantasy];

		//}
		//if (equipedSetContemporary > 0)
		//{
		//	for (int i = 0; i < dicRandomEffectById[equipedSetContemporary].Count; i++)
		//	{
		//		if (dicRandomEffectById[equipedSetContemporary][i] > 0)
		//              {
		//			RandomEffectData data = dicRandomEffectDatasById[dicRandomEffectById[equipedSetContemporary][i]];
		//			if (data.RandomEffect == name)
		//			{
		//				contemporaryvalue += data.RandomEffect_Val;						
		//			}
		//		}			
		//	}
		//	contemporaryvalue *= dicSetLevelById[equipedSetContemporary];

		//}
		//return murimvalue + fantasyvalue + contemporaryvalue;

		return 0;
	}

}

