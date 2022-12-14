using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HoleTrap : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private SphereCollider _playerWall;

    private Animator _animator;
    private AudioResources _audioResources;
    private bool _isActivated = false;
    private float _delayForClosed = 2f;
    private float _delayForActivated = 1f;
    private const string OpenGate = "OpenGate";
    private const string CloseGate = "CloseGate";
    private const string ActivateTrap = "ActivateTrap";

    private void Start()
    {
        _audioResources = FindObjectOfType<AudioResources>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated)
        {
            if (other.TryGetComponent(out Boid enemy))
            {
                enemy.HoleTrapCath(_target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActivated == false)
        {
            if (other.TryGetComponent(out Car car))
            {
                _audioResources.PlaySound(ActivateTrap);
                _animator.SetBool(OpenGate, true);
                _isActivated = true;
                _animator.SetBool(OpenGate, true);
                _animator.SetBool(CloseGate, false);
                StartCoroutine(TimerForCloseGate());
                _playerWall.enabled = true;
            }
        }
    }

    private IEnumerator TimerForCloseGate()
    {
        yield return new WaitForSeconds(_delayForClosed);

        _animator.SetBool(CloseGate, true);
        _animator.SetBool(OpenGate, false);
        _playerWall.enabled = false;

        yield return new WaitForSeconds(_delayForActivated);

        _isActivated = false;
    }
}
