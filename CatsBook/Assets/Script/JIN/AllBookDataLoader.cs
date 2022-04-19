using System;
using System.Collections.Generic;
using System.Linq;
using EasyExcel;
using UnityEngine;

/// <summary>
/// Examples of customDataLoader
/// 演示自定义数据加载器（load from Resources or AssetBundle）
/// 示例数据表来自EasyExcel\Example\ExcelFiles
/// </summary>
public class AllBookDataLoader : MonoBehaviour
{
	private EEDataManager _eeDataManager;

	 public JINtest_book test;

	private void Start()
	{
		// use ExampleDataLoaderResources as loader
		// 从Resources里加载数据
		_eeDataManager = new EEDataManager(new ExampleDataLoaderResources());
		
		// use ExampleDataLoaderAssetBundle as loader
		// 从AssetBundle里加载数据
		//_eeDataManager = new EEDataManager(new ExampleDataLoaderAssetBundle());
		
		// Set CustomDataLoader before load.
		// 先用自定义loader创建EEDataManager，后执行Load
		_eeDataManager.Load();
		  FindDataExample();
	 }

	 private void FindDataExample()
	 {
		  for (int i = 0; i < 40; i++)
		  {
			   BookStorage.Instance.book_Dict.Add(i, new List<Book>());
		  }

		  List<EasyExcelGenerated.AllBookDB> list = _eeDataManager.GetList<EasyExcelGenerated.AllBookDB>();
		  for(int i = 0; i < list.Count; i++)
          {
			   AllBookDB.Instance.AllBookdb[i] = new Book(list[i].BookName, list[i].Genre, list[i].Contents);

			   //AllBookDB.ALLBOOK_DICT.Add(list[i].Genre, new Book(list[i].BookName, list[i].Genre, list[i].Contents));
          }

		  OldBookDBSetting();


		  test.get_bookstest();

		  // ------------Uncomment below after importing example files------------
		  // ------------取消下面的注释需要先运行完Tools/EasyExcel/Import导入完示例文件！------------
		  /*

		  // Get EasyExcelGenerated.KeyColumn with string-type key
		  // You can specify a column in a sheet as key with Your_Column_Name:Key.
		  var keyColumn = _eeDataManager.Get<EasyExcelGenerated.KeyColumn>("Brigand");
		  Debug.Log(keyColumn.Description);

		  // Get EasyExcelGenerated.MultiSheet01 with int-type key
		  var multiSheet01 = _eeDataManager.Get<EasyExcelGenerated.MultiSheet01>(1001);
		  Debug.Log(multiSheet01.Description);

		  // Get EasyExcelGenerated.KeyColumn list
		  List<EasyExcelGenerated.KeyColumn> list = _eeDataManager.GetList<EasyExcelGenerated.KeyColumn>();
		  foreach (var data in list)
		  {
			  Debug.Log(data.Icon);
		  }

		  */
	 }

	 private void OldBookDBSettingModule(string GenreName)
     {
		  List<Book> X = AllBookDB.Instance.AllBookdb.Where(x => x.Genre.Equals(GenreName)).ToList();
		  if (X.Count > 0)
		  {
			   for (int i = 0; i < X.Count; i++)
			   {
					AllBookDB.Instance.special_situation_book.OldBook_Genre_Book.Add(X[i]);
			   }
		  }
	 }

	 private void OldBookDBSetting()
     {
		  OldBookDBSettingModule("시");
		  OldBookDBSettingModule("소설");
		  OldBookDBSettingModule("수필");
		  OldBookDBSettingModule("가정/요리/취미");
		  OldBookDBSettingModule("어린이");
		  OldBookDBSettingModule("사회과학");
		  OldBookDBSettingModule("청소년");
		  OldBookDBSettingModule("고전");
		  OldBookDBSettingModule("외국어");

	 }


	 // Load from Resources.Load. 从Resources里加载。
	 private class ExampleDataLoaderResources : IEEDataLoader
	{
		public EERowDataCollection Load(string sheetClassName)
		{
			var headName = sheetClassName;
			var filePath = EESettings.Current.GeneratedAssetPath.
				               Substring(EESettings.Current.GeneratedAssetPath.IndexOf("Resources/", StringComparison.Ordinal) + "Resources/".Length)
			               + headName;
			var collection = Resources.Load(filePath) as EERowDataCollection;
			return collection;
		}
	}
	
	// Load from AssetBundle. 从AssetBundle里加载。
	private class ExampleDataLoaderAssetBundle : IEEDataLoader
	{
		public EERowDataCollection Load(string sheetClassName)
		{
			// Your AssetBundle file path
			var bundlePath = Application.persistentDataPath + "/***Your AssetBundle File Name***";
			// Your AssetBundle file path
			var assetPath = "Assets/ExampleFolder/" + sheetClassName + ".asset";
			var bundle = AssetBundle.LoadFromFile(bundlePath);
			var collection =  bundle.LoadAsset(assetPath) as EERowDataCollection;
			return collection;
		}
	}


	#region 演示示例结果用，不必读。Just for showing the data. You do not have to read these
	
	private void OnGUI()
	{
		//ExampleLoadData.gui(_eeDataManager);
	}
	
	#endregion
	
}