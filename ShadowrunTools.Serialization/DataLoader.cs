using Castle.Core.Logging;
using Newtonsoft.Json;
using ShadowrunTools.Characters;
using ShadowrunTools.Characters.Priorities;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShadowrunTools.Serialization
{
    public class DataLoader : IDataLoader, ICharacterPersistence
    {
        private readonly JsonSerializer _serializer;
        private readonly ILogger _logger;

        public IPrototypeRepository Repository { get; protected set; }

        public List<string> CurrentFiles { get; set; } = new List<string>();

        public DataLoader(JsonSerializer serializer, ILogger logger)
        {
            _serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual IPrototypeRepository ReloadAll()
        {
            if (CurrentFiles != null)
            {
                var repository = new PrototypeRepository();
                foreach (var file in CurrentFiles)
                {
                    LoadFile(repository, file);
                }

                Repository = repository; 
            }
            return Repository;
        }

        private void LoadFile(PrototypeRepository repository, string filename)
        {
            if (!File.Exists(filename))
            {
                _logger.Error($"File does not exist: {filename}");
                return;
            }

            using (var stream = new StreamReader(filename))
            using (var reader = new JsonTextReader(stream))
            {
                var dto = _serializer.Deserialize<PrototypeFile>(reader);

                repository.MergeFile(dto);
            }
        }

        #region ICharacterPersistence

        public void SaveCharacter(string filename, ICharacterLoader loader, ICharacter character)
        {
            using (var stream = new StreamWriter(filename))
            {
                SaveCharacter(stream, loader, character);
            }
        }

        public void SaveCharacter(Stream stream, ICharacterLoader loader, ICharacter character)
        {
            using (var writer = new StreamWriter(stream))
            {
                SaveCharacter(writer, loader, character);
            }
        }

        private void SaveCharacter(TextWriter textWriter, ICharacterLoader loader, ICharacter character)
        {
            var dto = loader.ToDto(character);
            _serializer.Serialize(textWriter, dto);
        }

        public ICharacter LoadCharacter(string filename, ICharacterLoader loader)
        {
            ICharacter character = null;

            if (!File.Exists(filename))
            {
                _logger.Error($"File does not exist: {filename}");
                return character;
            }

            using (var stream = new StreamReader(filename))
            using (var reader = new JsonTextReader(stream))
            {
                var dto = _serializer.Deserialize<CharacterDto>(reader);
                return loader.FromDto(dto);
            }
        }

        #endregion
    }
}
