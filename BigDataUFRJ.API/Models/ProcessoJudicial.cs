using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataUFRJ.API.Models
{
    public class ProcessoJudicial
    {
        public int Id { get; set; }
        public string NumJustica { get; set; }
        public int NumJusticaAntigo { get; set; }
        public int NumInternoTribunal { get; set; }
        public string Grau { get; set; }
        public string Eletronico { get; set; }
        public string Tribunal { get; set; }
        public string Comarca { get; set; }
        public string UF { get; set; }
        public string Vara { get; set; }
        public int Reparticao { get; set; }
        public string DataDistribuicao { get; set; }
        public string FaseAtual { get; set; }
        public string DataFaseAtual { get; set; }
        public int ValorAcao { get; set; }
        public string EstadoProcesso { get; set; }
        public string DataEstadoProcesso { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        public List<ParteJudicial> Partes { get; set; }
        public List<AndamentoProcesso> Andamentos { get; set; }
    }
}
