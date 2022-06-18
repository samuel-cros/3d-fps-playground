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
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform spawnBulletPosition;

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
        // Player is offset compared with the crosshair, fortunately the crosshair is screen-centered
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }


        if (starterAssetsInputs.aim)
        {
            // Switch to zoom in
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false); // Stop the character controller script from rotating the player when zooming

            // Compute direction
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            // Rotate the player
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); // Alternative to Quaternion
        } else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(true); // Allow the character controller script to rotate the player when not zooming
        }

        // SHOOTING
        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(bulletProjectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            starterAssetsInputs.shoot = false; // Unsure it doesn't shoot constantly
        }
        
    }


}
