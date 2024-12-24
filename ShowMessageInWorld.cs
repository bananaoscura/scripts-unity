using UnityEngine;
using UnityEngine.UI; // Necesario para controlar la UI
using System.Collections;

public class ShowMessageInWorld : MonoBehaviour
{
    public GameObject messageText; // Asigna el texto en el Inspector
    public float fadeDuration = 0.2f; // Tiempo de desvanecimiento




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activa el mensaje
            messageText.SetActive(true);

            // Obtener el tamaño del BoxCollider2D
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            float colliderTop = collider.bounds.max.y; // Borde superior del collider

            // Ajustar la posición del texto en el borde superior del collider
            messageText.transform.position = new Vector3(transform.position.x, colliderTop + transform.position.z);

            // Iniciar la corutina para hacer el fade in
            StartCoroutine(FadeInMessage());
        }
    }

    private void OnTriggerStay2D(Collider2D other) // Se mantiene mientras el jugador esté dentro
    {
        if (other.CompareTag("Player"))
        {
            // Asegura que el mensaje siga visible mientras el jugador esté dentro del collider
            if (!messageText.activeSelf)
            {
                messageText.SetActive(true);
                StartCoroutine(FadeInMessage());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Desactiva el mensaje cuando el jugador salga
            StartCoroutine(FadeOutMessage());
        }
    }






    private IEnumerator FadeInMessage()
    {
        CanvasGroup canvasGroup = messageText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = messageText.AddComponent<CanvasGroup>(); // Si no tiene un CanvasGroup, lo añadimos
        }

        canvasGroup.alpha = 0; // Inicializa el alfa a 0 (invisible)

        // Gradualmente aumenta la opacidad del mensaje
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1; // Asegúrate de que la opacidad llegue al máximo
    }

    private IEnumerator FadeOutMessage()
    {
        CanvasGroup canvasGroup = messageText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = messageText.AddComponent<CanvasGroup>(); // Si no tiene un CanvasGroup, lo añadimos
        }

        // Gradualmente disminuye la opacidad del mensaje
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0; // Asegúrate de que la opacidad llegue a 0
        messageText.SetActive(false); // Desactiva el mensaje una vez que esté completamente invisible
    }
}
