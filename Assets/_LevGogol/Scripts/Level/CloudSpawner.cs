using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private float _horizontalDistanceBetweenClouds = 0.6f;
    [SerializeField] private float _verticalDistanceBetweenClouds = 1.5f;
    [SerializeField] private Cloud[] _cloudPrefabs;
    [SerializeField] private DamageCloud[] _damageCloudPrefabs;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Heart _heartPrefab;

    private float _downOffset;
    private float _upOffset;
    private float _horizontalOffset = -16;
    private Queue<Cloud> _activeClouds = new Queue<Cloud>();
    private bool _lockInput;

    public float DownOffset => _downOffset;
    public float UpOffset => _upOffset;

    private void Awake()
    {
        var cloud = MakeCloud(0, 0);
        cloud.transform.position -= Vector3.right * cloud.SizeX() / 2f;
        cloud.LockInput = true;
        _downOffset -= _verticalDistanceBetweenClouds;
        _upOffset = _downOffset;
    }

    public void CreateLine()
    {
        CreateBorder();
        CreateBoard();
        CreateBorder();

        _downOffset -= _verticalDistanceBetweenClouds;
        _horizontalOffset = Random.Range(-17, -14);
    }

    public void RemoveLine()
    {
        for (int i = 0; i < 15; i++)
        {
            var cloud = _activeClouds.Dequeue();
            Destroy(cloud.gameObject);
        }
        
        _upOffset -= _verticalDistanceBetweenClouds;
    }

    public void EnableInputReaction() //move to clouds
    {
        _lockInput = false;
        foreach (var activeCloud in _activeClouds)
        {
            activeCloud.LockInput = false;
        }
    }

    public void DisableInputReaction()
    {
        _lockInput = true;
        foreach (var activeCloud in _activeClouds)
        {
            activeCloud.LockInput = true;
        }
    }

    public void RotateFirstCloud()
    {
        _activeClouds.Peek().Rotate();
    }

    private void CreateBorder()
    {
        for (var i = 0; i < 3; i++)
        {
            var damageCloud = MakeDamageCloud(_horizontalOffset, _downOffset);
            _horizontalOffset += _horizontalDistanceBetweenClouds + damageCloud.Cloud.SizeX();
        }
    }

    private void CreateBoard()
    {
        var badCloudIndex = Random.Range(0, 9);
        for (var i = 0; i < 9; i++)
        {
            float cloudSize;
            
            if (i == badCloudIndex)
            {
                var damageCloud = MakeDamageCloud(_horizontalOffset, _downOffset);
                cloudSize = damageCloud.Cloud.SizeX();
            }
            else
            {
                var cloud = MakeCloud(_horizontalOffset, _downOffset);

                var grayComp = 255 - 100 * Math.Abs(-(float) i / 4 + 1);
                cloud.Sprite.color = new Color(grayComp / 255, grayComp / 255, grayComp / 255);

                if (Random.value > 0.6f)
                    MakeCoin(cloud);
                else if (Random.value > 0.99f)
                    MakeHeart(cloud);

                cloudSize = cloud.SizeX();
            }
            
            _horizontalOffset += _horizontalDistanceBetweenClouds + cloudSize;
        }
    }

    private Cloud MakeCloud(float x, float y)
    {
        var index = Random.Range(0, _cloudPrefabs.Length);
        var cloud = Instantiate(_cloudPrefabs[index], new Vector3(x, y, 0f), Quaternion.identity, transform);
        cloud.transform.position += Vector3.right * cloud.SizeX() / 2f;
        cloud.LockInput = _lockInput;
        _activeClouds.Enqueue(cloud);
        if (Random.Range(0, 2) == 1)
            cloud.Flip();
        return cloud;
    }

    private DamageCloud MakeDamageCloud(float x, float y)
    {
        var index = Random.Range(0, _damageCloudPrefabs.Length);
        var cloud = Instantiate(_damageCloudPrefabs[index], new Vector3(x, y, 0f), Quaternion.identity, transform);
        cloud.transform.position += Vector3.right * cloud.Cloud.SizeX() / 2f;
        cloud.Cloud.LockInput = _lockInput;
        _activeClouds.Enqueue(cloud.Cloud);
        return cloud;
    }

    private Coin MakeCoin(Cloud cloud)
    {
        var position = cloud.transform.position + Vector3.up * 0.3f;
        return Instantiate(_coinPrefab, position, Quaternion.identity, transform);
    }

    private Heart MakeHeart(Cloud cloud)
    {
        var position = cloud.transform.position + Vector3.up * 0.3f;
        return Instantiate(_heartPrefab, position, Quaternion.identity, transform);
    }
}