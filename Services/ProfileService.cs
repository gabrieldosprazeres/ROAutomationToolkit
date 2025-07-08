using ROAutomationToolkit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ROAutomationToolkit.Services
{
    public interface IProfileService
    {
        List<Profile> LoadProfiles();
        Profile GetProfile(string name);
        void AddOrUpdateProfile(Profile profile);
        void DeleteProfile(string name);
    }

    public class ProfileService : IProfileService
    {
        private readonly string _profilesFilePath;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly object _fileLock = new object();

        public ProfileService()
        {
            _profilesFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                "profiles.json"
            );
            
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new ProfileJsonConverter() }
            };
            
            InitializeProfilesFile();
        }

        private void InitializeProfilesFile()
        {
            lock (_fileLock)
            {
                if (!File.Exists(_profilesFilePath))
                {
                    File.WriteAllText(_profilesFilePath, "[]");
                }
                else if (new FileInfo(_profilesFilePath).Length == 0)
                {
                    File.WriteAllText(_profilesFilePath, "[]");
                }
            }
        }

        public List<Profile> LoadProfiles()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!File.Exists(_profilesFilePath)) 
                        return new List<Profile>();

                    string json = File.ReadAllText(_profilesFilePath);

                    if (string.IsNullOrWhiteSpace(json) || json.Trim() == "[]")
                        return new List<Profile>();
                    
                    return JsonSerializer.Deserialize<List<Profile>>(json, _jsonOptions) 
                           ?? new List<Profile>();
                }
                catch (JsonException)
                {
                    TryRepairCorruptedFile();
                    return new List<Profile>();
                }
                catch (Exception ex)
                {
                    throw new ProfileServiceException(
                        $"Erro ao carregar perfis: {ex.Message}", ex);
                }
            }
        }

        private void TryRepairCorruptedFile()
        {
            try
            {
                string backupPath = _profilesFilePath + ".bak";
                File.Copy(_profilesFilePath, backupPath, true);
                
                string json = File.ReadAllText(_profilesFilePath);

                var profiles = new List<Profile>();
                int startIndex = 0;
                
                while ((startIndex = json.IndexOf('{', startIndex)) != -1)
                {
                    int endIndex = json.IndexOf('}', startIndex);
                    if (endIndex == -1) break;
                    
                    string segment = json.Substring(startIndex, endIndex - startIndex + 1);
                    
                    try
                    {
                        var profile = JsonSerializer.Deserialize<Profile>(segment, _jsonOptions);
                        if (profile != null && !string.IsNullOrWhiteSpace(profile.Name))
                        {
                            profiles.Add(profile);
                        }
                    }
                    catch {}
                    
                    startIndex = endIndex + 1;
                }
                
                SaveProfiles(profiles);
            }
            catch
            {
                File.WriteAllText(_profilesFilePath, "[]");
            }
        }

        public Profile GetProfile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome inválido", nameof(name));

            return LoadProfiles()?.FirstOrDefault(p => 
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void AddOrUpdateProfile(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));
            
            if (string.IsNullOrWhiteSpace(profile.Name))
                throw new ArgumentException("Nome do perfil inválido");

            lock (_fileLock)
            {
                var profiles = LoadProfiles() ?? new List<Profile>();
                var existing = profiles?.FirstOrDefault(p => 
                    p.Name.Equals(profile.Name, StringComparison.OrdinalIgnoreCase));
                
                if (existing != null)
                {
                    existing.Key = profile.Key;
                    existing.KeyCode = profile.KeyCode;
                    existing.Interval = profile.Interval;
                }
                else
                {
                    profiles.Add(profile);
                }
                
                SaveProfiles(profiles);
            }
        }

        public void DeleteProfile(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome inválido", nameof(name));

            lock (_fileLock)
            {
                var profiles = LoadProfiles();
                if (profiles == null) return;
                
                int removed = profiles.RemoveAll(p => 
                    p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                
                if (removed > 0)
                {
                    SaveProfiles(profiles);
                }
            }
        }

        private void SaveProfiles(List<Profile> profiles)
        {
            try
            {
                string json = JsonSerializer.Serialize(profiles, _jsonOptions);
                File.WriteAllText(_profilesFilePath, json);
            }
            catch (Exception ex)
            {
                throw new ProfileServiceException(
                    $"Erro ao salvar perfis: {ex.Message}", ex);
            }
        }
    }

    public class ProfileServiceException : Exception
    {
        public ProfileServiceException(string message, Exception inner) 
            : base(message, inner) { }
    }

    public class ProfileJsonConverter : JsonConverter<Profile>
    {
        public override Profile Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                string name = 
                    root.TryGetProperty("name", out JsonElement nameElem) ? nameElem.GetString() :
                    root.TryGetProperty("Name", out nameElem) ? nameElem.GetString() : null;
                
                string key = 
                    root.TryGetProperty("key", out JsonElement keyElem) ? keyElem.GetString() :
                    root.TryGetProperty("Key", out keyElem) ? keyElem.GetString() : null;
                
                int keyCode = 
                    root.TryGetProperty("keyCode", out JsonElement keyCodeElem) ? keyCodeElem.GetInt32() :
                    root.TryGetProperty("KeyCode", out keyCodeElem) ? keyCodeElem.GetInt32() : 0;
                
                int interval = 
                    root.TryGetProperty("interval", out JsonElement intervalElem) ? intervalElem.GetInt32() :
                    root.TryGetProperty("Interval", out intervalElem) ? intervalElem.GetInt32() : 0;

                return new Profile
                {
                    Name = name,
                    Key = key,
                    KeyCode = keyCode,
                    Interval = interval
                };
            }
        }

        public override void Write(
            Utf8JsonWriter writer,
            Profile value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("name", value.Name);
            writer.WriteString("key", value.Key);
            writer.WriteNumber("keyCode", value.KeyCode);
            writer.WriteNumber("interval", value.Interval);
            writer.WriteEndObject();
        }
    }
}