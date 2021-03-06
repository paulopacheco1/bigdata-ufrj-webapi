using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BigDataUFRJ.API.Models
{
    [BsonIgnoreExtraElements]
    public class ProcessoJudicial
    {
        public ObjectId Id { get; set; }
        public string NumJustica { get; set; }
        public string Tribunal { get; set; }
        public string Uf { get; set; }
        public List<dynamic> Partes { get; set; }
        public List<dynamic> Andamentos { get; set; }
    }

    public class ProcessoJudicialCreateDTO
    {
        public string NumJustica { get; set; }
        public string Tribunal { get; set; }
        public string Uf { get; set; }
        public List<dynamic> Partes { get; set; }
        public List<dynamic> Andamentos { get; set; }

        public ProcessoJudicial ToModel()
        {
            return new ProcessoJudicial()
            {
                NumJustica = NumJustica,
                Tribunal = Tribunal,
                Uf = Uf,
                Partes = Partes,
                Andamentos = Andamentos,
            };
        }
    }

    public class ProcessoJudicialUpdateDTO
    {
        public string Tribunal { get; set; }
        public string Uf { get; set; }
        public List<dynamic> Partes { get; set; }
        public List<dynamic> Andamentos { get; set; }
    }
}
