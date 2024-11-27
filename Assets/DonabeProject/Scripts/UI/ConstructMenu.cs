using System;
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

            foreach (var building in buildingList.buildings)
            {
                var buildingTemplate = buildingElement.Instantiate();
                Debug.Log(building.name);
                buildingTemplate.dataSource = building;
                root.Q<VisualElement>("Grid").contentContainer.Add(buildingTemplate);
            }
        }
    }
}