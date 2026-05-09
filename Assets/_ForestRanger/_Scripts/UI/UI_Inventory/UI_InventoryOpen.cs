using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_InventoryOpen : MonoBehaviour
{
    [Inject] private GameStateMachine _stateMachine;

    [SerializeField] private Button _inventoryOpenButton;

    private void Start()
    {
        _inventoryOpenButton.onClick.AddListener(OpenInventory);
    }

    private void OpenInventory()
    {
        _stateMachine.SwitchState<InventoryState>().Forget();
    }

}
