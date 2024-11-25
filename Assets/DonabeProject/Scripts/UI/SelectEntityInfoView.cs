using Citizen;
using DonabeProject.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace DonabeProject.UI
{
    public class SelectEntityInfoView : MonoBehaviour
    {
        private VisualElement root;
        void Start()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        }

        public void ChangeView(CitizenInfoData citizenBase)
        {
            root.Q<Label>("name").text = "citizenBase";
            root.Q<Label>("pocketMoney").text = citizenBase.pocketMoney.ToString();
            root.Q<Label>("appetite").text = citizenBase.appetite.ToString();
        }
    }
}