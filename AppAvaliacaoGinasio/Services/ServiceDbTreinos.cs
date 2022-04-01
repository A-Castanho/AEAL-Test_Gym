using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using AppAvaliacaoGinasio.Models;
using SQLite;

namespace AppAvaliacaoGinasio.Services
{
    public class ServiceDbTreinos
    {
        private readonly SQLiteConnection connection;
        public string StatusMessage { get; set; }
        public ServiceDbTreinos(string dbPath)
        {
            if (dbPath == "")
                dbPath = App.DbPath;

            //Definir a base de dados
            connection = new SQLiteConnection(dbPath);
            //Criação das tabelas
            connection.CreateTable<Treino>();
        }
        public void Inserir(Treino treino)
        {
            try
            {
                ValidarDados(treino);

                int result = connection.Insert(treino); //Devolve o nº de linhas (0 no caso de erro)
                if (result != 0)
                {
                    StatusMessage = string.Format("{0} registos inseridos", result);
                }
                else
                {
                    string.Format("0 registos inseridos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        private static void ValidarDados(Treino treino)
        {
            bool propriedadeValida = true;
            //Obtem as propriedades do model Treino
            List<PropertyInfo> propriedadesTreino = new List<PropertyInfo>
                    (
                        typeof(Treino).GetProperties()
                    );
            //Ignora a propriedade DateTimeSaida para poder ser inserida em edição
            propriedadesTreino.Remove(typeof(Treino).GetProperty("DateTimeSaida"));

            foreach (PropertyInfo property in propriedadesTreino)
            {
                if (property.GetValue(treino) != null)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (((string)property.GetValue(treino)).Trim() == "")
                        {
                            propriedadeValida = false;
                        }
                    }
                }
                else
                {
                    propriedadeValida = false;
                }
                if (!propriedadeValida)
                {
                    string nomePropriedade = property.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .Cast<DisplayNameAttribute>().FirstOrDefault().DisplayName;
                    throw new Exception("O campo " + nomePropriedade + " não é válido");

                }
            }
        }

        public void Atualizar(Treino treino)
        {
            try
            {
                ValidarDados(treino);
                connection.Update(treino);
                StatusMessage = "Operação realizada com sucesso!";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public void Apagar(int id)
        {
            try
            {
                //aceder aos dados dentro da tabela
                int result = connection.Table<Treino>().Delete(r => r.Id == id);
                StatusMessage = string.Format("{0} registos eliminados", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public List<Treino> Procurar(DateTime dateTimeEntrada)
        {
            List<Treino> lista = new List<Treino>();
            try
            {
                var resp = from p in connection.Table<Treino>()
                           where p.DateTimeEntrada.Date == dateTimeEntrada.Date
                           select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            lista = lista.OrderBy(t => t.DateTimeEntrada).ToList();
            return lista;
        }
        public Treino GetTreino(int id)
        {
            Treino m = new Treino();
            try
            {
                m = connection.Table<Treino>().First(n => n.Id == id);
                StatusMessage = string.Format("Encontrou a anotação");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return m;
        }
        public List<Treino> GetTreinos()
        {
            List<Treino> lista;
            try
            {
                lista = connection.Table<Treino>().ToList();
                this.StatusMessage = "Listagem dos treinos";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            lista = lista.OrderBy(t => t.DateTimeEntrada).ToList();
            return lista;
        }
    }
}
