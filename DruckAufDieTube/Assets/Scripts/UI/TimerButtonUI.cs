using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(Stopwatch), typeof(UIDocument))]
public class TimerButtonUI : MonoBehaviour
{
    //EXTERNAL VARIABLES
    public bool isLoggingButton = false;
    [Range(25f, 300f)] public float buttonWidthPX = 300f;
    [Range(25f, 300f)] public float buttonHeightPX = 50f;
    [Range(0.05f, 1f)] public float spawnCooldown = 0.15f;

    //INTERNAL VARIABLES
    private float timeSinceLastSpawn = 0f;
    private bool CanSpawnNewButton { get => timeSinceLastSpawn > spawnCooldown; }

    //UI REFERENCES
    private UIDocument UI_Doc;
    private VisualElement clickArea;
    [Required] public VisualTreeAsset logButtonTemplate;

    //COMPONENT REFERENCES
    private Stopwatch stopwatch;

    //OTHER REFERENCES
    private List<VisualElement> spawnedButtons = new List<VisualElement>();


    public void Start()
    {
        //Find the Document holding all the Visual Elements
        UI_Doc = this.transform.GetComponent<UIDocument>();
        //Get the Stopwatch Reference
        stopwatch = this.transform.GetComponent<Stopwatch>();

        //Make a nullcheck to visualize in console if anything is assigned wrong
        if (UI_Doc == null || stopwatch == null)
        {
            Debug.LogWarning("Couldnt find UI Document or Stopwatch - Fatal Error");
        }
        //get the root Document of the Game View
        var rootElement = UI_Doc.rootVisualElement;

        //Find the Clickarea and assign an On Click Delegate to it, so it Spawns new Buttons On Click
        clickArea = rootElement.Q("ClickArea");
        clickArea.AddManipulator(new Clickable(evt => CreateNewButton(evt.originalMousePosition, false, buttonWidthPX, buttonHeightPX)));

        //Create a Random Start Button that is the Logger
        Vector2 randomPositionOnScreen = new Vector2(UnityEngine.Random.Range(0f + buttonWidthPX / 2f, Screen.width - buttonWidthPX / 2f), UnityEngine.Random.Range(0f + buttonHeightPX / 2f, Screen.height - buttonHeightPX / 2f));
        CreateNewButton(randomPositionOnScreen, true, buttonWidthPX, buttonHeightPX);
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
    }

    public void CreateNewButton(Vector2 pos, bool isLoggingButton, float width, float height) // Here we could add as a Parameter on which Playerside it should be spawned
    {
        // When Cooldown is not reached, dont create a button 
        if (!CanSpawnNewButton)
        {
            return;
        }

        // Create a Visual Element from the Template provided by the uxml file that has the asset type VisualAssetTree and set values on it
        VisualElement buttonInstance = logButtonTemplate.CloneTree();
        buttonInstance.style.position = new StyleEnum<Position>(Position.Absolute);
        buttonInstance.style.width = new StyleLength(new Length(width , LengthUnit.Pixel));
        buttonInstance.style.height = new StyleLength(new Length(height , LengthUnit.Pixel));
        buttonInstance.style.left = new StyleLength(new Length(pos.x - (width / 2f), LengthUnit.Pixel));
        buttonInstance.style.top = new StyleLength(new Length(pos.y - (height / 2f), LengthUnit.Pixel));

        // Set a Bool used for determining if the Object that was created is a LoggingButton or a decoy
        VisualElementWithBool logContainer = buttonInstance.Q<VisualElementWithBool>("LogButtonContainer");
        logContainer.boolAttr = isLoggingButton;

        // Find the Instance of the logButton and Set the Click Event. if loggingButton ends stopwatch, else selfdestruction & removing from local List
        Button logButton = buttonInstance.Q<Button>("TimeLogButton");
        if (isLoggingButton)
        {
            logButton.clickable.clicked += () => { stopwatch.StopStopwatch(); }; // muss das noch unsubscribed werden?
        }
        else
        {
            logButton.clickable.clicked += () => { spawnedButtons.Remove(buttonInstance); clickArea.Remove(buttonInstance);}; // muss das noch unsubscribed werden?
        }

        //Add the buttonInstance to the UI Document
        clickArea.Add(buttonInstance);

        //Add the buttonInstance to a local List for keeping track and doing modifications
        spawnedButtons.Add(buttonInstance);

        // Reset Time since Last Spawn
        timeSinceLastSpawn = 0f;
        

    }

}
