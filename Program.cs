using System.Text;
using DesafioProjetoHospedagem.Models;
using Newtonsoft.Json;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
HospedagePessoa<Pessoa> hospedes = new HospedagePessoa<Pessoa>();

Pessoa p1 = new Pessoa(nome: "Hóspede 1");
Pessoa p2 = new Pessoa(nome: "Hóspede 2");

hospedes.Add(p1);
hospedes.Add(p2);

// Cria a suíte
Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

// Cria uma nova reserva, passando a suíte e os hóspedes
Reserva reserva = new Reserva(diasReservados: 3);
reserva.CadastrarSuite(suite);
reserva.CadastrarHospedes(hospedes.GetValues);

// Exibe a quantidade de hóspedes e o valor da diária
Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria()}");


public class HospedagePessoa<T>
{

  public HospedagePessoa()
  {

    Values = new List<T>();
  }

  private List<T> Values;

  public void Add(T value)
  {
    FileAddObjet("SystemFile/hospedes.json", value);
    Values.Add(value);
  }

  public List<T> GetValues => Values;
  public override string ToString()
  {
    var obj = GetObjetJson("SystemFile/hospedes.json");
    return $"Hospedes atuais: \n {obj}";
  }
  private void FileAddObjet(string pathAndFile, T newValue)
  {
    List<T> newObjList = new List<T>();

    if (File.Exists(pathAndFile))
    {
      var objectJson = File.ReadAllText(pathAndFile);

      List<T> values = JsonConvert.DeserializeObject<List<T>>(objectJson);
      if (values != null)
      {
        newObjList.AddRange(values);
      }

    }

    newObjList.Add(newValue);

    var jsonConverted = JsonConvert.SerializeObject(newObjList, Formatting.Indented);

    File.WriteAllText(pathAndFile, jsonConverted);

  }

  private string GetObjetJson(string pathName)
  {
    var objectJson = File.ReadAllText(pathName);
    return objectJson;
  }
}