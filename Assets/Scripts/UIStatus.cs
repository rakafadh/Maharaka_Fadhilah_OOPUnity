using UnityEngine;
using UnityEngine.UIElements;

public class UIStatus : MonoBehaviour
{
    private Label pointsLabel;
    private Label healthLabel;
    private Label waveLabel;
    private Label enemiesLabel;

    private Player player;
    private CombatManager combatManager;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        pointsLabel = root.Q<Label>("Points");
        healthLabel = root.Q<Label>("Health");
        Debug.Log("Health Ditemukan");
        waveLabel = root.Q<Label>("Wave");
        enemiesLabel = root.Q<Label>("Enemies");

        player = Player.Instance;
        combatManager = FindObjectOfType<CombatManager>();
    }

    void Update()
    {
        if (player != null && healthLabel != null)
        {
            healthLabel.text = "Health: " + player.GetComponent<HealthComponent>().Health;
        }

        if (combatManager != null)
        {
            if (pointsLabel != null)
            Debug.Log("Points Ditemukan");
            {
                pointsLabel.text = "Points: " + combatManager.totalPoints;
            }
            if (waveLabel != null)
            {
                waveLabel.text = "Wave: " + combatManager.waveNumber;
            }
            if (enemiesLabel != null)
            {
                enemiesLabel.text = "Enemies Left: " + combatManager.totalEnemies;
            }
        }
    }
}