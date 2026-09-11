using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    void Start()
    {
        // Reorganizar la pantalla tan pronto como la escena cargue
        RebuildSplitScreen();
    }

    /// <summary>
    /// Busca todas las cámaras activas de los jugadores y ajusta su Viewport Rect.
    /// </summary>
    public void RebuildSplitScreen()
    {
        // Encuentra todas las cámaras activas en la escena
        Camera[] cameras = Camera.allCameras;
        int activePlayers = cameras.Length;

        // Si no hay suficientes cámaras para pantalla dividida, salir
        if (activePlayers < 2) return;

        for (int i = 0; i < activePlayers; i++)
        {
            // AUDIO: Desactivar el AudioListener en todas las cámaras excepto en la del Jugador 1
            AudioListener listener = cameras[i].GetComponent<AudioListener>();
            if (listener != null)
            {
                listener.enabled = (i == 0);
            }

            // CASO 2 JUGADORES: Dividido 50% Izquierda y 50% Derecha
            if (activePlayers == 2)
            {
                cameras[i].rect = new Rect(i * 0.5f, 0f, 0.5f, 1f);
            }
            // CASO 3 Y 4 JUGADORES: Cuadrantes 2x2
            else if (activePlayers == 3 || activePlayers == 4)
            {
                float x = (i % 2) * 0.5f;
                float y = (i < 2) ? 0.5f : 0f;

                // Si son 3 jugadores, el Jugador 3 (índice 2) ocupa todo el ancho inferior
                float width = (activePlayers == 3 && i == 2) ? 1f : 0.5f;

                cameras[i].rect = new Rect(x, y, width, 0.5f);
            }
        }
    }
}
