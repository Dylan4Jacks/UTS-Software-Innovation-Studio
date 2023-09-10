using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenAIController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_InputField inputField;
    public Button submitCharacterButton;

    private OpenAIAPI api;
    private List<ChatMessage> cardCreationMessage;
    //Need to be a list because multiple Requests to the API will be made

    // Start is called before the first frame update
    void Start()
    {
        //Create a new instance of the OpenAI API, and give it the APIKEY (Stored in the System Environment Variables)
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPEN_AI_APIKEY", EnvironmentVariableTarget.User));
        StartCharacterCreation();
        submitCharacterButton.onClick.AddListener(() => GetResponse());
    }

    private void StartCharacterCreation()
    {
        cardCreationMessage = new List<ChatMessage> { 
            //This is where the prompt limits are imput
            new ChatMessage(ChatMessageRole.System, "You are to create 3 creatures related to the character brief that is given. These creatures will be used for cards in a card game. You will respond with only the creature's name, HP, Speed, and Attack stats, no other information. Each stat must be greater than 0 and cannot exceed 20.")
            // Example Brief: The character brief is: I am a noble knight. I was born in a little village and conscripted into the royal army for training at a young age. I fight with sword and shield honourably to protect the king's palace.
        };

        inputField.text = "";
        string startString = "What is your backstory? What is your profession? What motivates this character?";
        textField.text = startString;
        Debug.Log(startString);
    }

    private async void GetResponse()
    {
        if (inputField.text.Length < 1)
        {
            return;
        }

        //Disable the OK button
        submitCharacterButton.enabled = false;

        // Fill the user message form the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputField.text;
        if (userMessage.Content.Length > 200) //This is here to save tokens when making an API
        {
            //Shorten user message if over 100
            userMessage.Content = userMessage.Content.Substring(0, 200);
        }
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        //Add Message to list
        cardCreationMessage.Add(userMessage);

        // Update the text field with the user message
        textField.text = string.Format("You: {0}", userMessage.Content);

        // Clear input field
        inputField.text = "";

        // Send Character creation message to OpenAI to get the reponse in cards
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = cardCreationMessage
        });

        // Get Response from API
        ChatMessage APIResponse = new ChatMessage();
        APIResponse.Role = chatResult.Choices[0].Message.Role;
        APIResponse.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", APIResponse.rawRole, APIResponse.Content));

        cardCreationMessage.Add(APIResponse);

        // Update the Test field with response
        textField.text = APIResponse.Content;

        // Re-enable OK button
        submitCharacterButton.enabled = true;
    }
}
