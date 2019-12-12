using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    [SerializeField]
    float _cooldown = 5.0f;

    Slider _cooldownSlider;

    int _availableItems = 3;
    int _maxItems = 3;
    bool _isCooldownOn = false;

    void Start() {
        _cooldownSlider = GetComponent<Slider>();
    }

    void Update() {
        if (_isCooldownOn) {
            _cooldownSlider.value += Time.deltaTime / _cooldown;

            if (_cooldownSlider.value >= _cooldownSlider.maxValue) {
                _availableItems++;
                _cooldownSlider.value = 0.0f;
            }

            _isCooldownOn = _availableItems < _maxItems;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            _availableItems = 0;
            _isCooldownOn = true;
        }
    }
}