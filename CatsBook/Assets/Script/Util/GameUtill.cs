using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using LitJson;
using System.Text.RegularExpressions;
//using BackEnd;

public enum ParameterValueType
{
    Normal = 0,
    Percet,
    Second
}

namespace GameUtils
{
    public static class CGameUtils 
    {
        private const int rondomCount = 1000;
        private static int m_nIndex = 0;
        private static int[] m_nArr = new int[rondomCount];
        private static Transform popuptrs = null;
        const string Zero = "0";
        public enum CurrencyType { Default, SI, }
        static readonly string[] CurrencyUnits = new string[]
        {
            "", "만", "억", "조", "경", "해", "썩",// "자", "양", "구", "간", "정", "재", "극",  "항하사", "아승기", "나유타", "불가사의", "무량대수",
            "쏘","당","썩쏘", "썩쏘당", "썩쏘당당", "썩쏘당당당", "썩쏘당당당당", "썩쏘당당당당당", "썩쏘당당당당당당", "썩쏘당당당당당당당", "썩쏘당당당당당당당당",
            "썩쏘당당당당당당당당당", "썩쏘당당당당당당당당당당", "썩쏘염", "썩쏘염염", "썩쏘염염염", "썩쏘염염염염", "썩쏘염염염염염", "썩쏘염염염염염염",
            "썩쏘염염염염염염염", "썩쏘염염염염염염염염", "썩쏘염염염염염염염염염", "썩쏘염염염염염염염염염염","썩쏘염당", "썩쏘염당당", "썩쏘염당당당",
            "썩쏘염당당당당", "썩쏘염당당당당당", "썩쏘염당당당당당당","썩쏘염당당당당당당당", "썩쏘염당당당당당당당당", "썩쏘염당당당당당당당당당당",
            "썩쏘키", "썩쏘키우", "썩쏘키우기", "썩쏘키우기당", "썩쏘키우기당당", "썩쏘키우기당당당", "썩쏘키우기당당당당", "썩쏘키우기당당당당당", "썩쏘키우기당당당당당당",
            "썩쏘키우기당당당당당당당", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO",
            "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ", "CA", "CB", "CC", "CD", "CE",
            "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU",
            "CV", 
        };
        //1,000,000,000,000
        static readonly string[] SI = new string[] { };

        static CGameUtils()
        {
            for (int i = 0; i < rondomCount; ++i)
            {
                m_nArr[i] = UnityEngine.Random.Range(0, 10000);
            }
        }


        private static int nIndex
        {
            get
            {
                int nTemp = m_nIndex++;

                if (m_nIndex >= rondomCount) { m_nIndex = 0; }

                return nTemp;
            }
        }

        static Dictionary<uint, WaitForSeconds> m_dicMapTime = new Dictionary<uint, WaitForSeconds>();

        public static WaitForSeconds WaitForSeconds(float a_fSeconds)
        {
            if (a_fSeconds < 0.0f) { Debug.Log("arg error"); return null; }

            uint nVal = 1;

            if (a_fSeconds >= 0.001f)
            {
                nVal = (uint)(a_fSeconds * 1000);
            }
            return WaitForMilliSeconds(nVal);
        }

        public static WaitForSeconds WaitForMilliSeconds(uint a_nMilliSecond)
        {
            if (m_dicMapTime.TryGetValue(a_nMilliSecond, out WaitForSeconds instance) == false)
            {
                instance = new WaitForSeconds(a_nMilliSecond / (float)1000);
                m_dicMapTime.Add(a_nMilliSecond, instance);
            }
            return instance;
        }

        public static string ToStringData(this List<int> list, string splitChar)
        {
            string toData = string.Join(splitChar, list.ToArray());
            return toData; 
        }

        public static List<int> getParseToIntList(this string dataStr, char splitChar)
        {
            List<int> dataList = new List<int>();
            string[] splitedDatas = dataStr.Split(splitChar);
            for(int i=0; i< splitedDatas.Length; i++)
            {
                string data = splitedDatas[i];
                dataList.Add(int.Parse(data));
            }
            return dataList;
        }

        public static List<T> GetShuffleList<T>(List<T> _list)
        {
            System.Random rng = new System.Random();
            int n = _list.Count;

            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = _list[k];
                _list[k] = _list[n];
                _list[n] = value;              
            }

            return _list;

        }

