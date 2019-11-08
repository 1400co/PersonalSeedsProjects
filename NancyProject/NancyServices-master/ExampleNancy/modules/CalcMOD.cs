using ExampleNancy.DTO;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json;
using System;
using WalletsApi.Views;

namespace ExampleNancy.modules
{

    public class CalcMod : NancyModule
    {

        public CalcMod()
        {
            
            Get("/calc/{category}", parameters =>
            {
                var wallet = new WalletView()
                {
                    Id = (Nancy.DynamicDictionaryValue)parameters.category,
                    BankAccountNumber = "123987",
                    CurrentBalance = 150000,
                };

                return wallet; 
            });


            Post("/calc", parameters =>
                {

                    var a = this.Request.Body.AsString();
                    Console.WriteLine("Los parametros llegaron "+a);
                    var b = JsonConvert.DeserializeObject<RequestGeneric>(a);

                    var wallet = new WalletView()
                    {
                        Id = b.Key,
                        BankAccountNumber = b.Object,
                        CurrentBalance = 150000,
                    };

                    return wallet;
                }

            );

        }
    }
}
