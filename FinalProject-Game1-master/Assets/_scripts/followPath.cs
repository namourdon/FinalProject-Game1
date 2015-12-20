using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class followPath : MonoBehaviour {
    public enum FollowType
    {
        moveTowards,
        lerp
    }

    public FollowType Type = FollowType.moveTowards;
    public platformController Path;
    public float Speed = 1;
    public float MaxDistance = .1f;

    private IEnumerator<Transform> _currentPoint;

    public void Start()
    {
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }
        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
            return;

        transform.position = _currentPoint.Current.position;

    }



    public void Update()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        if (Type == FollowType.moveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        else if (Type == FollowType.lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistance * MaxDistance)
            _currentPoint.MoveNext();
    }
	
}
