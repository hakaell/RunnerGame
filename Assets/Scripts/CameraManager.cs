using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
   public Camera _camera;
   public PlayerController player;

   public Transform camPos;
   public Transform introCam;
   public Transform follewerCam;
   public Transform deadCam;
   public Transform finishCam;

   private void Awake()
   {
      _camera = Camera.main;
   }
   private void Update()
   {
      CamFollow();
   }
   private void CamLerp()
   {
      _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.zero, Time.deltaTime * 3);
      _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.identity, Time.deltaTime * 3);
   }
   
   private void CamFollow()
   {
      camPos.position = Vector3.Lerp(camPos.position, player.transform.position, Time.deltaTime * 10);
      switch (GameManager.manager.CurrentGameState)
      {
         case GameManager.GameState.Prepare:
            
            if (_camera.transform.parent != introCam)
            {
               _camera.transform.SetParent(introCam);
            }
            CamLerp();
            break;
         case GameManager.GameState.MainGame:
            if (_camera.transform.parent != follewerCam)
            {
               _camera.transform.SetParent(follewerCam);
            }
            CamLerp();
            break;
         case GameManager.GameState.DeadGame:
            if (_camera.transform.parent != deadCam)
            {
               _camera.transform.SetParent(deadCam);
            }
            CamLerp();
            break;
         case GameManager.GameState.EndGame:
            if (_camera.transform.parent != finishCam)
            {
               _camera.transform.SetParent(finishCam);
            }
            CamLerp();
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }
}
