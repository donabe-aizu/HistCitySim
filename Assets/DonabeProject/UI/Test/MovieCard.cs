using UnityEngine;

[CreateAssetMenu(fileName = "MovieCard", menuName = "Scriptable Objects/MovieCard")]
public class MovieCard : ScriptableObject
{
    public Texture2D thumbnail;
    public string movieTitle;
    public string channelName;
}
