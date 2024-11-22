using UnityEngine;
using UnityEngine.UIElements;

public class UITest : MonoBehaviour
{
    public MovieList movieList1;
    public MovieList movieList2;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("button-test1").clicked += () => Debug.Log("test1");
        root.Q<Button>("button-test2").clicked += () => Debug.Log("test2");
        root.Q<Button>("button-test3").clicked += () => Debug.Log("test3");

        root.Q<Button>("send").clicked += () => Debug.Log(root.Q<TextField>("textfield-test1").value);

        root.Q<ListView>("movie-card-list1").itemsSource = movieList1.movieCards;
        root.Q<ListView>("movie-card-list2").itemsSource = movieList2.movieCards;
    }
}
