using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtonsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backround;

    public Sprite beamPlaySprite;
    public Sprite beamSettingsSprite;
    public Sprite beamExitSprite;
    public Sprite beamNewSprite;

    public Sprite defaultBackroundSprite;

    public enum ButtonAction
    {
        play,
        settings,
        exit,
        new_game
    }

    public ButtonAction buttonAction;

    private void Awake()
    {
        backround.sprite = defaultBackroundSprite;
    }

    public void DoAction()
    {
        switch (buttonAction)
        {
            case ButtonAction.play:
                if (transform.parent.parent.parent.gameObject.TryGetComponent<MainMenuAsyncPlay>(out MainMenuAsyncPlay component)){
                    component.Load();
                };
                break;
            case ButtonAction.settings:
                backround.sprite = beamSettingsSprite;
                break;
            case ButtonAction.new_game:
                backround.sprite = beamNewSprite;
                break;
            case ButtonAction.exit:
                backround.sprite = beamExitSprite;
                break;
            default:
                backround.sprite = defaultBackroundSprite;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (buttonAction)
        {
            case ButtonAction.play:
                backround.sprite = beamPlaySprite;
                break;
            case ButtonAction.settings:
                backround.sprite = beamSettingsSprite;
                break;
            case ButtonAction.new_game:
                backround.sprite = beamNewSprite;
                break;
            case ButtonAction.exit:
                backround.sprite = beamExitSprite;
                break;
            default:
                backround.sprite = defaultBackroundSprite;
                break;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        backround.sprite = defaultBackroundSprite;
    }
}
