using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

public class ModularOpenAIController : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> cardCreationMessage;
    BaseCard[] cards;
    private ModuleConfigGetterSetter moduleConfigGetterSetter;

    // REGEX Expression for card creation

    string rxCardNameString = @"(?<=([0-9]. )).*(?=(: HP:))";
    string rxHPString = @"(?<=(: HP: )).*(?=(, Speed:))";
    string rxSpeedString = @"(?<=(, Speed: )).*(?=(, Attack: ))";
    string rxAttackString = @"(?<=(, Attack: )).*(?=(\n)?)";

    public ModularOpenAIController(){
        string configJsonString  = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Assets/OpenAI_Module/moduleConfig.json"));
        moduleConfigGetterSetter = JsonConvert.DeserializeObject<ModuleConfigGetterSetter>(configJsonString)!;
    }
    //Need to be a list because multiple Requests to the API will be made

    // Start is called before the first frame update
    public string submitCharacterPrompt(string inputPrompt)
    {
        //Create a new instance of the OpenAI API, and give it the APIKEY (Stored in the System Environment Variables)
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPEN_AI_APIKEY", EnvironmentVariableTarget.User));
        return StartCharacterCreation(inputPrompt).ToString();
    }
    private string StartCharacterCreation(string inputPrompt)
    {
        Debug.Log("Modular Button function Beginning");
        cardCreationMessage = new List<ChatMessage> { 
            //This is where the prompt limits are imput
            new (ChatMessageRole.System, "You are to create" + moduleConfigGetterSetter.NumberOfObjcets + " creatures related to the character brief that is given. These creatures will be used for " + moduleConfigGetterSetter.ObjectContextDescription + ". You will respond with only the creature's " + moduleConfigGetterSetter.ObjectAttributes + " stats, no other information. Each stat must be greater than 0 and cannot exceed 20. The format for each creature should be numbered list similar to this '1. {Creature Name}: HP: 10, Speed: 10, Attack: 10' then go to a new line")
            // Example Brief: The character brief is: I am a noble knight. I was born in a little village and conscripted into the royal army for training at a young age. I fight with sword and shield honourably to protect the king's palace.
        };

        Task<string> task = Task.Run(() =>
        {
            return GetResponse(inputPrompt);
        });

        task.Wait();

        return task.Result;
    }

    private async Task<string> GetResponse(string inputPrompt)
    {
        if (inputPrompt.Length < 1)
        {
            return "Prompt Length too Small!";
        }

        // Fill the user message form the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputPrompt;
        if (userMessage.Content.Length > 200) //This is here to save tokens when making an API
        {
            //Shorten user message if over 100
            userMessage.Content = userMessage.Content.Substring(0, 200);
        }
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        //Add Message to list
        cardCreationMessage.Add(userMessage);


        // Send Character creation message to OpenAI to get the reponse in cards
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 300,
            Messages = cardCreationMessage
        });

        // Get Response from API
        ChatMessage APIResponse = new ChatMessage();
        APIResponse.Role = chatResult.Choices[0].Message.Role;
        APIResponse.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", APIResponse.rawRole, APIResponse.Content));

        cardCreationMessage.Add(APIResponse);

        string apiResponseString = APIResponse.Content;


        // Split Creatures/Objects into individual Strings
        string[] cardUnserialized = apiResponseString.Split(
            new string[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        //Initialize Array of Card Objects
        cards = new BaseCard[cardUnserialized.Length];
        int i = 0;
        foreach (var item in cardUnserialized)
        {

            Match nameMatch = Regex.Match(item, rxCardNameString);
            Match hpMatch = Regex.Match(item, rxHPString);
            Match speedMatch = Regex.Match(item, rxSpeedString);
            Match attackMatch = Regex.Match(item, rxAttackString);

            BaseCard card = new BaseCard(
                                nameMatch.Value, 
                                int.Parse(attackMatch.Value), 
                                int.Parse(speedMatch.Value), 
                                int.Parse(hpMatch.Value)
                                );
            cards[i] = card;
            i++;
        }

        Debug.Log(cards[0].cardName.ToString());
        Debug.Log(cards[0].strength.ToString());

        return apiResponseString;
    }

    internal static void submitCharacterPrompt()
    {
        throw new NotImplementedException();
    }
}
public class ModuleConfigGetterSetter {
    public int NumberOfObjcets { get; set; }
    public int NumberOfObjectAttributes { get; set; }
    public string ObjectAttributes { get; set; }
    public string ObjectContextDescription { get; set; }
}