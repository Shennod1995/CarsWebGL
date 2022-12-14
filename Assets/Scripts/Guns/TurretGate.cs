using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TurretGate : MonoBehaviour
{
    [SerializeField] private SphereCollider _playerWall;
    [SerializeField] private Gun _turret;

    private Animator _animator;
    private AudioResources _audioResources;
    private float _delayForClosed = 7f;
    private float _delayForActivated = 2f;
    private bool _isActivated = false;
    private const string OpenGate = "OpenGate";
    private const string CloseGate = "CloseGate";
    private const string ActivateTrap = "ActivateTrap";

    private void Start()
    {
        _audioResources = FindObjectOfType<AudioResources>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActivated == false)
        {
            if (other.TryGetComponent(out Car car))
            {
                _audioResources.PlaySound(ActivateTrap);
                _isActivated = true;
                _animator.SetBool(OpenGate, true);
                _animator.SetBool(CloseGate, false);
                StartCoroutine(TimerForCloseGate());
                _playerWall.enabled = true;
            }
        }
    }

    public void StartFireTurret() => _turret.StartFire();

    public void StopFireTurret() => _turret.StopFire();

    private IEnumerator TimerForCloseGate()
    {
        yield return new WaitForSeconds(_delayForClosed);

        _animator.SetBool(CloseGate, true);
        _animator.SetBool(OpenGate, false);

        yield return new WaitForSeconds(_delayForActivated);

        _playerWall.enabled = false;
        _isActivated = false;
    }
}
