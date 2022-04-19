using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyExcel;
using EasyExcelGenerated;

public class LoadData : MonoBehaviour
{
    private readonly EEDataManager _eeDataManager = new EEDataManager();

    // Start is called before the first frame update
    void Start()
    {
        _eeDataManager.Load();
        FindDataExample();
    }

	private void FindDataExample()
	{

		// Get EasyExcelGenerated.KeyColumn with string-type key
		// You can specify a column in a sheet as key with Your_Column_Name:Key.
		//var keyColumn = _eeDataManager.Get<Character>("Name");
		//Debug.Log(keyColumn.Description);

		Debug.Log("test");
		
		// Get EasyExcelGenerated.MultiSheet01 with int-type key
		//var multiSheet01 = _eeDataManager.Get<EasyExcelGenerated.MultiSheet01>(1001);
		//Debug.Log(multiSheet01.Description);

		// Get EasyExcelGenerated.KeyColumn list
		//List<EasyExcelGenerated.KeyColumn> list = _eeDataManager.GetList<EasyExcelGenerated.KeyColumn>();
		//foreach (var data in list)
		//{
		//	Debug.Log(data.Icon);
		//}
		

	}
}
