using UnityEngine;
using UnityEngine.UIElements;

public class BlinkingTextUI : MonoBehaviour
{
    public UIDocument uiDocument; // Referensi ke UIDocument utama
    public string[] textElementNames; // Array nama elemen teks yang ingin dikedipkan
    public float blinkDuration = 0.5f; // Durasi kedip dalam detik

    private Label[] textElements; // Array referensi elemen teks
    private bool isVisible = true; // Status visibilitas teks

    void Start()
    {
        // Ambil root dari UIDocument
        var root = uiDocument.rootVisualElement;

        // Inisialisasi array elemen teks
        textElements = new Label[textElementNames.Length];

        for (int i = 0; i < textElementNames.Length; i++)
        {
            // Cari setiap elemen teks berdasarkan nama
            textElements[i] = root.Q<Label>(textElementNames[i]);

            if (textElements[i] == null)
            {
                Debug.LogWarning($"Element with name '{textElementNames[i]}' not found in UIDocument.");
            }
        }

        // Mulai coroutine untuk efek kedip
        InvokeRepeating(nameof(ToggleVisibility), 0, blinkDuration);
    }

    void ToggleVisibility()
    {
        foreach (var textElement in textElements)
        {
            if (textElement != null)
            {
                // Ganti visibilitas teks antara 'Visible' dan 'Hidden'
                isVisible = !isVisible;
                textElement.style.visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }
}
