using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class RandomUserAPI : MonoBehaviour
{
    private const string API_URL = "https://randomuser.me/api/";

    public async Task<User> GetUserAsync()
    {
        return await FetchRandomUser();
    }

    private async Task<User> FetchRandomUser()
    {
        using UnityWebRequest request = UnityWebRequest.Get(API_URL);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
            await Task.Yield();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Erro na requisição: {request.error}");
            return null;
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            User user = ProcessRandomUser(jsonResponse);
            return user;
        }
    }

    private User ProcessRandomUser(string json)
    {
        RandomUserResponse userResponse = JsonUtility.FromJson<RandomUserResponse>(json);

        if (userResponse != null && userResponse.results != null && userResponse.results.Length > 0)
        {
            User user = userResponse.results[0];
            return user;
        }
        else
        {
            Debug.LogError("Nenhum usuário encontrado na resposta.");
            return null;
        }
    }
}

[System.Serializable]
public class RandomUserResponse
{
    public User[] results;
}

[System.Serializable]
public class User
{
    public Name name;
    public Location location;
}

[System.Serializable]
public class Name
{
    public string first;
    public string last;
}

[System.Serializable]
public class Location
{
    public string country;
    public string state;
    public string city;
    public Street street;
}

[System.Serializable]
public class Street
{
    public string name;
    public string number;
}
