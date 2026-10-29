using UnityEngine;

public class Otimization : MonoBehaviour
{
void Start()
    {
        // Ativa o giroscópio explicitamente na Unity
        Input.gyro.enabled = true;
        
        // Define o intervalo de atualização do giroscópio para 60Hz (0.016s) 
        // ou 90Hz (0.011s) para casar com a taxa de atualização de tela mobile comum.
        // Isso impede que o sensor inunde a CPU com dados inúteis entre os frames.
        Input.gyro.updateInterval = 0.016f; 
    }
}
