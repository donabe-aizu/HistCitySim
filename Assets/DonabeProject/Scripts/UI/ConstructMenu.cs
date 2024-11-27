using System;
using DonabeProject.Manager;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace DonabeProject.UI
{
    public class ConstructMenu : MonoBehaviour
    {
        [SerializeField] 
        private VisualTreeAsset buildingElement;
        
        [SerializeField] 
        private BuildingList buildingList;
        
        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.RegisterCallback<MouseOverEvent>(Enter);
            root.RegisterCallback<MouseOutEvent>(Exit);

            foreach (var building in buildingList.buildings)
            {
                var buildingTemplate = buildingElement.Instantiate();
                buildingTemplate.Q<VisualElement>("thumbnail").dataSource = building;
                root.Q<VisualElement>("Grid").contentContainer.Add(buildingTemplate);
                buildingTemplate.RegisterCallback<ClickEvent>(Clicked);
            }
        }
        
        private void Enter(MouseOverEvent evt)
        {
            Debug.Log("Enter: " + evt.currentTarget);
            PlayerStatusHolder.I.isBlockClickRaycast = true;
        }

        private void Exit(MouseOutEvent evt)
        {
            Debug.Log("Exit: " + evt.currentTarget);
            PlayerStatusHolder.I.isBlockClickRaycast = false;
        }

        private void Clicked(ClickEvent evt)
        {
            Debug.Log("Click");
        }
    }
}