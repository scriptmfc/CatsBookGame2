using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExteriorItem : MonoBehaviour
{
    [SerializeField] Text itemPrice;
    [SerializeField] Image itemImage;
    [SerializeField]  Transform goodsType;

    [SerializeField]  Button purchaseButton;
    private FurnitureChartData exteriorData;
    private bool isInit = false;

    public void setDataInfo(FurnitureChartData _data)
    {
        exteriorData = _data;
        isInit = true;

        //itemName.text = _data.name;
        itemPrice.text = _data.price.ToString();

        string where = "Furniture/";
        where += _data.attribute + "/";
        where += _data.name;

        itemImage.sprite = Resources.Load<Sprite>(where);


        if(_data.goodsType == GoodsId.Cash)
        {
            goodsType.GetChild(0).gameObject.SetActive(true);
        }
        else if(_data.goodsType == GoodsId.Gold)
        {
            goodsType.GetChild(1).gameObject.SetActive(true);
        }

        onUpdateInfo();
    }

    private void OnEnable()
    {
        if (isInit)
        {
            onUpdateInfo();
        }
    }

    public void onUpdateInfo()
    {
        // 구입 가능한지?
        // 가격 변동은 없는지?

    }

    public void onClickPurchase()
    {
        CPopupManager.Instance.allClosePopup();
        SpawnManager.Instance.spawnUnit<FurnitureData>(exteriorData.name, exteriorData.attribute);
    }
}
