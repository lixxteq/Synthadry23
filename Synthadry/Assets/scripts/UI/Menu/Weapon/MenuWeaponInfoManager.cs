using UnityEngine;

public class MenuWeaponInfoManager : MonoBehaviour
{
    public GameObject filledCellPref;
    public GameObject emptyCellPref;

    public GameObject damageCellContainer;
    public GameObject rateOfFireCellContainer;
    public GameObject maxAmmoCellContainer;
    public GameObject lanternCellContainer;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearCell(Transform cellContainer)
    {
        for (int i = 0; i < cellContainer.childCount; i++)
        {
            Destroy(cellContainer.GetChild(i).gameObject);
        }
    }

    void FillCell(Transform cellsContainer, int filledCells, int allCells)
    {

        ClearCell(cellsContainer);

        for (int i = 0; i < filledCells; i++)
        {
            GameObject filledCell = Instantiate(filledCellPref);
            filledCell.transform.SetParent(cellsContainer.transform);
        }

        for (int i = 0; i < allCells - filledCells; i++)
        {
            GameObject emptyCell = Instantiate(emptyCellPref);
            emptyCell.transform.SetParent(cellsContainer.transform);
        }
    }

    public bool CheckCell(Transform cell, int statMaxLevel)
    {
        if (statMaxLevel == 0)
        {
            cell.parent.gameObject.SetActive(false);
            return false;
        }
        else
        {
            cell.parent.gameObject.SetActive(true);
            return true;
        }
    }

    public void FillCells(ItemObject itemObject)
    {
        if (CheckCell(damageCellContainer.transform, itemObject.maxLevelDamage))
        {
            FillCell(damageCellContainer.transform, itemObject.levelDamage, itemObject.maxLevelDamage);
        }

        if (CheckCell(rateOfFireCellContainer.transform, itemObject.maxLevelRateOfFire))
        {
            FillCell(rateOfFireCellContainer.transform, itemObject.levelRateOfFire, itemObject.maxLevelRateOfFire);
        }

        if (CheckCell(maxAmmoCellContainer.transform, itemObject.maxLevelAmmo))
        {
            FillCell(maxAmmoCellContainer.transform, itemObject.levelAmmo, itemObject.maxLevelAmmo);
        }

        if (CheckCell(lanternCellContainer.transform, itemObject.canHasLantern ? 1 : 0))
        {
            FillCell(lanternCellContainer.transform, itemObject.hasLantern ? 1 : 0, itemObject.canHasLantern ? 1 : 0);
        }


    }
}
