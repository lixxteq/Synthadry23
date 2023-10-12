using UnityEngine.UI;
using UnityEngine;

public class BuffsSlotManager : MonoBehaviour
{
    public Image hp;
    public Sprite hpActive;
    public Sprite hpDisable;

    public Image armor;
    public Sprite armorActive;
    public Sprite armorDisable;

    public Image boost;
    public Sprite boostActive;
    public Sprite boostDisable;

    public Image grenade;

    public void DrawGrenades(int grenadesCount)
    {
        if (grenadesCount == 0)
        {
            TintImage(grenade);
        } else
        {
            UnTintImage(grenade);
        }
    }

    public void DrawBuffs(int hpCount, int armorCount, int boostCount, int activeBuff)
    {
        hp.sprite = hpDisable;
        armor.sprite = armorDisable;
        boost.sprite = boostDisable;
        switch (activeBuff)
        {
            case 0:
                hp.sprite = hpActive;
                break;

            case 1:
                armor.sprite = armorActive;
                break;

            case 2:
                boost.sprite = boostActive;
                break;

            default:
                break;
        }

        if (hpCount == 0)
        {
            TintImage(hp);
        } else
        {
            UnTintImage(hp);
        }

        if (armorCount == 0)
        {
            TintImage(armor);
        }
        else
        {
            UnTintImage(armor);
        }

        if (boostCount == 0)
        {
            TintImage(boost);
        }
        else
        {
            UnTintImage(boost);
        }
    }

    void UnTintImage(Image image)
    {
        image.color = new Color(1, 1, 1, 1);
    }

    void TintImage(Image image)
    {
        image.color = new Color(0.4f, 0.4f, 0.4f, 0.75f);
    }

}
