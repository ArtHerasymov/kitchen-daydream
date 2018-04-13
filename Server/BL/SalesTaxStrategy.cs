using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.BL
{
    static class TaxReference
    {
        public const double ChinaSanctionFee = 0.1;

        public const double firstLevelAmericanSalesTax = 0.15;
        public const double secondLevelAmericanSalesTax = 0.2;

        public const double firstLevelEuropeanSalesTax = 0.2;
        public const double secondLevelEuropeanSalesTax = 0.25;

        public const double ItalianSanctionFee = 0.02;

    }

    abstract class SalesTaxStrategy
    {
        public abstract double CalculateSalesTax(double orderValue,string type);
    }

    class AmericanTaxPolicy : SalesTaxStrategy
    {
        public override double CalculateSalesTax(double orderValue, string type)
        {
            if (orderValue > 0 && orderValue <= 40)
                if (type == "Chineese")
                    return orderValue * TaxReference.firstLevelAmericanSalesTax + orderValue * TaxReference.ChinaSanctionFee;
                else if (type == "Italian")
                    return orderValue * TaxReference.firstLevelAmericanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
                else
                    return orderValue * TaxReference.firstLevelAmericanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;
            else
                if (type == "Chineese")
                    return orderValue * TaxReference.secondLevelAmericanSalesTax + orderValue * TaxReference.ChinaSanctionFee;
                else if (type == "Italian")
                    return orderValue * TaxReference.secondLevelAmericanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
                else
                    return orderValue * TaxReference.secondLevelAmericanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;

        }
    }

    class EuropeanTaxPolicy  : SalesTaxStrategy
    {
        public override double CalculateSalesTax(double orderValue, string type)
        {
            if (orderValue > 0 && orderValue <= 40)
                if (type == "Chineese")
                    return orderValue * TaxReference.firstLevelEuropeanSalesTax + orderValue * TaxReference.ChinaSanctionFee;
                else if (type == "Italian")
                    return orderValue * TaxReference.firstLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
                else
                    return orderValue * TaxReference.firstLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;
            else
                if (type == "Chineese")
                return orderValue * TaxReference.secondLevelEuropeanSalesTax + orderValue * TaxReference.ChinaSanctionFee;
            else if (type == "Italian")
                return orderValue * TaxReference.secondLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
            else
                return orderValue * TaxReference.secondLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;
        }
    }

    class AsianTaxPolicy : SalesTaxStrategy
    {
        public override double CalculateSalesTax(double orderValue, string type)
        {
            if (orderValue > 0 && orderValue <= 40)
                if (type == "Chineese")
                    return 0;
                else if (type == "Italian")
                    return orderValue * TaxReference.firstLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
                else
                    return orderValue * TaxReference.firstLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;
            else
                if (type == "Chineese")
                return 0;
            else if (type == "Italian")
                return orderValue * TaxReference.secondLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee;
            else
                return orderValue * TaxReference.secondLevelEuropeanSalesTax + orderValue * TaxReference.ItalianSanctionFee + orderValue * TaxReference.ChinaSanctionFee;

        }
    }

    class Accounting
    {
        private SalesTaxStrategy _stategy;
        public void SetCurrentSubsidiary(SalesTaxStrategy strategy)
        {
            this._stategy = strategy;
        }

        public double GetSalesTax(double orderValue, string type)
        {
            return this._stategy.CalculateSalesTax(orderValue, type);
        }
    }
}