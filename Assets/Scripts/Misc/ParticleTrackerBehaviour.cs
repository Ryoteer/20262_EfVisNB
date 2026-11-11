using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class ParticleTrackerBehaviour : MonoBehaviour
{
    [Header("<color=orange>VFX</color>")] 
    [SerializeField] private bool _tracksPlayer = true;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private bool _playOnAwake = true;
    [SerializeField] private string _playEventName = "OnPlay";
    [SerializeField] private string _stopEventName = "OnStop";
    
    private VisualEffect _vfx;
    private Transform _target;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();

        if (_playOnAwake)
        {
            _vfx.SendEvent(_playEventName);
        }
    }

    private void Start()
    {
        if (_tracksPlayer)
        {
            _target = GameManager.Instance.ThirdPersonPlayer.transform;
        }
        else if (_targetTransform)
        {
            _target = _targetTransform;
        }
        else
        {
            _target = transform;
        }
    }

    private void LateUpdate()
    {
        if (_target.position != transform.position)
        {
            transform.position = _target.position;
        }
    }
}
