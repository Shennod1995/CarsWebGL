using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _currentQuanity;
    [SerializeField] private TMP_Text _maxQuanity;
    [SerializeField] private Button _sellButton;

    private Upgrade _upgrade;

    public Upgrade Upgrade => _upgrade;

    public event UnityAction<UpgradeView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _name.text = upgrade.UpgradeName.ToString();
        _icon.sprite = upgrade.ImageUpgrade;
        _price.text = upgrade.Price.ToString();
        _currentQuanity.text = upgrade.CurrentQuanity.ToString();
        _maxQuanity.text = upgrade.MaxQuanity.ToString();
    }

    public void DeactivateButton() => _sellButton.interactable = false;

    public void UpdateInteractable()
    {
        if(_upgrade.CanSellUpgrade() == false)
            _sellButton.interactable = false;

    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(this);
        Render(_upgrade);
    }
}
