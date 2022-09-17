using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;
using SerializationBenchmarks.Models;
using KM = SerializationBenchmarks.Models.KM;

public partial class BinaryBenchmark
{
    [Benchmark, BenchmarkCategory("Serialization", "Binary"), ArgumentsSource(nameof(GenerateKarmemDataSets))]
    public byte[] Karmem_Serialize(KarmemDataSet data)
    {
        var w = Karmem.Writer.NewWriter(0);
        var r = DataConvert_Karmem(data.Payload, w).ToArray();
        w.Dispose();
        return r;
    }
    
    [Benchmark, BenchmarkCategory("Deserialization", "Binary"), ArgumentsSource(nameof(GenerateKarmemDataSets))]
    public List<KM.User> Karmem_Deserialize(KarmemDataSet data)
    {
        KM.UserWrapper r = new KM.UserWrapper();
        r.ReadAsRoot(Karmem.Reader.NewManagedReader(data.Data));
        return r._Users;
    }
    
    [Benchmark, BenchmarkCategory("Serialization", "Binary"), ArgumentsSource(nameof(GenerateKarmemDataSets))]
    public Span<byte> KarmemReusable_Serialize(KarmemDataSet data)
    {
        return DataConvert_Karmem(data.Payload, data.Writer);
    }
    
    [Benchmark, BenchmarkCategory("Deserialization", "Binary"), ArgumentsSource(nameof(GenerateKarmemDataSets))]
    public List<KM.User> KarmemReusable_Deserialize(KarmemDataSet data)
    {
        data.Struc.ReadAsRoot(Karmem.Reader.NewManagedReader(data.Data));
        return data.Struc._Users;
    }
    
    private Span<byte> DataConvert_Karmem(List<KM.User> users, Karmem.Writer w)
    {
        w.Reset();
        KM.UserWrapper r = new KM.UserWrapper
        {
            _Users = users
        };
        r.WriteAsRoot(w);
        return w.Bytes();
    }

    private List<KM.User> DataConvertNativeUser_Karmem(List<User> users)
    {
        // Karmem doesn't support nullable types, so we need to convert them to non-nullable types,
        // and it uses List instead of Array, and doesn't support GUID ([16]byte is used instead)
        // so we need to convert that too.
        var r = new List<KM.User>(users.Count);
        foreach (var u in users)
        {
            var orders = new List<KM.Order>(u.Orders.Count);
            foreach (var o in u.Orders)
            {
                orders.Add(new KM.Order
                {
                    _OrderId = o.OrderId,
                    _Item = o.Item,
                    _LotNumber = o.LotNumber ?? 0,
                    _Quantity = o.Quantity,
                });
            }
            
            r.Add(new KM.User
            {
                _Id = u.Id,
                _FirstName = u.FirstName ?? "",
                _LastName = u.LastName ?? "",
                _FullName = u.FullName ?? "",
                _UserName = u.UserName ?? "",
                _Email = u.Email ?? "",
                _SomethingUnique = u.SomethingUnique,
                _SomeGuid = new List<byte>(u.SomeGuid.ToByteArray()),
                _Avatar = u.Avatar ?? "",
                _CartId = new List<byte>(u.CartId.ToByteArray()),
                _SSN = u.SSN ?? "",
                _Gender = u.Gender == Gender.Male ? KM.Gender.Male : KM.Gender.Female,
                _Orders = orders,
            });
        }

        return r;
    }
    
    public IEnumerable<KarmemDataSet> GenerateKarmemDataSets()
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        options.Converters.Add(new JsonStringEnumConverter());
        var sets = new[]
        {
#if DATASET_SMALL            
            new { File = "Data_Small.json", Subset = "Small" },
#endif
#if DATASET_MEDIUM            
            new { File = "Data_Medium.json", Subset = "Medium" },
#endif
#if DATASET_LARGE            
            new { File = "Data_Large.json", Subset = "Large" },
#endif
        };

        foreach (var set in sets)
        {
            var json = File.ReadAllText(set.File);
            var users = JsonSerializer.Deserialize<List<User>>(json, options)!;
            yield return new KarmemDataSet
            {
                Name = set.Subset,
                Payload =  DataConvertNativeUser_Karmem(users),
                Data = DataConvert_Karmem( DataConvertNativeUser_Karmem(users), Karmem.Writer.NewWriter(0)).ToArray(),
                Struc = new KM.UserWrapper { },
                Writer = Karmem.Writer.NewWriter(1<<24),
            };
        }
    }

    public class KarmemDataSet
    {
        public string Name { get; set; }
        public List<KM.User> Payload { get; set; }
        public byte[] Data { get; set; }
        public override string ToString() => Name;

        public KM.UserWrapper Struc;
        public Karmem.Writer Writer;
    }
}