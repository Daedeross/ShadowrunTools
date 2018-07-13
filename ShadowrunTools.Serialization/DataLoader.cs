using Castle.Core.Logging;
using Newtonsoft.Json;
using ShadowrunTools.Characters.Prototypes;
using ShadowrunTools.Serialization.Prototypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace ShadowrunTools.Serialization
{
    public class DataLoader
    {
        private readonly JsonSerializer _serializer;
        private readonly ILogger _logger;
        public IPrototypeRepository Repository { get; private set; }

        public List<string> CurrentFiles { get; set; } 

        public DataLoader(JsonSerializer serializer, ILogger logger)
        {
            _serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IPrototypeRepository ReloadAll()
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
                var traits = _serializer.Deserialize<ICollection<TraitPrototypeBase>>(reader);

                repository.MergeTraitCollection(traits);
            }
        }
    }
}
