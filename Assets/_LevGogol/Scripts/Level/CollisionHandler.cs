using System;
using DG.Tweening;
using UnityEngine;

public class CollisionHandler
{
    private Collider2D _lastCloud;
    private float _maxDepth = 3.9f; //todo refactor this
    private Player _player;
    private SoundManager _soundManager;

    public CollisionHandler(Player player, SoundManager soundManager)
    {
        _player = player;
        _soundManager = soundManager;
        
        _player.Collisioned += OnCollision;
    }
    
    private void OnCollision(Collision2D cloud)
    {
        if (cloud == null) return;
        if (cloud.collider == _lastCloud) return;

        if (cloud.transform.TryGetComponent<DamageCloud>(out var damageCloud))
        {
            _player.TakeDamage();
            damageCloud.PlayParticles(_player.transform.position);
            _soundManager.PlayClip(Clip.Damage);
        }
        else if (cloud.transform.GetComponent<Cloud>())
        {
            var position = cloud.transform.position;
            if (position.y < _maxDepth)
            {
                ScoreStorage.Count += (int) Math.Round((_maxDepth - position.y) / 1.5); //TODO No
                _maxDepth = position.y;
            }
            
            _soundManager.PlayClip(Clip.Cloud);
            //multipliceText.text = "x" + (ScoreStorage.Count / 10);
        }
        
        _lastCloud = cloud.collider;
    }
}