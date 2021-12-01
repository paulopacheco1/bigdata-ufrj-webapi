using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BigDataUFRJ.API.Models
{
    [BsonIgnoreExtraElements]
    public class ProcessoJudicial
    {
        public ObjectId Id { get; set; }
        public string NumJustica { get; set; }
        public string Tribunal { get; set; }
        public string Comarca { get; set; }
        public string UF { get; set; }
        public List<string> Partes { get; set; }
        public List<string> Andamentos { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProcessoJudicialCreateDTO
    {
        public string NumJustica { get; set; }
        public string Tribunal { get; set; }
        public string Comarca { get; set; }
        public string UF { get; set; }
        public List<string> Partes { get; set; }
        public List<string> Andamentos { get; set; }

        public ProcessoJudicial ToModel()
        {
            return new ProcessoJudicial()
            {
                NumJustica = NumJustica,
                Tribunal = Tribunal,
                Comarca = Comarca,
                UF = UF,
                Partes = Partes,
                Andamentos = Andamentos,
            };
        }
    }

    public class ProcessoJudicialUpdateDTO
    {
        public string Tribunal { get; set; }
        public string Comarca { get; set; }
        public string UF { get; set; }
        public List<string> Partes { get; set; }
        public List<string> Andamentos { get; set; }
    }
}
