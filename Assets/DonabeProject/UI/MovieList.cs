using UnityEngine;

[CreateAssetMenu(fileName = "MovieList", menuName = "Scriptable Objects/MovieList")]
public class MovieList : ScriptableObject
{
    public MovieCard[] movieCards;
}