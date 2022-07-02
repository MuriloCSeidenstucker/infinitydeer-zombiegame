using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class SaveGameData
{
    public int BestScore;
    public int HighestWavesSurvived;
}

public class AudioPreferences
{
    public float MasterVolume = 1;
    public float MusicVolume = 1;
    public float SFXVolume = 1;
}

public class GameSaver : MonoBehaviour
{
    private string _saveGameFilePath => $"{Application.persistentDataPath}/savegame.json";
    private string _audioPreferencesFilePath => $"{Application.persistentDataPath}/preferences.json";

    public SaveGameData CurrentSave { get; private set; }
    public AudioPreferences AudioPreferences { get; private set; }

    private bool IsLoaded => CurrentSave != null && AudioPreferences != null;

    private void SaveGameDataToFile(string filePath, SaveGameData data)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
        }
    }

    private void SaveAudioPreferencesToFile(string filePath, AudioPreferences preferences)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, preferences);
        }
    }

    private SaveGameData LoadGameDataFromFile(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<SaveGameData>(jsonReader);
        }
    }

    private AudioPreferences LoadAudioPreferencesFromFile(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<AudioPreferences>(jsonReader);
        }
    }

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        SaveGameDataToFile(_saveGameFilePath, CurrentSave);
    }

    public void SaveAudioPreferences(AudioPreferences preferences)
    {
        AudioPreferences = preferences;
        SaveAudioPreferencesToFile(_audioPreferencesFilePath, AudioPreferences);
    }

    public void LoadGame()
    {
        if (IsLoaded) return;

        CurrentSave = LoadGameDataFromFile(_saveGameFilePath) ?? new SaveGameData();
        AudioPreferences = LoadAudioPreferencesFromFile(_audioPreferencesFilePath) ?? new AudioPreferences();
    }

    public void DeleteAllData()
    {
        File.Delete(_saveGameFilePath);
        File.Delete(_audioPreferencesFilePath);
        CurrentSave = null;
        AudioPreferences = null;
        LoadGame();
    }
}
