using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // para mostrar el puntaje en la pantalla
    public TextMeshProUGUI questionText; // para mostrar las preguntas en la pantalla
    public Button button1; // para el primer botón de respuesta
    public Button button2; // para el segundo botón de respuesta
    public Button button3; // para el tercer botón de respuesta
    public Button button4; // para el cuarto botón de respuesta

    int score = 0; // para almacenar el puntaje actual del jugador

    string[] questions = { "Pregunta 1", "Pregunta 2", "Pregunta 3", "Pregunta 4" }; // array de preguntas
    string[][] answers = new string[4][]; // array bidimensional de respuestas, cada fila representa las respuestas de una pregunta
    int[] correctAnswers = { 2, 3, 1, 4 }; // array de índices de respuesta correcta, debe coincidir con las preguntas

    int currentQuestionIndex = 0; // índice de la pregunta actual
    bool gameOver = false; // indica si el juego ha terminado
    // Start is called before the first frame update
    void Start()
    {
        answers[0] = new string[] { "Respuesta 1-1", "Respuesta 1-2", "Respuesta 1-3", "Respuesta 1-4" };
        answers[1] = new string[] { "Respuesta 2-1", "Respuesta 2-2", "Respuesta 2-3", "Respuesta 2-4" };
        answers[2] = new string[] { "Respuesta 3-1", "Respuesta 3-2", "Respuesta 3-3", "Respuesta 3-4" };
        answers[3] = new string[] { "Respuesta 4-1", "Respuesta 4-2", "Respuesta 4-3", "Respuesta 4-4" };
    }

    void NextQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            gameOver = true;
            return;
        }
        questionText.text = questions[currentQuestionIndex];

        // generar una lista aleatoria de índices de respuesta
        List<int> indices = new List<int>();
        for (int i = 0; i < answers[currentQuestionIndex].Length; i++)
        {
            indices.Add(i);
        }
        indices = indices.OrderBy(x => Random.value).ToList();

        // mostrar las respuestas aleatoriamente
        button1.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][indices[0]];
        button2.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][indices[1]];
        button3.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][indices[2]];
        button4.GetComponentInChildren<TextMeshProUGUI>().text = answers[currentQuestionIndex][indices[3]];

        // marcar la respuesta correcta
        int correctIndex = indices.IndexOf(correctAnswers[currentQuestionIndex]);
        Button correctButton = null;
        switch (correctIndex)
        {
            case 0:
                correctButton = button1;
                break;
            case 1:
                correctButton = button2;
                break;
            case 2:
                correctButton = button3;
                break;
            case 3:
                correctButton = button4;
                break;
        }
        correctButton.GetComponent<Image>().color = Color.green;

        // escuchar los eventos de clic del botón
        button1.onClick.AddListener(delegate { AnswerQuestion(0); });
        button2.onClick.AddListener(delegate { AnswerQuestion(1); });
        button3.onClick.AddListener(delegate { AnswerQuestion(2); });
        button4.onClick.AddListener(delegate { AnswerQuestion(3); });
    }

    void AnswerQuestion(int index)
    {
        // deshabilitar los botones después de que el jugador haya seleccionado una respuesta
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
        // comprobar si la respuesta es correcta o incorrecta
        if (index == correctAnswers[currentQuestionIndex])
        {
            score += 100;
            scoreText.text = "Puntaje: " + score.ToString();
        }
        else
        {
            switch (index)
            {
                case 0:
                    button1.GetComponent<Image>().color = Color.red;
                    break;
                case 1:
                    button2.GetComponent<Image>().color = Color.red;
                    break;
                case 2:
                    button3.GetComponent<Image>().color = Color.red;
                    break;
                case 3:
                    button4.GetComponent<Image>().color = Color.red;
                    break;
            }
        }

        // pasar a la siguiente pregunta después de un breve retraso
        currentQuestionIndex++;
        Invoke("NextQuestion", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            {
                // mostrar una pantalla de Game Over con el puntaje obtenido
                questionText.text = "Game Over";
                scoreText.text = "Puntaje final: " + score.ToString();
            }
        
    }
}
