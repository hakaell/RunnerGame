using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _coinsSprites;
    private SpriteRenderer _spriteRenderer;
    private float _tempTime = 0;
    private int _animasyonSayac = 0;
    
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        CoinsAnimation();
    }

    private void CoinsAnimation()
    {
        _tempTime += Time.deltaTime;
        if (_tempTime > 0.03f)
        {
            _spriteRenderer.sprite = _coinsSprites[_animasyonSayac++];
            if (_animasyonSayac == _coinsSprites.Length)
            {
                _animasyonSayac = 0;
            }
            _tempTime = 0;
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Temas Etti");
            GameManager.manager.score++;
            Debug.Log(GameManager.manager.score);
            //_spriteRenderer.enabled = false;
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
