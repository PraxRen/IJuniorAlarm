using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speedUpdateVolume;

    private const float MaxVolume = 1f;
    private const float MinVolume = 0f;

    private float _targetVolume;
    private Coroutine _jobUpdateVolume;

    private void OnEnable()
    {
        _building.Enter += OnEnterBuilding;
        _building.Exit += OnExitBuilding;
    }

    private void OnDisable()
    {
        _building.Enter -= OnEnterBuilding;
    }

    private void OnEnterBuilding()
    {
        _targetVolume = MaxVolume;
        RunUpdateVolume();
    }

    private void OnExitBuilding()
    {
        _targetVolume = MinVolume;
        RunUpdateVolume();
    }

    private void RunUpdateVolume()
    {
        if (_jobUpdateVolume != null)
            return;

        _jobUpdateVolume = StartCoroutine(UpdateVolume());
    }

    private IEnumerator UpdateVolume()
    {
        _audioSource.Play();

        while (_audioSource.volume != _targetVolume) 
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, Time.deltaTime * _speedUpdateVolume);
            yield return null;
        }

        _jobUpdateVolume = null;
    }
}
