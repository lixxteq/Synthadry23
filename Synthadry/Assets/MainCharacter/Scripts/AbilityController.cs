using UnityEngine;
using UnityEngine.InputSystem;
using EPOOutline;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using Cinemachine;

public class AbilityController : MonoBehaviour {
    private Camera mainCamera;
    private GameObject fpsCamera;
    private Outliner outliner;
    private GameObject player;
    private Coroutine _visionProcess = null;

    [Header("Abilities properties")]
    public float visionDistance = 40f;
    public int visionDuration = 4;

    [Header("Passive player stats")]
    public float weaponRecoilForce = 1f; // 1 = 100%

    public void Start() {
        mainCamera = Camera.main;
        fpsCamera = GameObject.Find("FPS Camera");
        outliner = mainCamera.GetComponent<Outliner>();
        player = this.gameObject;

        fpsCamera.GetComponent<CinemachineImpulseListener>().m_Gain = weaponRecoilForce;

        // load player abilities properties from save
    }

    public void OnUseVision(InputAction.CallbackContext ctx) {
        bool state = ctx.ReadValueAsButton();
        if (state) {
            // outliner.enabled = true;
            VisionCallback();
        } else {
            // outliner.enabled = false;
            // VisionCallback(globOutlinables, false);
        }
    }

    public void VisionCallback() {
        Outlinable[] outlinables = FindObjectsOfType<Outlinable>();
        foreach (var item in outlinables.Select((value, i) => new {i, value}))
        {
            float distance = Vector3.Distance(player.transform.position, item.value.transform.position);
            if (distance > visionDistance) outlinables[item.i] = null;
        }
        if (_visionProcess != null) {
            StopCoroutine(_visionProcess);
            _visionProcess = null;
        }
        Debug.Log("VisionCallback fired");
        _visionProcess = StartCoroutine(ActivateOutlinables(outlinables));
        Debug.Log("VisionCallback ended");
    }

    private IEnumerator ActivateOutlinables(Outlinable[] outlinables) {
        foreach (Outlinable item in outlinables) {
            if (item != null) item.enabled = true;
        }
        for (int i = visionDuration; i > 0; i--) {
            yield return new WaitForSeconds(1);    
        }
        foreach (Outlinable item in outlinables) {
            if (item != null) item.enabled = false;
        }
    }
}