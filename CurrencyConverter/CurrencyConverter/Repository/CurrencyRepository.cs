using CurrencyConverter.DBContexts;
using CurrencyConverter.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly CurrencyConverterContext _dbContext;

        private readonly string BASE_CURRENCY = "USD";

        public CurrencyRepository(CurrencyConverterContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteCurrency(string currencyName)
        {
            if (currencyName == BASE_CURRENCY)
            {
                string message = $"{BASE_CURRENCY} é a moeda base da API, a mesma não pode ser excluída.";
                throw new Exception(message);
            }

            Currency currency = _dbContext.Currency.Where(bean => bean.Name == currencyName).FirstOrDefault();
            if (currency != null)
            {
                _dbContext.Currency.Remove(currency);
                Save();
            }
            else
            {
                string message = $"Não foi possível encontrar a moeda {currencyName} cadastrada na base de dados.";
                throw new ArgumentNullException(message);
            }
        }

        public Currency GetCurrencyById(long currencyId)
        {
            Currency currency = _dbContext.Currency.Where(bean => bean.Id == currencyId).FirstOrDefault();
            if (currency != null)
            {
                return currency;
            }
            else
            {
                string message = $"Não foi possível encontrar a moeda com Id {currencyId} cadastrada na base de dados.";
                throw new ArgumentNullException(message);
            }
        }

        public Currency GetCurrencyByName(string currencyName)
        {
            Currency currency = _dbContext.Currency.Where(bean => bean.Name == currencyName).FirstOrDefault();
            if (currency != null)
            {
                return currency;
            }
            else
            {
                string message = $"Não foi possível encontrar a moeda {currencyName} cadastrada na base de dados.";
                throw new ArgumentNullException(message);
            }
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _dbContext.Currency.ToList();
        }

        public void InsertCurrency(Currency currency)
        {
            _dbContext.Add(currency);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
