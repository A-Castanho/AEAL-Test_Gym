using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace AppAvaliacaoGinasio.Models
{
    [Table("Treinos")]
    public class Treino
    {
        /*
            Pretende-se criar uma aplicação móvel que permita guardar os registos
            diários de um utilizadorque frequenta um Ginásio. 
            Assim para o utilizador
            interessa guardar 
            a data em que frequentou o ginásio, 
            a intensidade da atividade fisica (baixa, média, alta), 

            os equipamentos onde realizou a atividade, 

            as partes do corpo trabalhadas, 
            o tipo de treino, 
            as calorias perdidas em cada treino e o 
            IMC.
         */
        public enum IntensidadeFisica { Baixa, Media, Alta }
        
        [DisplayName("Id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [DisplayName("Tempo de Entrada"),NotNull]
        public DateTime DateTimeEntrada { get; set; }
        [DisplayName("Tempo de Saída"),NotNull]
        public DateTime DateTimeSaida { get; set; }
        [DisplayName("Intensidade"),NotNull]
        public IntensidadeFisica Intensidade { get; set; }
        [DisplayName("Equipamentos Usados"),NotNull]
        public string EquipamentosUsados { get; set; }
        [DisplayName("Partes do Corpo Trabalhadas"), NotNull]
        public string PartesCorpoTrabalhadas { get; set; }
        [DisplayName("Tipo de Treino"), NotNull]
        public string TipoTreino { get; set; }
        [DisplayName("Calorias Perdidas"), NotNull]
        public int CaloriasPerdidas { get; set; }
        [DisplayName("IMC"), NotNull]
        public double Imc { get; set; }

        public Treino()
        {
            Id = 0;
            DateTimeEntrada = DateTime.Now;
            EquipamentosUsados = "";
            PartesCorpoTrabalhadas = "";
            TipoTreino = "";
            CaloriasPerdidas = 0;
            Imc = 0;
        }
    }
}