        //public static List<int> getRandomList(List<int> lipercent, List<string> list,int count)
        //{
        //    int index = 0;
        //    int num = 0;
        //    List<int> templist = new List<int>();
        //    while (count > index)
        //    {
        //        Debug.Log("num == " + num);
        //        bool isbool = getPercent(lipercent[num]);
        //        if (isbool)
        //        {
        //            string[] split = list[num].Split(',');
        //            int random = UnityEngine.Random.Range(0, split.Length);
        //            templist.Add(random);
        //            index++;
        //            num = 0;
        //            continue;
        //        }
        //        num++;
        //        if (num > count)
        //        {
        //            num = 0;
        //        }
        //    }
        //    return templist;
        //}

        public static int getRandom() { return m_nArr[nIndex]; }

        public static float getPercent() { return m_nArr[nIndex] * 0.0001f; }

        public static bool getPercent(float a_nPercent)
        {
            return m_nArr[nIndex] <= (a_nPercent * 100);
        }
        
        public static bool getRomusaeKintPercent(float a_nPercent, float artRomusaeBossPercent)
        {
            float result = (a_nPercent + artRomusaeBossPercent) * 0.01f;
            return m_nArr[nIndex] <= (result * 100);
        }

        public static string setStringBuilder(this string[] str)
        {
            StringBuilder sc = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                sc.Append(str[i]);
            }
            return sc.ToString();
        }

        public static string setCommaNumber(float value, string str = "")
        {
            string[] temp = new string[2];
            temp[0] = string.Format("{0:#,###}", value);
            temp[1] = str;
            string result = setStringBuilder(temp);
            return result;
        }

        public static string ToCurrencyString(this double number, CurrencyType currencyType = CurrencyType.Default)
        {
            if (-1d < number && number < 1d)
            {
                return Zero;
            }
            if (true == double.IsInfinity(number))
            {
                return "Infinity";
            }
            //string significant = (number < 0) ? "-" : string.Empty;
            string showNumber = string.Empty;
            string unitString = string.Empty;
            string[] partsSplit = number.ToString("E").Split('+');
            if (partsSplit.Length < 2)
            {
                UnityEngine.Debug.LogWarning(string.Format("Failed - ToCurrencyString({0})", number)); return Zero;
            }

            if (false == int.TryParse(partsSplit[1], out int exponent))
            {
                UnityEngine.Debug.LogWarning(string.Format("Failed - ToCurrencyString({0}) : partsSplit[1] = {1}", number, partsSplit[1]));
                return Zero;
            }

            int quotient = exponent / 4;
            int remainder = exponent % 4;
            if (exponent < 4)
            {
                showNumber = Math.Truncate(number).ToString();
            }
            else
            {
                var temp = double.Parse(partsSplit[0].Replace("E", "")) * Math.Pow(10, remainder);
                showNumber = temp.ToString("F").Replace(".00", "");
            }

            if (currencyType == CurrencyType.Default)
            {
                unitString = CurrencyUnits[quotient];
            }
            else
            {
                unitString = SI[quotient];
            }

            return string.Format("{0}{1}", showNumber, unitString);// significant,
        }

        public static double ToCurrencyDouble(this string currencyString, CurrencyType stringType = CurrencyType.Default)
        {
            double result = 0;
            bool isNumber = double.TryParse(currencyString, out result);
            if (true == isNumber)
            {
                return result;
            }
            else
            {
                int length = currencyString.Length; int lastNumberIndex = -1;
                for (int i = length - 1; 0 <= i; --i)
                {
                    if (true == char.IsNumber(currencyString, i))
                    {
                        lastNumberIndex = i;
                        break;
                    }
                }
                if (lastNumberIndex < 0)
                {
                    throw new Exception("Failed currency string");
                }
                string number = currencyString.Substring(0, lastNumberIndex + 1);
                string unit = currencyString.Substring(lastNumberIndex + 1);
                int index = (CurrencyType.Default == stringType) ? Array.FindIndex(CurrencyUnits, p => p == unit) : Array.FindIndex(SI, p => p == unit);
                if (-1 == index)
                {
                    throw new Exception("Failed currency string");
                }
                string exponentNumber = string.Format("{0}E+{1}", number, index * 3);
                return double.Parse(exponentNumber);
            }
        }

        public static string prettySecondsTimeFormat(int seconds)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(seconds);
            string[] strtime = timespan.ToString().Split('.');
            return strtime[0];
        }

        public static double getSliceDouble(this double self, int n)
        {
            int sliceNumber = (int)Math.Pow(10, n);
            return (Math.Truncate(self * sliceNumber) / sliceNumber);
        }
           

        public static Color copy(this Color color, float? r = null, float? g = null, float? b = null, float? a = null)
        {
            return new Color(r ?? color.r, g ?? color.g, b ?? color.b, a ?? color.a);
        }

        public static DateTime getServerDateTime()
        {
            DateTime serverDate = DateTime.Parse(DateTime.Now.ToString());
            //Backend.Utils.GetServerTime((servertime) => {
            //    string utctime = servertime.GetReturnValuetoJSON()["utcTime"].ToString();
            //    Debug.Log("utctime = " + utctime);
            //    serverDate = DateTime.Parse(utctime);
            //    serverDate = serverDate.ToUniversalTime().AddHours(9);
            //});         
            return serverDate;
        }

        public static double getRemainTime(string key)
        {
            double result;

            string datetime = PlayerPrefs.GetString(key);
            DateTime goaltime = Convert.ToDateTime(datetime);
            DateTime currenttime = getServerDateTime();
            TimeSpan resulttime = goaltime - currenttime;

            result = resulttime.TotalSeconds;
            return result;
        }

        public static DateTime getGoalTime(float time)
        {
            DateTime applytime;
            DateTime current = getServerDateTime();
            applytime = current.AddSeconds(time);
            return applytime;
        }

        public static DateTime getCalDayDateTime(int resettime, DateTime currentdate)
        {
            DateTime date;
            int hour = currentdate.Hour;
            int min = currentdate.Minute;
            int sec = currentdate.Second;

            int latehour = 0;
            int latemin = 0;
            int latesec = 0;
            int resultsec = 0;

            if (resettime > hour)
            {
                latehour = resettime - (hour + 1);
                latemin = 60 - min;
            }
            else
            {
                latehour = resettime + (23 - hour);
                latemin = 60 - min;
            }

            latesec = 60 - sec;
            resultsec = ((latehour * 3600) + (latemin * 60) + latesec) - 60;
            date = currentdate.AddSeconds(resultsec);
            return date;
        }


        public static int getCalSecond(int resettime, DateTime currentdate)
        {
            int hour = currentdate.Hour;
            int min = currentdate.Minute;
            int sec = currentdate.Second;

            int latehour = 0;
            int latemin = 0;
            int latesec = 0;
            int resultsec = 0;

            if (resettime > hour)
            {
                latehour = resettime - (hour + 1);
                latemin = 60 - min;
            }
            else
            {
                latehour = resettime + (23 - hour);
                latemin = 60 - min;
            }

            latesec = 60 - sec;
            resultsec = ((latehour * 3600) + (latemin * 60) + latesec) - 60;
            return resultsec;
        }

        public static bool checkNickName(string _name)
        {
            return Regex.IsMatch(_name, "^[0-9a-zA-Z가-힣]*$");
        }


        //펫 레벨업 공격력 공식 || 펫 스킬 레벨시 능력 공식
        public static float getCalPetStatLevel(float start , float increase, int level)
        {
            if (level == 0)
                return 0;
            float result = start + (increase * level);
            return result;
        }

        public static double getCalPetGoldSum(int level)
        {
            double result = (((double.Parse(level.ToString()) - 1) / 2) * 10 + 10) * (double.Parse(level.ToString()) * 2000);
            result = Math.Round(result);
            return result;
        }

        public static double getCalPetSkillSum(int level)
        {
            double result = (((level - 1) / 10) * 50 + 100) * level;
            result = Math.Round(result);
            return result;
        }

        //펫 레벨업 비용 공식 || 펫 스킬 레벨업 비용 공식 
        public static double getCalPetStatLevelSum(int startvalue, float value,int level)
        {
            double result = startvalue + (startvalue * (value * Math.Pow(level, 2)));
            result = Math.Round(result);
            return result;
        }

        //캐릭터 스텟 비용 공식 
        public static float getCalCharStatLevel(double start, float increase, int level)
        {
            double result = start + (increase * level);
            result = Math.Round(result, 4);//Math.Round(result, 4);
            return float.Parse(result.ToString());
        }


        //캐릭터 스텟 비용 공식 
        public static double getCalCharStatLevel(double start, float increase, float A, float B, float C, int level, int round = 2)
        {
            if (A == 0 && B == 0 && C == 0)
            {
                return start + (increase * level);
            }

            double result = start + ((A * Mathf.Pow(level, 3)) - (B * Mathf.Pow(level, 2)) + (C * level));
            result = Math.Round(result, round); //Math.Round(result, round);
            return result;
        }

        //캐릭터 스텟 비용 공식 
        public static double getCalCharStatLevelSum(double startgold,float goldA, float goldB, float goldC,int level,int round)
        {
            double result = startgold + ((goldA * Math.Pow(level, 3)) - (goldB * Math.Pow(level, 2)) + (goldC * level));
            result = Math.Round(result,round);
            return result;
        }

        //장비 공격력
        public static double getCalWeaponAttack(double attack ,float attackrate,int level)
        {
            if(level == 0)
                level = 1;

            double result = Math.Round(attack + ((level - 1) * attack * attackrate));
            return result;
        }

        //장비 공격속도
        public static float getCalCostumeSpeed(float speed, float speedrate, int level)
        {
            if (level == 0)
                level = 1;
            double result = Math.Round(speed + ((level-1)*speedrate) ,4);
            return (float)result;
        }

        //장비 공격속도
        public static float getCalCostumeAttributeAttack(float attack, float attackrate, int level)
        {
            if (level == 0)
                level = 1;
            double result = Math.Round(attack + ((level - 1) * attackrate), 4);
            return (float)result;
        }

        //장비 업그레이드 비용
        public static double getCalUpgradeGoldPrice(double price, float rate, int level)
        {
            double result = Math.Round(price + ((level - 1) * price * rate));
            return result;
        }

        //장비 업그레이드 비용
        public static double getCalUpgradeGemPrice(double price, int level,double increase)
        {
            double result = Math.Round(price + ((level - 1) * increase));
            return result;
        }

        //장비 업그레이드 확률
        public static float getCalUpgradePercent2(float limit, float decreaserate, int level)
        {
            float result = 10f;
            for (int i = 0; i < level; i++)
            {
                result -= decreaserate;
            }

            result = float.Parse(Math.Truncate(result).ToString());
            if (limit > result)
            {
                result = limit;
            }
            return result;
        }

        //장비 업그레이드 확률
        public static float getCalUpgradePercent(float limit,float decreaserate, int level)
        {
            float result = 100f;
            for (int i = 0; i < level; i++)
            {
                result -= decreaserate;
            }

            result = float.Parse(Math.Truncate(result).ToString());
            if (limit > result)
            {
                result = limit;
            }
            return result;
        }


        public static double getCalDoubleRelicPassive(int defaultvalue, float increase, int level)
        {
            if (level == 0)
                return 0;
            double result = double.Parse(Math.Round((defaultvalue + (increase * (level))), 5).ToString());

            return result;
        }

        public static float getCalRelicPassive(int defaultvalue,float increase,int level)
        {
            if (level == 0)
                return 0;
            float result = float.Parse(Math.Round((defaultvalue + (increase * (level))),5).ToString());

            return result;
        }

    
        public static Color32 getGradeColor(string name) {

            Color32 color = new Color32();
            if (name == "RRR" || name == "RR" || name == "R")
            {
                color = new Color32(255, 87, 174, 255);
                return color;
            }
            else if (name == "UUU" || name == "UU" || name == "U")
            {
                color = new Color32(255, 208, 0, 255);
                return color;
            }
            else if (name == "LLL" || name == "LL" || name == "L")
            {
                color = new Color32(230, 0, 18, 255);
                return color;
            }
            else if (name == "GGG" || name == "GG" || name == "G")
            {
                color = new Color32(0, 128, 224, 255);
                return color;
            }
            else if (name == "SSS" || name == "SS" || name == "S")
            {
                color = new Color32(166, 223, 255, 255);
                return color;
            }
            else if (name == "AAA" || name == "AA" || name == "A")
            {
                color = new Color32(101, 193, 96, 255);
                return color;
            }
            else
            {
                color = new Color32(186, 186, 186, 255);
                return color;
            }            
        }


        public static string getGradeResName(string name)
        {
            string resname = "";
            if (name == "RRR" || name == "RR" || name == "R")
            {
                resname = "equipmentboxborder_hotpink_small";
                return resname;
            }
            else if (name == "UUU" || name == "UU" || name == "U")
            {
                resname = "equipmentboxborder_gold_small";
                return resname;
            }
            else if (name == "LLL" || name == "LL" || name == "L")
            {
                resname = "equipmentboxborder_red_small";
                return resname;
            }
            else if (name == "GGG" || name == "GG" || name == "G")
            {
                resname = "equipmentboxborder_blue_small";
                return resname;
            }
            else if (name == "SSS" || name == "SS" || name == "S")
            {
                resname = "equipmentboxborder_sky_small";
                return resname;
            }
            else if (name == "AAA" || name == "AA" || name == "A")
            {
                resname = "equipmentboxborder_green_small";
                return resname;
            }
            else
            {
                resname = "equipmentboxborder_gray_small";
                return resname;
            }
        }

        public static void isServerStatus(Action actcallback = null)
        {
            //Backend.Utils.GetServerStatus((callback) =>
            //{
            //    if (callback.IsSuccess())
            //    {
            //        int stauts = int.Parse(callback.GetReturnValuetoJSON()[0].ToString());//int.Parse(rows["serverStatus"]["N"].ToString());
            //        if (stauts == 2)
            //        {
            //            if (popuptrs == null)
            //            {
            //                popuptrs = GameObject.Find("Canvas").transform;
            //            }
            //            if (actcallback != null)
            //            {
            //                actcallback();
            //            }
            //            PlayerPrefs.DeleteAll();
            //            COneButtonPopup popup = CPopupManager.Instance.openPopup(popuptrs, "LoginOneButtonPopup").GetComponent<COneButtonPopup>();
            //            popup.onUpdateInfo("점검중입니다.", () =>
            //            {
                            
            //                Application.Quit();
            //            });
            //        }
            //        else if (stauts == 1)
            //        {
            //            PlayerPrefs.DeleteAll();
            //            CInGameDataManager.Instance.onUpdateUserInfo();
            //            CInGameDataManager.Instance.onUpdateAchievementInfo();
            //        }
            //    }
            //});
        }


        public static void isClinetVersion()
        {
        //    BackendReturnObject versionBackend = Backend.Utils.GetLatestVersion();
        //    //버전 컨트롤

        //    //#if !UNITY_EDITOR
        //    if (versionBackend.IsSuccess())
        //    {
        //        JsonData rows = versionBackend.GetReturnValuetoJSON();
        //        string clientServerVersion = rows["version"].ToString();
        //        int clientUpdateStatus = int.Parse(rows["type"].ToString());
        //        string[] clientServerVersionCut = clientServerVersion.Split('.');
        //        CGameManager.Instance.isClientUpdate = false;
        //        if (clientUpdateStatus.Equals(2))
        //        {
        //            string[] clientLocalVersionCut = CGameManager.Instance.version.Split('.');

        //            for (int i = 0; i < clientLocalVersionCut.Length; ++i)
        //            {
        //                if (int.Parse(clientServerVersionCut[i]) > int.Parse(clientLocalVersionCut[i]))
        //                {
        //                    if (popuptrs == null)
        //                    {
        //                        popuptrs = GameObject.Find("Canvas").transform;
        //                    }
        //                    CGameManager.Instance.isClientUpdate = true;
        //                    PlayerPrefs.DeleteAll();
        //                    COneButtonPopup popup = CPopupManager.Instance.openPopup(popuptrs, "OneButtonPopup").GetComponent<COneButtonPopup>();
        //                    popup.onUpdateInfo("업데이트를 위해 스토어로 이동하세요!", () => {
        //                        Application.OpenURL("market://details?id=com.dwgames.ssucksso");
        //                        Application.Quit();
        //                    });

        //                    return;
        //                }
        //                else if (int.Parse(clientServerVersionCut[i]) < int.Parse(clientLocalVersionCut[i]))
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Transform popuptrs = null;
        //        popuptrs = GameObject.Find("Canvas").transform;

        //        COneButtonPopup popup = CPopupManager.Instance.openPopup(popuptrs, "LoginOneButtonPopup").GetComponent<COneButtonPopup>();
        //        popup.onUpdateInfo("서버 통신이 원활하지 않으니 재접속 부탁드립니다.", () =>
        //        {
        //            Application.Quit();
        //        });
        //        return;
        //    }
        }
    }
}

