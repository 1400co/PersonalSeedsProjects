using System;
using UmpaLumpaBTC.BusinessLayer;
using UmpaLumpaBTC.Persistence;
using UmpaLumpaBTC.Providers.Interface;
using Unity;
using Unity.Lifetime;

namespace UmpaLumpaBTC.Application
{
    class Program
    {
        static IUnityContainer _container;

        static void Main(string[] args)
        {

            Console.Write("Consultando api...");
            Console.Write("");

            ConfigureContainer();

            Console.ReadLine();

        }

        private static void ConfigureContainer()
        {

            _container = new UnityContainer();

            _container.RegisterType<IProviders, Coinmarketcap.Client.Coinmarketcap>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IMarketProvider, MarketProvider>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ICurrencyDAO, CurrencyDAO>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<IStrategyDAO, StrategyDAO>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ITransactionLayer, TransactionLayer>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<ITransactionDAO, TransactionDAO>(
                new ContainerControlledLifetimeManager());

            var algo = _container.Resolve<UmpaLumpaWorker>();
            algo.UpdateCurrentBalance();

        }

    }





}
