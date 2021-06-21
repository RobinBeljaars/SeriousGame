using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class questionHandler : MonoBehaviour
{
    public GameObject correctAnswerScreen;
    public Text questionText;
    public List<Text> answertexts;
    public Button answer1Btn;
    public Button answer2Btn;
    public Button answer3Btn;
    public Button answer4Btn;
    private int correctAnswer;
    Dictionary<int, string> questions = new Dictionary<int, string>();
    Dictionary<int, string> answers = new Dictionary<int, string>();
    List<string> cities = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Button btn = answer1Btn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClickAnswer1);

        Button btn2 = answer2Btn.GetComponent<Button>();
        btn2.onClick.AddListener(TaskOnClickAnswer2);

        Button btn3 = answer3Btn.GetComponent<Button>();
        btn3.onClick.AddListener(TaskOnClickAnswer3);

        Button btn4 = answer4Btn.GetComponent<Button>();
        btn4.onClick.AddListener(TaskOnClickAnswer4);
        collectQuestions();
        collectAnswers();
        setQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClickAnswer1()
    {
        Debug.Log("You have clicked the button for answer 1");
        string answer = answertexts[0].text;
        checkAnswerIsCorrect(answer);
    }

    void TaskOnClickAnswer2()
    {
        Debug.Log("You have clicked the button for answer 2");
        string answer = answertexts[1].text;
        checkAnswerIsCorrect(answer);
    }

    void TaskOnClickAnswer3()
    {
        Debug.Log("You have clicked the button for answer 3");
        string answer = answertexts[2].text;
        checkAnswerIsCorrect(answer);
    }

    void TaskOnClickAnswer4()
    {
        Debug.Log("You have clicked the button for answer 4");
        string answer = answertexts[3].text;
        checkAnswerIsCorrect(answer);
    }

    void checkAnswerIsCorrect(string answer)
    {
        //collect question
        string question = questionText.text;
        int keyOfQuestion = 0;

        if (questions.ContainsValue(question))
        {
            keyOfQuestion = GetKeyFromValue(question);
        }
        if (keyOfQuestion != 0)
        {
            if (answers.ContainsKey(keyOfQuestion))
            {
                if (answer == answers[keyOfQuestion])
                {
                    Debug.Log("das nondeju goed jonge");
                    correctAnswerScreen.SetActive(true);
                }
            }
        }
    }

    int GetKeyFromValue(string valueVar)
    {
        foreach (int keyVar in questions.Keys)
        {
            if (questions[keyVar] == valueVar)
            {
                return keyVar;
            }
        }
        return 0;
    }

    void setQuestion()
    {
        var key = generateRandomNumberMaxQuestions();
        string value = "";

        if (questions.ContainsKey(key))
        {
            value = questions[key];
        }
        questionText.text = value;

        setAnswers(key);
    }

    void setAnswers(int keyOfQuestion)
    {
        int random = UnityEngine.Random.Range(0, answertexts.Count-1);
        Debug.Log(random);
        if (answers.ContainsKey(keyOfQuestion))
        {
            answertexts[random].text = answers[keyOfQuestion];
            correctAnswer = random;
        }

        if (keyOfQuestion <= 3)
        {
            generateCities();
            foreach(Text t in answertexts){
                if(t != answertexts[random]){
                    t.text = getRandomCity(answertexts[random].text);
                }
            }
        } else
        {
            List<String> wrongAnswers = new List<String>();
            wrongAnswers.Add("75");
            wrongAnswers.Add("100");
            wrongAnswers.Add("40");
            int answerIndex = 0;
            foreach(Text t in answertexts){
                if(t != answertexts[random]){
                    var text = wrongAnswers[answerIndex];
                    t.text = text;
                    answerIndex++;
                }
            }
        }

        
    }

    string getRandomCity(string cityNotToChoose)
    {
        System.Random random = new System.Random();
        string returnCity = "";
        cities.Remove(cityNotToChoose);

        if (!cities[random.Next(cities.Count)].Equals(cityNotToChoose))
        {
            returnCity = cities[random.Next(cities.Count)];
            cities.Remove(returnCity);
        } else
        {
            getRandomCity(cities[random.Next(cities.Count)]);
        }


        return returnCity;
    }

    void collectQuestions()
    {
        questions.Add(1, "Wat is de hoofdstad van Madagaskar?");
        questions.Add(2, "Wat is de hoofdstad van Nederland?");
        questions.Add(3, "Wat is de hoofdstad van Spanje?");
        questions.Add(4, "Hoeveel is 25 * 4 / 2?");
        questions.Add(5, "Hoeveel is 100 / 10 * 25?");
    }

    void collectAnswers()
    {
        answers.Add(1, "Antananarivo");
        answers.Add(2, "Amsterdam");
        answers.Add(3, "Madrid");
        answers.Add(4, "50");
        answers.Add(5, "250");
    }

    void generateCities()
    {
        cities.Add("London");
        cities.Add("Antananarivo");
        cities.Add("Amsterdam");
        cities.Add("Madrid");
        cities.Add("Barcelona");
        cities.Add("Parijs");
        cities.Add("Berlijn");
    }

    int generateRandomNumberMaxQuestions()
    {
        var max = questions.Count + 1;
        System.Random rd = new System.Random();

        return rd.Next(1, max);
    }
}
