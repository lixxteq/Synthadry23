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

    public void FillCells(ItemObject itemObject)
    {
        FillCell(damageCellContainer.transform, itemObject.levelDamage, itemObject.maxLevelDamage);
        FillCell(rateOfFireCellContainer.transform, itemObject.levelRateOfFire, itemObject.maxLevelRateOfFire);
        FillCell(maxAmmoCellContainer.transform, itemObject.levelAmmo, itemObject.maxLevelAmmo);
        FillCell(lanternCellContainer.transform, itemObject.hasLantern ? 1 : 0, 1);
    }
}
