using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Server.BL
{
    class TaxReference
    {

        dynamic deserializedValue;
        public TaxReference()
        {
            string contents = File.ReadAllText(@"E:\TaxAPI.json");
            deserializedValue = JsonConvert.DeserializeObject(contents);
        }

        public double GetFirstLevelAmericanTax()
        {
            return deserializedValue["SalesTaxes"]["FirstLevelAmericanSalesTax"];
        }

        public double GetSecondLevelAmericanTax()
        {
            return deserializedValue["SalesTaxes"]["SecondLevelAmericanSalesTax"];
        }

        public double GetFirstLevelEuropeanTax()
        {
            return deserializedValue["SalesTaxes"]["FirstLevelEuropeanSalesTax"];
        }
        public double GetSecondLevelEuropeanTax()
        {
            return deserializedValue["SalesTaxes"]["SecondLevelEuropeanSalesTax"];
        }
        public double GetFirstLevelAsianTax()
        {
            return deserializedValue["SalesTaxes"]["FirstLevelAsianSalesTax"];
        }
        public double GetSecondLevelAsianTax()
        {
            return deserializedValue["SalesTaxes"]["SecondLevelAsianTax"];
        }
        public double GetChinaSancionFee()
        {
            return deserializedValue["SanctionFees"]["ChinaSanctionFee"];
        }
        public double GetItalianSanctionFee()
        {
            return deserializedValue["SanctionFees"]["ItalianSanctionFee"];
        }
        public double GetFirstLevelTreshold()
        {
            return deserializedValue["Tresholds"]["firstLevelPriceTreshold"];
        }
    }

    abstract class Accounting
    {
        public abstract double CalculateTax(double initialPrice);
        public abstract double CalculateSanction(string menuType);
        public double GetAdditionalPrice(string menuType, double initialPrice)
        {
            return initialPrice * (CalculateSanction(menuType) + CalculateTax(initialPrice));
        }
    }

    class AmericanTax : Accounting
    {
        TaxReference taxReference = new TaxReference();

        public override double CalculateSanction(string menuType)
        {
            if (menuType == "ITALIAN")
                return taxReference.GetItalianSanctionFee();
            return 0;
        }

        public override double CalculateTax(double initialPrice)
        {
            if(initialPrice < taxReference.GetFirstLevelTreshold())
                return taxReference.GetFirstLevelAmericanTax();
            else
                return taxReference.GetSecondLevelAmericanTax();
        }
    }

    class EuropeanTax : Accounting
    {
        TaxReference taxReference = new TaxReference();

        public override double CalculateSanction(string menuType)
        {
            if (menuType == "CHINEESE")
                return taxReference.GetChinaSancionFee();
            return 0;
        }

        public override double CalculateTax(double initialPrice)
        {
            if (initialPrice < taxReference.GetFirstLevelTreshold())
                return taxReference.GetFirstLevelEuropeanTax();
            else
                return taxReference.GetSecondLevelEuropeanTax();
        }
    }

    class AsianTax : Accounting
    {
        TaxReference taxReference = new TaxReference();
        public override double CalculateSanction(string menuType)
        {
            if (menuType == "ITALIAN")
                return taxReference.GetItalianSanctionFee();
            return 0;
        }

        public override double CalculateTax(double initialPrice)
        {
            if (initialPrice < taxReference.GetFirstLevelTreshold())
                return taxReference.GetFirstLevelAsianTax();
            else
                return taxReference.GetSecondLevelAsianTax();
        }
    }
}