using Gerenciador_Financeiro.Domains.Domains.Demostrative;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Repository;
using Google.Cloud.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.Infra.Repositories
{
    public class DemonstrativeRepository : IDemonstrativeRepository
    {
        private FirestoreDb _dbContext = DataBaseContext.OpenConnectionDb();

        public async Task<List<Demonstrative>> GetAllsDemonstrative()
        {
            List<Demonstrative> listDemonstrative = new List<Demonstrative>();
            Query query = _dbContext.Collection("Extrato");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (var item in snap)
            {
                Demonstrative demonstrative = item.ConvertTo<Demonstrative>();
                demonstrative.Id = item.Id;
                listDemonstrative.Add(demonstrative);
            }
            listDemonstrative.Reverse();
            return listDemonstrative;
        }

        public async Task<List<Demonstrative>> GetDemonstrativeByInterval(string interval1, string interval2)
        {
            List<Demonstrative> listDemonstrative = new List<Demonstrative>();
            Query query = _dbContext.Collection("Extrato");
            QuerySnapshot snap = await query.GetSnapshotAsync();

            foreach (var item in snap)
            {
                if(DateTime.Parse(item.Id).Date >= DateTime.Parse(interval1).Date && DateTime.Parse(item.Id).Date <= DateTime.Parse(interval2).Date)
                {
                    Demonstrative demonstrative = item.ConvertTo<Demonstrative>();
                    demonstrative.Id = item.Id;
                    listDemonstrative.Add(demonstrative);
                }
            }
            listDemonstrative.Reverse();
            return listDemonstrative;
        }

        public async void Save(string typeOperation, double operationValue, double value, string operationId, string date)
        {
            Demonstrative demonstrative = new Demonstrative();
            //demonstrative.Id = DateTime.Now.ToString("dd-MM-yyyy");
            demonstrative.Id = date;
            demonstrative = VerificationDemonstrativeId(demonstrative.Id).Result;
            DocumentReference docRef = _dbContext.Collection("Extrato").Document(demonstrative.Id);
            Dictionary<string, object> data = new Dictionary<string, object>();
            Dictionary<string, object> dicList = new Dictionary<string, object>()
                {
                    { "Id da operação", operationId},
                    { $"Valor do {typeOperation}", operationValue},
                    { "Valor Atual", value},
                    { "Operação", typeOperation}
                };

            ArrayList arrayList = new ArrayList();
            if(demonstrative.Operation != null)
            {
                foreach (var item in demonstrative.Operation)
                {
                    arrayList.Add(item);
                }
            }
            arrayList.Insert(0, dicList);

            data.Add("Operação", arrayList);
            await docRef.SetAsync(data);

        }

        public async void Update(double value, string date, string operationId)
        {
            Query query = _dbContext.Collection("Extrato");
            QuerySnapshot snap = await query.GetSnapshotAsync();
            bool demonstrativeFound = false; // verifica se ja encontrou a data do extrato
            bool operationFound = false; //verifica se ja encontrou a operação dentro do extrato
            double newBalance = 0; // atualiza o saldo de acordo com a operação em andamento

            foreach (var item in snap)
            {
                Demonstrative demonstrative = item.ConvertTo<Demonstrative>();
                demonstrative.Id = item.Id;
                if(demonstrative.Id == date || demonstrativeFound)
                {
                    ArrayList arrayList = new ArrayList();
                    DocumentReference docRef = _dbContext.Collection("Extrato").Document(demonstrative.Id);
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    foreach (var myArray in demonstrative.Operation)
                    {
                        var valueOperation = myArray["Operação"]; // tipo da operação - debito ou credito

                        //condicional para atualizar o saldo da operação escolhida
                        if (myArray["Id da operação"] == operationId || operationFound)
                        {
                            if(!operationFound)
                            {
                                var valueOld = myArray[$"Valor do {valueOperation}"];

                                myArray["Id da operação"] = operationId;
                                myArray[$"Valor do {valueOperation}"] = value;
                                myArray["Valor Atual"] = valueOperation == "Credito" ? (myArray["Valor Atual"] - valueOld) + value : (myArray["Valor Atual"] + valueOld) - value;
                                newBalance = myArray["Valor Atual"];
                            }else
                            {
                                //atualiza o saldos das outras operações do extrato
                                myArray["Valor Atual"] = valueOperation == "Credito" ? newBalance + myArray["Valor do Credito"] : newBalance - myArray["Valor do Debito"];
                                newBalance = myArray["Valor Atual"];
                            }
                            operationFound = true;
                        }
                        
                        arrayList.Add(myArray);
                    }
                    data.Add("Operação", arrayList);

                    await docRef.UpdateAsync(data);
                    demonstrativeFound = true;
                }
            }
        }

        public async void Delete(string date, string operationId)
        {
            Query query = _dbContext.Collection("Extrato");
            QuerySnapshot snap = await query.GetSnapshotAsync();
            bool demonstrativeFound = false;
            bool operationFound = false; //verifica se ja encontrou a operação dentro do extrato
            double newBalance = 0; // atualiza o saldo de acordo com a operação em andamento
            string typeOperation = "";

            foreach (var item in snap)
            {
                Demonstrative demonstrative = item.ConvertTo<Demonstrative>();
                demonstrative.Id = item.Id;
                if(demonstrative.Id == date || demonstrativeFound)
                {
                    ArrayList arrayList = new ArrayList();
                    DocumentReference docRef = _dbContext.Collection("Extrato").Document(demonstrative.Id);
                    Dictionary<string, object> data = new Dictionary<string, object>();

                    foreach (var myArray in demonstrative.Operation)
                    {

                        if (myArray["Id da operação"] == operationId)
                        {
                            newBalance = myArray[$"Valor do {myArray["Operação"]}"];
                            typeOperation = myArray["Operação"];
                            operationFound = true;

                            List<object> list = demonstrative.Operation.ToList();
                            list.Remove(myArray);
                            demonstrative.Operation = list.ToArray();
                        }else
                        {
                            if(!operationFound)
                            {
                                arrayList.Add(myArray);
                            }else
                            {

                                if(typeOperation != "Credito")
                                {
                                    myArray["Valor Atual"] += newBalance;
                                }else
                                {
                                    myArray["Valor Atual"] -= newBalance;
                                }
                                arrayList.Add(myArray);
                            }
                        }
                    }
                    data.Add("Operação", arrayList);

                    await docRef.UpdateAsync(data);
                    demonstrativeFound = true;
                }
            }
        }

        public async Task<dynamic> VerificationDemonstrativeId(string id)
        {
            DocumentReference docRef = _dbContext.Collection("Extrato").Document(id);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();

            if(snap.Exists)
            {
                Demonstrative demonstrative = snap.ConvertTo<Demonstrative>();
                demonstrative.Id = id;
                return demonstrative;
            }else
            {
                Demonstrative demonstrative = new Demonstrative();
                demonstrative.Id = id;
                return demonstrative;
            }

        }
    }
}
