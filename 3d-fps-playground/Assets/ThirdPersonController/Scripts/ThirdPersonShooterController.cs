using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    // GENERAL
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    // AIM
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    // SHOOTING
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // AWAKE
    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }


    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    // UPDATE
    private void Update()
    {
        // AIMING
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
        } else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(aimSensitivity);
        }

        // SHOOTING
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
        }
    }


}
