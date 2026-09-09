using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ElectricalPanel : MonoBehaviour, IInteractable
{
    public AudioSource SwitchAudioSource;
    public AudioClip switchSound;

    public GameObject electricalPanelUI;
    public GameObject cellsParent;
    public Image[] cells;
    public TMP_Text powerLevelText;
    public Image border;

    public Color emptyColor = new Color32(37, 40, 37, 255);
    public Color redColor = new Color32(166, 61, 54, 255);
    public Color yellowColor = new Color32(180, 154, 58, 255);
    public Color greenColor = new Color32(95, 158, 69, 255);

    public InputActionReference cancelAction;

    public int maxEnergy = 15;
    public int energy = 15;

    private float timer = 0f;
    private float cellWorkTime = 3f;

    public RectTransform bar;
    public RectTransform target;
    public RectTransform greenZone;

    public RectTransform cursor;
    public float cursorSpeed = 500f;
    private float cursorDirection = 1f;

    private void OnEnable()
    {
        cancelAction.action.Enable();
    }
    private void OnDisable()
    {
        cancelAction.action.Disable();
    }

    public void Interact()
    {
        OpenUI();
        //if (!G.lightManager.isLightOn)
        //{
        //    SwitchAudioSource.PlayOneShot(switchSound);
        //    StartCoroutine(G.lightManager.TurnOnLights());
        //}
        //else
        //{
        //    G.lightManager.TurnOffLights();
        //}
    }

    public void OpenUI()
    {
        electricalPanelUI.SetActive(true);

        G.player.SetMovementEnabled(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        electricalPanelUI.SetActive(false);

        G.player.SetMovementEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public string GetInteractText()
    {
        if (G.lightManager.isLightOn)
        {
            return "Light switch";
        }
        else
        {
            return "Press E to turn lights on";
        }
    }

    void Awake()
    {
        cells = cellsParent.GetComponentsInChildren<Image>();
    }

    void Start()
    {
        RandomizeTarget();
    }

    void Update()
    {
        if (electricalPanelUI.activeSelf && cancelAction.action.WasPressedThisFrame())
        {
            CloseUI();
        }
        if (timer >= cellWorkTime && energy > 0)
        {
            AddEnergy(-1);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
        if (energy == 0 && G.lightManager.isLightOn)
        {
            G.lightManager.TurnOffLights();
        }
        if (electricalPanelUI.activeSelf)
        {
            MoveCursor();
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                CheckHit();
            }
        }
        
    }

    void AddEnergy(int n)
    {
        energy = Mathf.Clamp(energy + n, 0, maxEnergy);
        UpdateUIColor();
    }

    void UpdateUIColor()
    {
        Color color = GetEnergyColor();
        //cells
        for (int i = 0; i < energy; i++)
        {
            cells[i].color = color;
        }
        for (int i = energy; i < maxEnergy; i++)
        {
            cells[i].color = emptyColor;
        }
        //UI
        powerLevelText.color = color;
        powerLevelText.text = $"{energy}/{maxEnergy}";
        border.color = color;
    }
    Color32 GetEnergyColor()
    {
        if (energy <= 3f)
        {
            return redColor;
        }
        if (energy <= 10f)
        {
            return yellowColor;
        }
        return greenColor;
    }

    void RandomizeTarget()
    {
        float barWidth = bar.rect.width;
        float targetWidth = target.rect.width;

        float minX = -barWidth / 2f + targetWidth / 2f;
        float maxX = barWidth / 2f - targetWidth / 2f;

        float randomX = Random.Range(minX, maxX);

        target.anchoredPosition = new Vector2(randomX, target.anchoredPosition.y);
    }

    void MoveCursor()
    {
        Vector2 position = cursor.anchoredPosition;

        position.x += cursorDirection * cursorSpeed * Time.deltaTime;

        float minX = -bar.rect.width / 2f;
        float maxX = bar.rect.width / 2f;

        if (position.x >= maxX)
        {
            position.x = maxX;
            cursorDirection = -1f;
        }
        if (position.x <= minX)
        {
            position.x = minX;
            cursorDirection = 1f;
        }

        cursor.anchoredPosition = position;
    }

    void CheckHit()
    {
        float cursorX = cursor.anchoredPosition.x;

        float targetLeft = target.anchoredPosition.x - target.rect.width / 2f;
        float targetRight = target.anchoredPosition.x + target.rect.width / 2f;

        float greenZoneLeft = target.anchoredPosition.x - greenZone.rect.width / 2f;
        float greenZoneRight = target.anchoredPosition.x + greenZone.rect.width / 2f;

        if (cursorX >= targetLeft && cursorX <= targetRight)
        {
            //press on green zone
            if (cursorX >= greenZoneLeft && cursorX <= greenZoneRight)
            {
                AddEnergy(3);
            }
            //press on yellow zone
            else
            {
                AddEnergy(1);
            }
            cursorSpeed = Mathf.Clamp(cursorSpeed + 100f, 500, 1000);
            RandomizeTarget();
        }
        else
        {
            AddEnergy(-1);
            cursorSpeed = Mathf.Clamp(cursorSpeed - 100f, 500, 1000);
        }
    }
}
