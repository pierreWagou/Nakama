using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour
{
  GameObject lineTemplate;
  GameObject messageTemplate;
  GameObject line;
  Dictionary<string, List<string>> commandDict;
  GameController gameController;
  List<string> commandHistory;
  int indexCommand;

    // Start is called before the first frame update
    void Start()
    {
      commandHistory = new List<string>();
      gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
      lineTemplate = transform.GetChild(0).gameObject;
      messageTemplate = transform.GetChild(1).gameObject;
      buildCommandDict();
      displayPrompt();
    }

    // Update is called once per frame
    void Update()
    {
      executeCommand();
      focus();
      browseHistory();
    }

    void browseHistory() {
      if (Input.GetKeyDown(KeyCode.UpArrow) && indexCommand>0) {
        indexCommand -=1;
        useHistory();
      }
      if (Input.GetKeyDown(KeyCode.DownArrow) && indexCommand<commandHistory.Count) {
        indexCommand +=1;
        useHistory();
      }
    }

    void useHistory() {
      InputField commandInput = line.transform.GetChild(1).gameObject.GetComponent<InputField>();
      commandInput.text = (indexCommand==commandHistory.Count) ? "" : commandHistory[indexCommand];
    }

    void focus() {
      InputField commandInput = line.transform.GetChild(1).gameObject.GetComponent<InputField>();
      commandInput.Select();
    }

    void displayPrompt() {
      line = Instantiate(lineTemplate) as GameObject;
      line.SetActive(true);
      line.transform.SetParent(lineTemplate.transform.parent);
      ScrollRect scrollRect = transform.parent.transform.parent.GetComponent<ScrollRect>();
      scrollRect.normalizedPosition = new Vector2(0, -1);
    }

    void executeCommand() {
      if (Input.GetKeyUp(KeyCode.Return)) {
        InputField commandInput = line.transform.GetChild(1).gameObject.GetComponent<InputField>();
        commandInput.interactable = false;
        execute(commandInput.text);
        displayPrompt();
      }
    }

    void displayMessage(string output) {
      GameObject message = Instantiate(messageTemplate) as GameObject;
      message.SetActive(true);
      message.transform.SetParent(messageTemplate.transform.parent);
      Text messageText = message.GetComponent<Text>();
      messageText.text = output;
    }

    void execute(string commandInputText) {
      if (commandInputText!="") {
        commandHistory.Add(commandInputText);
        indexCommand = commandHistory.Count;
        List<string> instruction = new List<string>(commandInputText.Split(new char[] {' '}));
        string output = "";
        if (checkMnemo(instruction[0])) {
          if (checkOption(instruction)) { output = commandMapping(instruction); }
          else { output = unknownOption(instruction); }
        }
        else { output = unknownMnemo(instruction[0]); }
        displayMessage(output);
      }
    }

    public void executeSystem(string commandInputText) {
      InputField commandInput = line.transform.GetChild(1).gameObject.GetComponent<InputField>();
      commandInput.text = commandInputText;
      commandInput.interactable = false;
      List<string> instruction = new List<string>(commandInputText.Split(new char[] {' '}));
      string output = commandMapping(instruction);
      displayMessage(output);
      displayPrompt();
    }


    string commandMapping(List<string> instruction) {
      switch (instruction[0]) {
        case "help":
          return printHelp(instruction[1]);
        case "game":
          return gameState(instruction[1]);
        case "message":
          return message(instruction[1]);
        case "cd":
          return cd(instruction[1]);
        default:
          return unknownMnemo(instruction[0]);
      }
    }

    string cd(string option) {
      switch(option) {
        case "menu":
          SceneManager.LoadScene("Menu");
          return "Loading Menu";
        case "level1":
          SceneManager.LoadScene("Level 1");
          return "Loading Level 1";
        case "level2":
          SceneManager.LoadScene("Level 2");
          return "Loading Level 2";
        case "level3":
          SceneManager.LoadScene("Level 3");
          return "Loading Level 3";
        case "level4":
          SceneManager.LoadScene("Level 4");
          return "Loading Level 4";
        case "arene":
          SceneManager.LoadScene("Level Arene");
          return "Loading Level Arene";
        case "gameover":
          SceneManager.LoadScene("Game Over");
          return "Loading Game Over";
        default:
          return "no level to be load";
      }
    }

    string printHelp(string option) {
      string helpText = "Le principe de Nakama est simple : coopérez pour gagner. Pour progresser dans les différents niveau, il faudra que vos 2 avatars s'entraident sans pourtant être dans le même espace virtuelle. Ce menu vous permettra également d'interagir avec le code source du jeu, soyez donc sûr de ce que vous faites quand vous le manipulez. Par mesure de sécurité et pour assurer la meilleure expérience de jeu possible, l'accès aux données critiques vous est interdit.\n Pour avoir plus d'information sur les contrôles, lancez 'help player'.";
      string helpAmmo = "Pour utiliser votre arme, commencez par rammasser des munitions, des balles pour la ville et du plasma pour la cave. Le nombre de muntion sera indiqué en haut à droite de votre partie d'écran. Les munitions ne sont pas conservées à la mort du joueur et au passage au niveau suivant.";
      string helpInteraction = "Vous pouvez agir avec certains éléments du jeu. Ces interactions se font au travers de bouton qui vous permettront d'activer ou désactiver des pièges mais également faire apparaître des plateformes.";
      string helpPlayer = "Continuer -> game resume \n Quitter -> game quit \n Afficher les crédits -> game credit \n Joueur 1 (Ville) : \n Déplacer à droite -> D \n Déplacer à auche -> Q \n Sauter, grimper -> Z \n S'accroupir -> S \n Courir -> LeftShift \n Interagir -> E \n Tirer -> A \n\n Joueur 2 (Grotte) : \n Déplacer à droite -> RightArrow \n Déplacer à gauche -> LeftArrow \n Sauter, grimper -> UpArrow \n S'accroupir -> DownArrow \n Courir -> RightShift \n Interagir -> RightAlt \n Tirer -> + \n \n Pour en savoir plus sur les systèmes de munitions, entrez 'help ammo'. Pour plus d'information sur les interactions en jeu, lancez 'help interaction'.";
      switch (option) {
        case "nakama":
          return helpText;
        case "player":
          return helpPlayer;
        case "interaction":
          return helpInteraction;
        case "ammo":
          return helpAmmo;
        default:
          return helpText;
      }
    }

    string gameState(string option) {
      string credit = "Merci à vous de jouer à Nakama. Nakama a été développé par trois étudiants UTCéens dans le cadre de l'UV de conception de jeux vidéo IC06. Pierre Romon, Edmond Giraud et Alexandre Touzeau. Les musiques utilisées sont de NIHILORE et sont libres de droit. Les visuels utilisés sont de ANSIMUZ et sont libres de droit. Nous tenons à remercier Nicolas Esposito pour l'encadrement, les conseils et la pédagogie.";
      switch (option) {
        case "resume":
          gameController.isPaused = false;
          return "Game resumed";
        case "quit":
          Application.Quit();
          return "Game left";
        case "credit":
          return credit;
        default:
          return "No option";
      }
    }

    string message(string option) {
      string welcomeMessage = "Bienvenu dans Nakama ! Tout d'abord merci de vous être porté volontaire pour tester ce jeu ! Je vous recommande de lancer 'help nakama' pour avoir plus d'information sur le gameplay et 'game credit' pour afficher les crédits. Pour sortir du menu, frapper la touche Tab. (っ◔◡◔)っ Bon jeu !!";
      switch (option) {
        case "welcome":
          return welcomeMessage;
        default:
          return welcomeMessage;
      }
    }

    bool checkMnemo(string mnemo) {
      return commandDict.ContainsKey(mnemo);
    }

    string unknownMnemo(string mnemo) {
      return mnemo + " : commande non reconnue.";
    }

    bool checkOption(List<string> instruction) {
      if (instruction.Count==1) {
        instruction.Add("none");
      }
      return commandDict[instruction[0]].Contains(instruction[1]);
    }

    string unknownOption(List<string> instruction) {
      string errorText = instruction[1] + " : option pour " + instruction[0] + " non reconnue. \n Les options sont les suivantes : ";
      foreach(string option in commandDict[instruction[0]]) {
        errorText += option + ", ";
      }
      return errorText;
    }

    void buildCommandDict() {
      commandDict = new Dictionary<string, List<string>>();
      List<string> list = new List<string>();
      commandDict.Add("help", new List<string>(new string[] {"nakama", "player", "ammo", "interaction"}));
      commandDict.Add("game", new List<string>(new string[] {"resume", "quit", "credit"}));
      commandDict.Add("message", new List<string>(new string[] {"welcome"}));
      commandDict.Add("cd", new List<string>(new string[] {"menu", "level1", "level2", "level3", "level4", "arene", "gameover"}));
    }
}
