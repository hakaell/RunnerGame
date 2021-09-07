using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int speed;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Camera _cam;
    public bool isInjured = true;

    public float maxDistanceX;
    private float _dirX;
    private float _mousePosX;
    public float swipeValue;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        _cam = Camera.main;
    }

    public void IdleAnimation()
    {
        _animator.Play("Idle");
        _animator.ResetTrigger("Running");
    }
    public void RunningAnimation()
    {
        _animator.ResetTrigger("Dance");
        _animator.SetTrigger("Running");
        isInjured = false;
    }
   public void DeadAnimation()
    {
        _animator.SetTrigger("Dead");
        GameManager.manager.deadGameCanvas.gameObject.SetActive(true);
        GameManager.manager.inGameCanvas.gameObject.SetActive(false);
    }

    public void DanceAnimation()
    {
        _animator.SetTrigger("Dance");
        
    }
   public void SliderMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            Vector3 dir = transform.forward * (Time.deltaTime * speed);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector3 playerPos = transform.position;
                    _dirX = playerPos.x;
                    _mousePosX = _cam.ScreenToViewportPoint(touch.position).x;
                    break;
                case TouchPhase.Moved:
                    float newMousePosX = _cam.ScreenToViewportPoint(touch.position).x;
                    float distance = newMousePosX - _mousePosX;
                    float posX = _dirX + (distance * swipeValue);
                    Vector3 pos = transform.position;
                    pos.x = posX;
                    pos.x = Mathf.Clamp(pos.x, -maxDistanceX, maxDistanceX);
                    transform.position = pos;
                    break;

                case TouchPhase.Ended:
                    transform.Translate(dir);
                    break;
            }
        }
    }
   
    //Playerımızı hareket ettirdiğimiz fonksiyon
    public void PlayerMove()
    {
        var temp = _rigidbody.velocity = Vector3.forward *(Time.deltaTime * speed);
        transform.Translate(temp); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            speed = 0;
            GameManager.manager.EndCanvas();
            //finishCam = true;
            GameManager.manager.CurrentGameState = GameManager.GameState.EndGame;
        }
    }
}
