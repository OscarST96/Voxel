using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "GameOS/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Properties")]
    public int scene_number; //Numero de escena en el Scene List
    public string scene_name; //Nombre del nivel
    public string scene_titlle; //Titulo del nivel para que aparezca en la UI
    [Header("Settings")]
    public float timer = 60.0f; //Tiempo Max. que dura el nivel
    [Header("Visuals && Audio")]
    public Sprite Miniature; //Miniatura del nivel
    public AudioClip backgroundMusic; //Musica de fondo.
}
