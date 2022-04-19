using System;
using UnityEngine;

public class CInGameDataManager : MonoBehaviour
{
    private static CInGameDataManager instance = null;

    public static CInGameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(CInGameDataManager)) as CInGameDataManager;
                if (instance == null)
                {
                    instance = new GameObject("CInGameDataManager", typeof(CInGameDataManager)).GetComponent<CInGameDataManager>();
                }
            }
            return instance;
        }
    }



	protected virtual void Start()
    {
		instance = this;

		resetServerData(() =>
		{

		}, true);

	}

	public void resetServerData(Action _callback = null, bool isfirst = false)
    {

    }




	//var assembly = EEUtility.GetSourceAssembly();
	//var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EERowDataCollection)));
	//var sheetClassTypes = types as Type[] ?? types.ToArray();
	//	if (!sheetClassTypes.Any())


 //   			foreach (var sheetClassType in sheetClassTypes)
	//		{
	//			var collectClassName = sheetClassType.FullName;
	///*var headNameRaw =
	//	collectClassName.Substring(0, collectClassName.IndexOf(EESettings.Current.SheetDataPostfix, StringComparison.Ordinal));
	//var headParts = headNameRaw.Split('.');*/
	//var headParts = collectClassName.Split('.');
	//var fileName = headParts.Length == 1 ? null : headParts[0].Substring(EESettings.Current.NameSpacePrefix.Length);
	//var sheetClassName = headParts.Length == 1 ? headParts[0] : headParts[1];
	//var sheetName = EESettings.Current.GetSheetName(sheetClassType);
	//var rowDataClassName = EESettings.Current.GetRowDataClassName(fileName, sheetName, true);
	//var rowType = assembly.GetType(rowDataClassName);
	//var dic = eeDataManager.GetList(rowType);
	//var rowDataClassNameShort = EESettings.Current.GetRowDataClassName(fileName, sheetName, false);
	//GUI.Label(new Rect(30, labelBottom + typesIndex* 20, 380, 20), string.Format("Sheet Class: <color=#569CD6>{0}</color>", sheetClassName));
	//			GUI.Label(new Rect(410, labelBottom + typesIndex* 20, 250, 20), string.Format("RowData Class: <color=#569CD6>{0}</color>", rowDataClassNameShort));
	//			GUI.Label(new Rect(660, labelBottom + typesIndex* 20, 200, 20), string.Format("Rows: <color=#569CD6>{0}</color>", dic != null ? dic.Count.ToString() : "empty"));
	//			typesIndex++;
	//		}
    
}
