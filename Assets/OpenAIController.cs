using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAIController : MonoBehaviour
{
    //These should all be linked/changed to a input text field: public TMP_InputField inputField
    public string input1;
    public string input2;
    public string input3;
    public string input4;

    //link to a button that submits user input
    //public Button submitCharacterButton

    private OpenAIAPI api;
    private v cardCreationMessage;
    //Need to be a list because multiple Requests to the API will be made

    // Start is called before the first frame update
    void Start()
    {
        //Create a new instance of the OpenAI API, and give it the APIKEY (Stored in the System Environment Variables)
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPEN_AI_APIKEY"), EnvironmentVariableTarget.User);
        StartCharacterCreation();
        //submitCharacterButton.onClick.AddListener(() => GetResponse());
    }

    private void StartCharacterCreation()
    {
        cardCreationMessage = new List<ChatMessage> { 
            //This is where the prompt limits are imput
            new ChatMessage(ChatMessageRole.System, "You are to create 30 creatures related to the character brief that is given. These creatures will be used for cards in a card game. You will respond with only the creature's name, HP, Speed, and Attack stats, no other information. Each stat must be greater than 0 and cannot exceed 20.")
            // Example Brief: The character brief is: I am a noble knight. I was born in a little village and conscripted into the royal army for training at a young age. I fight with sword and shield honourably to protect the king's palace.
        };
    }

    private async void GetResponse()
    {
        throw new NotImplementedException();
    }
}
